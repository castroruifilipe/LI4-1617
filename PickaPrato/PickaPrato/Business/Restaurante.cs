using System;

namespace PickaPrato.Business {
    
    public class Restaurante {
        
        public String Username { set; get; }
        public String Password { set; get; }


		public Restaurante(String Username, String Password) {
			this.Username = Username;
			this.Password = Password;
		}
    }

}