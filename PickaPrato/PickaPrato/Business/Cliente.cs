﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;

namespace PickaPrato.Business {
    
    public class Cliente {
        
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
    }

}