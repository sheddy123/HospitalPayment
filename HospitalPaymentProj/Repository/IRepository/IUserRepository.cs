using HospitalPaymentProj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HospitalPaymentProj.Repository.IRepository
{
    public interface IUserRepository : IRepository<Users>
    {
        Task<string> AuthenticateUser(Users user);
        Task<bool> RegisterUser(Users user);
        Task<string> CreatePatientRecord(PatientPayment_VM _patientDetails);
        Task<List<PatientPayment_VM>> GetPatientRecord();
    }
}
