using HospitalPaymentProj.Models;
using HospitalPaymentProj.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace HospitalPaymentProj.Repository
{
    public class PaymentRepository : Repository<PatientPayment_VM>, IPaymentRepository
    {
        private readonly IHttpClientFactory _clientFactory;

        public PaymentRepository(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }

    }
}
