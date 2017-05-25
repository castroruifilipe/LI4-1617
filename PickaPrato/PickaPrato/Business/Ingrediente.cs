using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PickaPrato.Business {
    
    public class Ingrediente {
        
        public string Designacao  { get; set; }
        public byte Customizavel  { get; set; }


        public Ingrediente(string Designacao) {
            this.Designacao = Designacao;
        }

        public Ingrediente(string Designacao, bool Customizavel) {
            this.Designacao = Designacao;
            if (Customizavel == true) {
				this.Customizavel = 1;
            } else {
                this.Customizavel = 0;
            }
        }

        public Ingrediente() {
            
        }
    }
}