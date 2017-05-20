using System;

namespace PickaPrato.Business {
    
    public class Proprietario {
        
        public String Username { set; get; }
        public String Password { set; get; }


		public Proprietario(String Username, String Password) {
			this.Username = Username;
			this.Password = Password;
		}
    }

}