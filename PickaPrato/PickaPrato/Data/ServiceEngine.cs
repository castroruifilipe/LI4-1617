using System;
using System.IO;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PickaPrato.Business;
using System.Net;

namespace PickaPrato.Data {
    
    public class ServiceEngine {

        private HttpClient client;
        private String urlBase = "http://192.168.1.8/PickaPratoServer/";


        public ServiceEngine() {
            client = new HttpClient();
            client.BaseAddress = new Uri(urlBase);
        }

        public async Task<Cliente> GetCliente(String username) {
            var response = client.GetAsync("api/Cliente/" + username).Result;
            //if (response.IsSuccessStatusCode == true) {
            var stream = await response.Content.ReadAsStringAsync();
            Cliente c = JsonConvert.DeserializeObject<Cliente>(stream);
            return c;
            //}
        }

        public async Task PostCliente(Cliente c) {
            var uri = new Uri(urlBase + "api/Cliente");
            var json = JsonConvert.SerializeObject(c);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(uri, content);
        }

        public async Task<Proprietario> GetProprietario(String username) {
			var response = client.GetAsync("api/Cliente/" + username).Result;
			//if (response.IsSuccessStatusCode == true) {
			var stream = await response.Content.ReadAsStringAsync();
			Proprietario p = JsonConvert.DeserializeObject<Proprietario>(stream);
			return p;
			//}
		}
    }
}
