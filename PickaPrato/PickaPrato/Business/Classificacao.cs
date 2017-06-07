using System;
namespace PickaPrato.Business {

    public class Classificacao {

        public string Comentario { get; set; }
        public int Atribuicao { get; set; }


        public Classificacao(string Comentario, int Atribuicao) {
            this.Comentario = Comentario;
            this.Atribuicao = Atribuicao;
        }

        public Classificacao() {
            
        }
    }
}
