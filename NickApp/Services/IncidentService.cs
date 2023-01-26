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
    public class IncidentService : IIncidentService
    {
        private readonly HttpClient _httpClient;

        private JsonSerializer _serializer = new JsonSerializer();

        public IncidentService(HttpClient httpClient)
        {
             _httpClient = httpClient; _httpClient.BaseAddress = new Uri("http://localhost:8090/api/");

            // _httpClient.DefaultRequestHeaders.Add("ApiKey", "hL4bA4nB4yI0vI0fC8fH87eTy7236");

            _httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));


        }

        public async Task<IEnumerable<Incident>> GetIncident()
        {
            //var response = await _httpClient.GetAsync("Incident​/GetAllIncident");

            var response = await _httpClient.GetAsync("Incident/GetAllIncident");

            response.EnsureSuccessStatusCode();


            using (var stream = await response.Content.ReadAsStreamAsync())
            using (var reader = new StreamReader(stream))
            using (var json = new JsonTextReader(reader))
            {
                return _serializer.Deserialize<IEnumerable<Incident>>(json);
            }

        
        }

        public async Task<IEnumerable<Incident>> GetIncident(string IncidentCode)
        {

            var response = await _httpClient.GetAsync($"Incident/GetIncident?IncidentCode={IncidentCode}");

            response.EnsureSuccessStatusCode();

            using (var stream = await response.Content.ReadAsStreamAsync())
            using (var reader = new StreamReader(stream))
            using (var json = new JsonTextReader(reader))
            {
                return _serializer.Deserialize<IEnumerable<Incident>>(json);
            }

        }
       

        public async Task AddIncident(Incident Incident)
        {
            var response = await _httpClient.PostAsync("Incident/AddIncident", new StringContent(JsonConvert.SerializeObject(Incident), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
        }
            public async Task DeleteIncident(Incident Incident)
        {  
            var response = await _httpClient.DeleteAsync($"Incident/DeleteIncident?IncidentCode={Incident.IncidentCode}");

            response.EnsureSuccessStatusCode();
        }      

        public async Task SaveIncident(Incident Incident)
        {

              var response = await _httpClient.PutAsync($"Incident/UpdateIncident",
                new StringContent(JsonConvert.SerializeObject(Incident), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
        }
    }
}
