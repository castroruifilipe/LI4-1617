using System;
namespace PickaPrato.Business {

    public class Classificacao {

        public string Comentario { get; set; }
        public int Atribuicao { get; set; }
        public string Utilizador { get; set; }
        public string Foto { get; set; }
        public int idPrato { get; set; }


        public Classificacao(string Comentario, int Atribuicao, string Utilizador, string Foto, int idPrato) {
            this.Comentario = Comentario;
            this.Atribuicao = Atribuicao;
            this.Utilizador = Utilizador;
            this.Foto = Foto;
            this.idPrato = idPrato;
        }

        public Classificacao() {
            
        }
    }
}
