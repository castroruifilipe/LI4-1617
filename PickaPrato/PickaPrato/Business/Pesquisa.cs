using System;

using SQLite;

namespace PickaPrato.Business {

    public class Pesquisa {

        [PrimaryKey]
		public string pesquisa { get; set; }


        public Pesquisa(string pesquisa) {
            this.pesquisa = pesquisa;
        }

        public Pesquisa() {
            
        }
    }
}
