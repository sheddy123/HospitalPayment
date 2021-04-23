using HospitalPaymentProj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalPaymentProj.Repository.IRepository
{
    public interface IPaymentRepository : IRepository<PatientPayment_VM>
    {
    }
}
