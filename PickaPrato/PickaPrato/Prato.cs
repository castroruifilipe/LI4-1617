using System;

namespace PickaPrato {
    
    public class Prato {

        public int idPrato;
        public string designacao;
        public int photo;


        public Prato(int idPrato, string designacao) {
            this.idPrato = idPrato;
            this.designacao = designacao;
            this.photo = Resource.Drawable.Francesinha1;
        }
    }
}
