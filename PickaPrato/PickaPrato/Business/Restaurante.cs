using System;
using System.Collections.Generic;

namespace PickaPrato.Business {
    
    public class Restaurante {

        public String Proprietario;
        public String Password;
        public String Localizacao;
        public String Telefone;
        public String Email;
        public String Nome;
        public Byte Estado;
        public List<string> Fotografias;


		public Restaurante(String Proprietario, String Password, String Localizacao, String Telefone, String Email,
                           String Nome, List<string> Fotografias) {
            this.Proprietario = Proprietario;
			this.Password = Password;
            this.Localizacao = Localizacao;
            this.Telefone = Telefone;
            this.Email = Email;
            this.Nome = Nome;
            this.Estado = 0;
            this.Fotografias = Fotografias;
		}

        public Restaurante() {
            
        }
    }

}