using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Http;
using PickaPratoServer.Models;

namespace PickaPratoServer.Controllers {

    public class ClienteController : ApiController {

        // GET: api/Cliente
        public IEnumerable<string> Get() {
            Console.Out.Write("Um pedido recebido\n");
            return new string[] { "Cliente", "Cliente" };
        }

        // GET: api/Cliente/5
        public Cliente Get(String id) {
            Console.Out.Write("Um pedido recebido" + id + "\n");
            Cliente c = new Cliente();
            c.Username = "Alves";
            c.Password = "Joao";
            return c;
        }

        // POST: api/Cliente
        public void Post([FromBody]Cliente value) {
            Console.Out.WriteLine("Valores lidos: " + value.Username + value.Password);
        }

        // PUT: api/Cliente/5
        public void Put(int id, [FromBody]string value) {
        }

        // DELETE: api/Cliente/5
        public void Delete(int id) {
        }
    }
}