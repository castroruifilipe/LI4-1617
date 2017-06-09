using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PickaPratoServer.Controllers
{
    public class PesquisaController : ApiController
    {
        // GET: api/Pesquisa
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Pesquisa/5
        [Route("api/pesquisa/{id}/{pref}")]
        public string Get(string id, int pref){
            return "value";
        }

        // POST: api/Pesquisa
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Pesquisa/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Pesquisa/5
        public void Delete(int id)
        {
        }
    }
}
