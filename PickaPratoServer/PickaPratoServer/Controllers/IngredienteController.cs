using PickaPratoServer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PickaPratoServer.Controllers
{
    public class IngredienteController : ApiController
    {
        private IngredienteDAO ingredientes = new IngredienteDAO();

        // GET: api/Ingrediente
        public IEnumerable<string> Get()
        {
            return ingredientes.Get();
        }

        // GET: api/Ingrediente/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Ingrediente
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Ingrediente/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Ingrediente/5
        public void Delete(int id)
        {
        }
    }
}
