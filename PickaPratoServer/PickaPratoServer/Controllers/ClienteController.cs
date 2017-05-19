using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Http;
using PickaPratoServer.Models;
using PickaPratoServer.Data;
using System.Diagnostics;

namespace PickaPratoServer.Controllers {

    public class ClienteController : ApiController {

        private ClienteDAO clientes = new ClienteDAO();

        // GET: api/Cliente
        public IEnumerable<string> Get() {
            Console.Out.Write("Um pedido recebido\n");
            return new string[] { "Cliente", "Cliente" };
        }

        // GET: api/Cliente/5
        public Cliente Get(String id) {
            Console.Out.Write("Um pedido recebido" + id + "\n");
            Cliente c = clientes.GetCliente();
            return c;
        }

        // POST: api/Cliente
        public void Post([FromBody]Cliente value) {
            Debug.Write("Chego aqui!!!!!!!" + value.Username + "\n");
            clientes.Put(value);
        }

        // PUT: api/Cliente/5
        public void Put(int id, [FromBody]string value) {
        }

        // DELETE: api/Cliente/5
        public void Delete(int id) {
        }
    }
}