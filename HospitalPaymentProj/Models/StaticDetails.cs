using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalPaymentProj.Models
{
    public class StaticDetails
    {
        
        public static IConfiguration Configuration { get; set; }
        private static string BasePaths(string path)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(JsonConfigFile.AppSetting);
            Configuration = builder.Build();
            string apiBasePath = Configuration[path];
            return apiBasePath;
        }


        public static string _basePath = "https://localhost:44383/Api/";
        public static string _loginPath = _basePath + "Users/login";
        public static string _registerUserPath = _basePath + "Users/register";
        public static string _savePatientData = _basePath + "Users/SavePatientData";
        public static string _getPatientData = _basePath + "Users/GetPatientData";
        public static string _connectionStringPath = BasePaths(JsonConfigFile.ConnectionString);
    }
    public class JsonConfigFile
    {
        public const string AppSetting = "appsettings.json";
        public const string ConnectionString = "ConnectionStrings:DefaultConnection";
    }
}
