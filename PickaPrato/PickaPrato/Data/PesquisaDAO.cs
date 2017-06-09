using System;
using System.Collections.Generic;
using System.IO;

using PickaPrato.Business;
using SQLite;


namespace PickaPrato.Data {

    public class PesquisaDAO {

        private string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "db_sqlnet.db");
 

        public List<Pesquisa> GetPesquisas() {
			var connection = new SQLiteConnection(path);
            List<Pesquisa> pesquisas = new List<Pesquisa>();
            var table = connection.Table<Pesquisa>();
            foreach (var s in table) {
                pesquisas.Add(s);
            }
            connection.Close();
            return pesquisas;
		}

        public void Put(Pesquisa p) {
            try {
				var connection = new SQLiteConnection(path);
				connection.Insert(p);
				connection.Close();
            } catch (Exception) {
            }
        }
    }
}
