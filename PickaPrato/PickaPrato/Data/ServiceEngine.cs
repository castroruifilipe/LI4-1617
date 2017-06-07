using System;
using System.IO;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PickaPrato.Business;
using System.Net;
using System.Collections.Generic;
using PickaPrato.Exceptions;

namespace PickaPrato.Data {
    
    public class ServiceEngine {

        private HttpClient client;
        private String urlBase = "http://192.168.1.100/PickaPratoServer/";


        public ServiceEngine() {
            client = new HttpClient();
            client.BaseAddress = new Uri(urlBase);
        }

        public async Task<Cliente> GetCliente(String username) {
            var response = client.GetAsync("api/Cliente/" + username).Result;
            var stream = await response.Content.ReadAsStringAsync();
            Cliente c = JsonConvert.DeserializeObject<Cliente>(stream);
            return c;
        }

        public async Task PostCliente(Cliente c) {
            var uri = new Uri(urlBase + "api/Cliente");
            var json = JsonConvert.SerializeObject(c);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(uri, content);
        }

        public async Task<Restaurante> GetRestaurante(String username) {
			var response = client.GetAsync("api/Restaurante/" + username).Result;
			var stream = await response.Content.ReadAsStringAsync();
			Restaurante p = JsonConvert.DeserializeObject<Restaurante>(stream);
			return p;
		}

        public async Task PostRestaurante(Restaurante r) {
            var uri = new Uri(urlBase + "api/Restaurante");
            var json = JsonConvert.SerializeObject(r);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(uri, content);
        }

        public async Task<List<string>> GetPreferencias(String username) {
            var response = client.GetAsync("api/Preferencia/" + username).Result;
            var stream = await response.Content.ReadAsStringAsync();
            List<string> preferencias = JsonConvert.DeserializeObject<List<string>>(stream);
            return preferencias;
        }

        public async Task PostPreferencias(String username, List<string> preferencias) {
            var uri = new Uri(urlBase + "api/Preferencia");
            preferencias.Insert(0, username);
            var json = JsonConvert.SerializeObject(preferencias);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(uri, content);
        }

        public async Task<List<string>> GetIngredientes() {
            var response = client.GetAsync("api/Ingrediente").Result;
            var stream = await response.Content.ReadAsStringAsync();
            List<string> ingredientes = JsonConvert.DeserializeObject<List<string>>(stream);
            return ingredientes;
        }

		public async Task PostPrato(Prato p) {
			var uri = new Uri(urlBase + "api/Prato");
			var json = JsonConvert.SerializeObject(p);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await client.PostAsync(uri, content);
		}
    }
}
