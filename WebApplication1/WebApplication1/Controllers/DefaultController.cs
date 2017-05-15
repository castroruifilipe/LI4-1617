using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class DefaultController : ApiController
    {
        // GET: api/Default
        public IEnumerable<string> Get()
        {
            return new string[] { "Person1", "Person2" };
        }

        // GET: api/Default/5
        public Person Get(String id)
        {
            Person person = new Person();
            person.Username = "Alves";
            person.Password = "Joao";
            return person;
        }

        // POST: api/Default
        public void Post([FromBody]Person value)
        {
            Console.Out.WriteLine("Valores lidos: " + value.Username + value.Password);
        }

        // PUT: api/Default/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Default/5
        public void Delete(int id)
        {
        }
    }
}
