using HospitalPaymentProj.Models;
using HospitalPaymentProj.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HospitalPaymentProj.Repository
{
    public class UserRepository : Repository<Users>, IUserRepository
    {
        private readonly IHttpClientFactory _clientFactory;
        public UserRepository(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public Task<string> AuthenticateUser(Users user)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(StaticDetails._connectionStringPath))
                {
                    using (SqlCommand cmd = new SqlCommand("STP_AUTHENTICATE_USER", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserName", user.UserNameLogin);
                        cmd.Parameters.AddWithValue("@Password", user.PasswordLogin);

                        cmd.Parameters.Add("@retValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                        con.Open();
                        cmd.ExecuteNonQuery();

                        int retval = (int)cmd.Parameters["@retValue"].Value;
                        con.Close();
                        
                        if (retval != 0)
                            return Task.FromResult(retval.ToString());
                        
                        return Task.FromResult(retval.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Task<List<PatientPayment_VM>> GetPatientRecord()
        {
            List<PatientPayment_VM> patientInfo = new List<PatientPayment_VM>();
            try
            {
                using (SqlConnection con = new SqlConnection(StaticDetails._connectionStringPath))
                {
                    using (SqlCommand cmd = new SqlCommand("STP_GET_PATIENTS_RECORD", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 12000;

                        con.Open();
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var patientObj = new PatientPayment_VM
                                {
                                    FirstName = reader["FirstName"].ToString(),
                                    DateCreated = Convert.ToDateTime(reader["DateCreated"]).ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    PhoneNumber = reader["PhoneNumber"].ToString(),
                                    Amount = reader["Amount"].ToString(),
                                    AdminAttended = reader["UserName"].ToString(),
                                };
                                patientInfo.Add(patientObj);
                            }
                        }
                        if (patientInfo.Count > 0)
                        {
                            return Task.FromResult(patientInfo);
                        }
                        else
                        {
                            return Task.FromResult(patientInfo);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Task<string> CreatePatientRecord(PatientPayment_VM _patientDetails)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(StaticDetails._connectionStringPath))
                {
                    using (SqlCommand cmd = new SqlCommand("STP_CREATE_PATIENT", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserId", _patientDetails.UserId);
                        cmd.Parameters.AddWithValue("@FirstName", _patientDetails.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", _patientDetails.LastName);
                        cmd.Parameters.AddWithValue("@PhoneNumber", _patientDetails.PhoneNumber);
                        cmd.Parameters.AddWithValue("@Amount", Convert.ToDecimal(_patientDetails.Amount));

                        cmd.Parameters.Add("@retValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                        con.Open();
                        cmd.ExecuteNonQuery();

                        int retval = (int)cmd.Parameters["@retValue"].Value;
                        con.Close();

                        if (retval == 1)
                            return Task.FromResult("Created successfully");

                        return Task.FromResult("Error Creating User");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Task<bool> RegisterUser(Users user)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(StaticDetails._connectionStringPath))
                {
                    using (SqlCommand cmd = new SqlCommand("STP_REGISTER_USER", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserName", user.UserName);
                        cmd.Parameters.AddWithValue("@Password", user.Password);

                        cmd.Parameters.Add("@retValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                        con.Open();
                        cmd.ExecuteNonQuery();

                        int retval = (int)cmd.Parameters["@retValue"].Value;
                        con.Close();

                        if (retval == 1)
                            return Task.FromResult(true);

                        return Task.FromResult(false);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
