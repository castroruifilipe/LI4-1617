using PickaPratoServer.Data;
using PickaPratoServer.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PickaPratoServer.Controllers
{
    public class RestauranteController : ApiController
    {
        private RestauranteDAO restaurantes = new RestauranteDAO();

        // GET: api/Restaurante
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Restaurante/5
        public Restaurante Get(String id)
        {
            Restaurante r = restaurantes.Get(id);
            return r;
        }

        // POST: api/Restaurante
        public void Post(Restaurante r)
        {
                restaurantes.Put(r);
        }

        // PUT: api/Restaurante/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Restaurante/5
        public void Delete(int id)
        {
        }
    }
}
