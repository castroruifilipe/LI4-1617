using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PickaPrato.Business;

namespace PickaPrato.Data {
    
    public class ServiceEngine {

        private HttpClient client;
        
        public ServiceEngine(string url) {

            client = new HttpClient();
            client.BaseAddress = new Uri(url);
        }

        public async Task<Person> GetCliente() {
            Console.Out.Write("\n\n\n\n\nAqui1\n\n\n\n");
            var response = client.GetAsync("api/Default/").Result;
            Console.Out.Write("\n\n\n\n\nAqui2\n\n\n\n");
            if (response.IsSuccessStatusCode) {
                Console.Out.Write("\n\n\n\n\nAqui3\n\n\n\n");
                var stream = await response.Content.ReadAsStreamAsync();
                Console.Out.Write("\n\n\n\n\nAqui4\n\n\n\n");
                var sr = new StreamReader(stream);
                Console.Out.Write("Aqui5");
                var jsonReader = new JsonTextReader(sr);
                Console.Out.Write("Aqui6");

                var serializer = new JsonSerializer();
                Console.Out.Write("Aqui7");
                Person p = serializer.Deserialize<Person>(jsonReader);
                Console.Out.Write("Aqui8");

                return p;
            } else {
                return null;
            }
        }
    }
}
