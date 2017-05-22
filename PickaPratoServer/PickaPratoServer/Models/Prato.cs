using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PickaPratoServer.Models
{
    public class Prato
    {
        public int IdPrato { get; set; }
        public String Designacao { get; set; }
        public String TipoComida { get; set; }
        public double Preco { get; set; }
        public double Classificacao { get; set; }
        public String Fotografia { get; set; }
        public Restaurante Restaurante { get; set; }
        public List<Ingrediente> Ingredientes { get; set; }
        public List<Classificacao> Classificacoes { get; set; }
       
    }
}