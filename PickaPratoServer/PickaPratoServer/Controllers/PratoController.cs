using PickaPratoServer.Data;
using PickaPratoServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PickaPratoServer.Controllers
{
    public class PratoController : ApiController
    {
        private PratoDAO pratos = new PratoDAO();

        // GET: api/Prato
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Prato/5
        public Prato Get(int id)
        {
            Prato p = pratos.Get(id);
            return p;
        }

        // POST: api/Prato
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Prato/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Prato/5
        public void Delete(int id)
        {
        }
    }
}
