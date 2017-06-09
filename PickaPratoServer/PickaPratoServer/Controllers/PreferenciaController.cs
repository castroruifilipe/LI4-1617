using PickaPratoServer.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PickaPratoServer.Controllers
{
    public class PreferenciaController : ApiController {

        private PreferenciaDAO preferencias = new PreferenciaDAO();

        // GET: api/Preferencia
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Preferencia/5
        [HttpGet]
        public List<string> Get(string id)
        {
            List<string> r = new List<string>();
            r = preferencias.Get(id);
            return r;
        }

        // POST: api/Preferencia
        public void Post(List<string> value)
        {
            preferencias.Put(value);
        }

        // PUT: api/Preferencia/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Preferencia/5
        public void Delete(int id)
        {
        }
    }
}
