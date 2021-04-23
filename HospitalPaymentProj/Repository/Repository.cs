using HospitalPaymentProj.Models;
using HospitalPaymentProj.Repository.IRepository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HospitalPaymentProj.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IHttpClientFactory _clientFactory;

        public Repository(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<string> Authenticate(string url, Users users)
        {
            using(var httpClient = new HttpClient())
            {
                StringContent cont = new StringContent(JsonConvert.SerializeObject(users), Encoding.UTF8, "application/json");
                using(var req = await httpClient.PostAsync(url, cont))
                {
                    var _apiRes = req.Content.ReadAsStringAsync();
                    return _apiRes.Result == "0" ? "0" : _apiRes.Result;
                }
            }
        }

        public async Task<bool> CreateAsync(string url, T objToCreate)
        {
            using(var httpClient = new HttpClient())
            {
                StringContent conten1 = new StringContent(JsonConvert.SerializeObject(objToCreate), Encoding.UTF8, "application/json");
                using(var res = await httpClient.PostAsync(url, conten1))
                {
                    var _apiRes = await res.Content.ReadAsStringAsync();
                    switch (_apiRes)
                    {
                        case "true": return true;
                        case "Created successfully": return true;
                        default: return false;
                    }
                }
            }
        }

        public async Task<bool> DeleteAsync(string url, int Id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, url + Id);
            var client = _clientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                return true;
            
            return false;
        }

        public async Task<T> GetAsync(string url, int Id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url + "/" + Id);
            var client = _clientFactory.CreateClient();
            
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(jsonString);
            }
            return null;
        }

        public async Task<IEnumerable<T>> GetAsyncAll(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var client = _clientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<T>>(jsonString);
            }
            return null;
        }

        public async Task<bool> UpdateAsync(string url, T objToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
