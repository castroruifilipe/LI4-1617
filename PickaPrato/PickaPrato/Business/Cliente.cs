using System;

using SQLite;


namespace PickaPrato.Business {
    
    public class Cliente {

        [PrimaryKey, AutoIncrement]
        public String Username { set; get; }
        public String Password { set; get; }
        public String Foto { set; get; }


        public Cliente(String Username, String Password, String Foto) {
            this.Username = Username;
            this.Password = Password;
            this.Foto = Foto;
		}

		public Cliente(String Username, String Password) {
			this.Username = Username;
			this.Password = Password;
		}

        public override string ToString() {
            return string.Format("[Cliente: Username={0}, Password={1}, Foto={2}]", Username, Password, Foto);
        }
    }

}