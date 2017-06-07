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
    public class PratoController : ApiController
    {
        private PratoDAO pratos = new PratoDAO();
        private PreferenciaDAO prefs = new PreferenciaDAO();
        private IngredienteDAO ings = new IngredienteDAO();


        // GET: api/Prato
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [Route("api/Prato/{pesquisa}/{user}")]
        public List<Prato> Get(string pesquisa, String user)
        {
            List<Prato> lista;
            List<Prato> devolve = new List<Prato>();
            lista = pratos.Pesquisa(pesquisa);
            if (!user.Equals("NO"))
            {
                List<String> preferencias = prefs.Get(user);
                foreach(Prato p in lista)
                {
                    if (ings.TestaIngredientes(p.IdPrato, preferencias) == true)
                    {
                        devolve.Add(p);
                    }
                }
            }
            return devolve;

        }


        // GET: api/Prato/5
        public Prato Get(int id)
        {
            Prato p = pratos.Get(id);
            return p;
        }

        // POST: api/Prato
        public void Post([FromBody]Prato value)
        {
            int id=pratos.Put(value);
            foreach(Ingrediente i in value.Ingredientes)
            {
                pratos.PutIngrediente(id, i);
            }
            
        }

        // PUT: api/Prato/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Prato/5
        public void Delete(List<int> id)
        {
            foreach (int j in id)
            {
                pratos.Delete(j);
            }
        }
    }
}
