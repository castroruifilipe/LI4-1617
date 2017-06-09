using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PickaPratoServer.Models
{
    public class Restaurante
    {
        public String Proprietario { get; set; }
        public String Password { get; set; }
        public String Localizacao { get; set; }
        public String Telefone { get; set; }
        public String Email { get; set; }
        public String Nome { get; set; }
        public Byte Estado { get; set; }
        public List<String> Fotografias { get; set; }
    }
}