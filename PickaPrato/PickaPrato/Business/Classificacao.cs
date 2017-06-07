using System;
namespace PickaPrato.Business {

    public class Classificacao {

        public string Comentario { get; set; }
        public int Atribuicao { get; set; }
        public string Utilizador { get; set; }
        public string Foto { get; set; }


        public Classificacao(string Comentario, int Atribuicao, string Utilizador, string Foto) {
            this.Comentario = Comentario;
            this.Atribuicao = Atribuicao;
        }

        public Classificacao() {
            
        }
    }
}
