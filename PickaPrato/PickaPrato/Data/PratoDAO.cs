using System;
using System.Collections.Generic;
using System.IO;

using PickaPrato.Business;
using SQLite;

namespace PickaPrato.Data {

    public class PratoDAO {

		private string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "db_sqlnet.db");

		
        public void Put(PratoLite p) {
            try {
                var connection = new SQLiteConnection(path);
                connection.Insert(p);
                connection.Close();
            } catch (Exception) {
            }
        }

        public List<Prato> GetPratos() {
            var connection = new SQLiteConnection(path);
            List<Prato> pratos = new List<Prato>();
            var table = connection.Table<PratoLite>();
            foreach (var s in table) {
                Prato p = new Prato();
                p.Designacao = s.Designacao;
                p.Fotografia = s.Fotografia;
                Restaurante r = new Restaurante();
                r.Nome = s.Restaurante;
                p.Restaurante = r;
                pratos.Add(p);
            }
            connection.Close();
            return pratos;
        }
    }
}
