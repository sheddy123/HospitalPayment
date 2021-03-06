USE [HospitalTransactions]
GO
/****** Object:  Table [dbo].[tbl_PatientsRecord]    Script Date: 23/04/2021 11:36:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_PatientsRecord](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](100) NULL,
	[LastName] [varchar](100) NULL,
	[PhoneNumber] [varchar](100) NULL,
 CONSTRAINT [PK_tbl_PatientsRecord] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_Transaction]    Script Date: 23/04/2021 11:36:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Transaction](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[PatientId] [int] NULL,
	[Amount] [decimal](18, 0) NOT NULL,
	[DateCreated] [datetime] NULL,
 CONSTRAINT [PK_tbl_Transaction] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_User]    Script Date: 23/04/2021 11:36:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NULL,
	[Password] [varchar](100) NULL,
	[DateCreated] [datetime] NULL,
 CONSTRAINT [PK_tbl_User_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[tbl_Transaction] ADD  CONSTRAINT [DF_tbl_Transaction_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[tbl_User] ADD  CONSTRAINT [DF_tbl_User_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[tbl_Transaction]  WITH CHECK ADD  CONSTRAINT [FK_tbl_Transaction_tbl_PatientsRecord] FOREIGN KEY([PatientId])
REFERENCES [dbo].[tbl_PatientsRecord] ([Id])
GO
ALTER TABLE [dbo].[tbl_Transaction] CHECK CONSTRAINT [FK_tbl_Transaction_tbl_PatientsRecord]
GO
/****** Object:  StoredProcedure [dbo].[STP_AUTHENTICATE_USER]    Script Date: 23/04/2021 11:36:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================
-- Author: Ifeanyi Odom
-- Create date: 22nd/04/2020
-- Description:	Procedure to Authenticate User
-- ==========================================================
CREATE PROCEDURE [dbo].[STP_AUTHENTICATE_USER]
	@UserName						nvarchar(300),
	@Password						nvarchar(300)
AS
BEGIN
	SET NOCOUNT ON;
	Declare @retValue int

	IF EXISTS (SELECT TOP(1) [Id]  from tbl_User WHERE UserName = @UserName and Password=@Password)
		BEGIN
			set @retValue = (SELECT TOP(1) [Id]  from tbl_User WHERE UserName = @UserName and Password=@Password)
			RETURN @retValue
		END
	ELSE
			set @retValue = 0
		RETURN @retValue

	SET NOCOUNT OFF;
END
GO
/****** Object:  StoredProcedure [dbo].[STP_CREATE_PATIENT]    Script Date: 23/04/2021 11:36:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================
-- Author: Ifeanyi Odom
-- Create date: 23RD/04/2020
-- Description:	Procedure to Register User
-- ==========================================================
CREATE PROCEDURE [dbo].[STP_CREATE_PATIENT]
	@UserId						nvarchar(300),
	@Amount						decimal(18,0),
	@PhoneNumber				nvarchar(100),
	@FirstName					nvarchar(100),
	@LastName					nvarchar(100)

AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @PatientExist nvarchar(100)
	DECLARE @patientId int

	IF NOT EXISTS (SELECT TOP(1) [Id]  from tbl_PatientsRecord WHERE PhoneNumber = @PhoneNumber)
		BEGIN
			INSERT INTO tbl_PatientsRecord(
			FirstName,
			PhoneNumber,
			LastName
			)
			VALUES(
			@FirstName,
			@LastName,
			@PhoneNumber
			)

			SET @patientId = IDENT_CURRENT('tbl_PatientsRecord')

			INSERT INTO tbl_Transaction(
			UserId,
			PatientId,
			Amount)
			VALUES(
			@UserId,
			@patientId,
			@Amount)
			RETURN 1
		END
	ELSE
		RETURN 0

	SET NOCOUNT OFF;
END
GO
/****** Object:  StoredProcedure [dbo].[STP_GET_PATIENTS_RECORD]    Script Date: 23/04/2021 11:36:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================
-- Author: Ifeanyi Odom
-- Create date: 23RD/04/2020
-- Description:	Procedure to Get Patient Record
-- ==========================================================
CREATE PROCEDURE [dbo].[STP_GET_PATIENTS_RECORD]
	
AS
BEGIN
	SET NOCOUNT ON;
	
	Select p.FirstName, p.LastName, p.PhoneNumber, t.Amount, t.DateCreated, u.UserName from tbl_PatientsRecord p inner join
	tbl_Transaction t on p.Id = t.PatientId inner join tbl_User u on t.UserId = u.Id
	
	SET NOCOUNT OFF;
END
GO
/****** Object:  StoredProcedure [dbo].[STP_REGISTER_USER]    Script Date: 23/04/2021 11:36:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================
-- Author: Ifeanyi Odom
-- Create date: 22nd/04/2020
-- Description:	Procedure to Register User
-- ==========================================================
CREATE PROCEDURE [dbo].[STP_REGISTER_USER]
	@UserName						nvarchar(300),
	@Password						nvarchar(300)
AS
BEGIN
	SET NOCOUNT ON;
	
	IF NOT EXISTS (SELECT TOP(1) [Id]  from tbl_User WHERE UserName = @UserName and Password=@Password)
		BEGIN
			INSERT INTO tbl_User(
			UserName,
			[Password])
			VALUES(
			@UserName,
			@Password)
			RETURN 1
		END
	ELSE
		RETURN 0

	SET NOCOUNT OFF;
END
GO
