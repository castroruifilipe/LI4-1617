using PickaPratoServer.Data;
using PickaPratoServer.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
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
        public List<Prato> Get(string pesquisa, String user) {
            String[] partida = Regex.Split(pesquisa," sem ");
            foreach (String s in partida)
            {
                Debug.Print(s);
            }
            List<Prato> lista = pratos.Pesquisa(partida[0]);
            Debug.Print("cheguei aqui");
            List<Prato> aux = new List<Prato>();
            List<Prato> devolve = new List<Prato>();
            if (partida.Count() > 1){
                Debug.Print("entrei");
                Debug.Print(lista.Count.ToString());
                foreach (Prato p in lista)
                {
                    if (ings.PesquisaPratoNoIng(p.IdPrato, partida[1]) == true)
                    {
                        aux.Add(p);
                    }
                }
                Debug.Print(aux.Count.ToString());
                lista = aux;
            }
            if (user.Equals("NO") == false) {
                List<String> preferencias = prefs.Get(user);
                Debug.Print(lista.Count.ToString());

                foreach(Prato p in lista) {
                    if (ings.TestaIngredientes(p.IdPrato, preferencias) == true) {
                        devolve.Add(p);
                    }
                }
                return devolve;
            } else {
                return lista;
            }
        }


        // GET: api/Prato/5
        public Prato Get(int id)
        {
            Prato p = pratos.Get(id);
            List<Classificacao> classificacoes = pratos.GetClassificacoes(id);
            p.Classificacoes = classificacoes;
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

        [Route("api/Prato/comment")]
        public void Post([FromBody]Classificacao classificacao)
        {
            pratos.InsereClassificacao(classificacao);

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
