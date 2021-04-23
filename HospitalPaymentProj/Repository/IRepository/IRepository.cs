using HospitalPaymentProj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalPaymentProj.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<bool> CreateAsync(string url, T objToCreate);
        Task<bool> UpdateAsync(string url, T objToUpdate);
        Task<bool> DeleteAsync(string url, int Id);
        Task<T> GetAsync(string url, int Id);
        Task<IEnumerable<T>> GetAsyncAll(string url);
        Task<string> Authenticate(string url, Users users);
    }
}
