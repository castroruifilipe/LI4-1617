using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
     
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(IdPrato+"\n");
            sb.Append(Designacao + "\n");
            sb.Append(TipoComida + "\n");
            sb.Append(Preco + "\n");
            sb.Append(Classificacao + "\n");
            sb.Append(Fotografia + "\n");
            sb.Append(Restaurante.Proprietario + "\n");
            foreach (Ingrediente i in Ingredientes)
            {
                sb.Append(i.Designacao+"\n");
                sb.Append(i.Customizavel+"\n");
            }
            foreach (Classificacao c in Classificacoes)
            {
                sb.Append(c.Atribuicao + "\n");
                sb.Append(c.Comentario + "\n");
            }
            return sb.ToString();

        } 

    }
}