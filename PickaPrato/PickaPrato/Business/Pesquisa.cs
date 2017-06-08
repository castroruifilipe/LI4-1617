using System;

using SQLite;

namespace PickaPrato.Business {

    public class Pesquisa {

        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
		public string pesquisa { get; set; }


        public Pesquisa(string pesquisa) {
            this.pesquisa = pesquisa;
        }

        public Pesquisa() {
            
        }
    }
}
