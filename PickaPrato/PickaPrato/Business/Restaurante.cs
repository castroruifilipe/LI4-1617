using System;
using PickaPrato.Business;
using System.Collections.Generic;
using SQLite;

namespace PickaPrato.Business {
    
    public class Restaurante {

        public string Proprietario { get; set; }
        public String Password { get; set; }
        public String Localizacao { get; set; }
        public String Telefone { get; set; }
        public String Email { get; set; }
        public string Nome { get; set; }
        public Byte Estado { get; set; }
        public List<string> Fotografias { get; set; }


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