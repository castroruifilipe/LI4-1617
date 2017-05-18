using System;
using System.IO;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PickaPrato.Business;

namespace PickaPrato.Data {
    
    public class ServiceEngine {

        private HttpClient client;
        private String urlBase = "http://10.0.2.2:5001/";
        
        public ServiceEngine() {
            client = new HttpClient();
            client.BaseAddress = new Uri(urlBase);
        }

        public async Task<Cliente> GetCliente(string username) {
            var response = client.GetAsync("api/Cliente/" + username).Result;
            if (response.IsSuccessStatusCode) {
                var stream = await response.Content.ReadAsStringAsync();
                Cliente c = JsonConvert.DeserializeObject<Cliente>(stream);
                return c;
            } else {
                return null;
            }
        }

        public async Task PostCliente(Cliente c) {

            var uri = new Uri(urlBase + "api/Cliente");

            var json = JsonConvert.SerializeObject(c);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(uri, content);

        }
    }
}
