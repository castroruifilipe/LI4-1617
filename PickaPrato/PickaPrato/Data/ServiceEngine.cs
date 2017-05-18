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
        
        public ServiceEngine(string url) {

            client = new HttpClient();
            client.BaseAddress = new Uri(url);
        }

        public async Task<Cliente> GetCliente() {
            var response = client.GetAsync("api/Cliente/2").Result;
            if (response.IsSuccessStatusCode) {
                var stream = await response.Content.ReadAsStringAsync();
                //var sr = new StreamReader(stream);
                //var jsonReader = new JsonTextReader(sr);
                //var serializer = new JsonSerializer();
                //String[] p = serializer.Deserialize<String[]>(jsonReader);
                //Console.Out.Write("\n\n\n" + p[1] + p[2] + "\n\n\n");
                //return p;
                Cliente c = JsonConvert.DeserializeObject<Cliente>(stream);
                return c;
            } else {
                return null;
            }
        }
    }
}
