using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using NickApp.Models;
using System.IO;

namespace NickApp.Services
{
    public class UserAccountService : IUserAccountService
    {
        private readonly HttpClient _httpClient;

        private JsonSerializer _serializer = new JsonSerializer();
        public UserAccountService(HttpClient httpClient)
        {
             _httpClient = httpClient; _httpClient.BaseAddress = new Uri("http://localhost:8090/api/");
           
            // _httpClient.DefaultRequestHeaders.Add("ApiKey", "hL4bA4nB4yI0vI0fC8fH87eTy7236");

            _httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));


        }

        public async Task<IEnumerable<UserAccount>> GetUserAccountByUserName(string UserName)
        {

            var response = await _httpClient.GetAsync($"UserAccount/GetUserAccountByUserName?UserName={UserName}");

            response.EnsureSuccessStatusCode();

            using (var stream = await response.Content.ReadAsStreamAsync())
            using (var reader = new StreamReader(stream))
            using (var json = new JsonTextReader(reader))
            {
                return _serializer.Deserialize<IEnumerable<UserAccount>>(json);
            }

        }

        public async Task AddUserAccount(UserAccount UserAccount)
        {
            var response = await _httpClient.PostAsync("UserAccount/AddUserAccount", new StringContent(JsonConvert.SerializeObject(UserAccount), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
        }
            public async Task DeleteUserAccount(UserAccount UserAccount)
        {  
            var response = await _httpClient.DeleteAsync($"UserAccount/DeleteUserAccount?UserAccountCode={UserAccount.UserAccountCode}");

            response.EnsureSuccessStatusCode();
        }      

        public async Task SaveUserAccount(UserAccount UserAccount)
        {

              var response = await _httpClient.PutAsync($"UserAccount/UpdateUserAccount",
                new StringContent(JsonConvert.SerializeObject(UserAccount), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
        }
    }
}
