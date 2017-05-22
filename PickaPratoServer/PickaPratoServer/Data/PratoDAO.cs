using PickaPratoServer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PickaPratoServer.Data
{
    public class PratoDAO
    {
        SqlConnection connection = new SqlConnection("Server=DIOGO-PC\\SQLEXPRESS; Database=PickPrato; Trusted_Connection=True;");

        public Prato Get(int id) {
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = @"
                select * from Prato
                join Restaurante
                on Prato.restaurante = Restaurante.proprietario
                where idPrato = @id
            ";
            command.Parameters.Add(new SqlParameter("@id", id));
            var result = command.ExecuteReader();
            Prato p = new Prato();
            Restaurante r = new Restaurante();
            if (result.Read())
            {
                p.IdPrato = id;
                p.Preco = (Double)result["preco"];
                p.Designacao = (String)result["designacao"];
                p.TipoComida = (String)result["tipoComida"];
                if(result["classificacao"] != System.DBNull.Value)
                    p.Classificacao = (Double)result["classificacao"];
                if (result["fotografia"] != System.DBNull.Value)
                    p.Classificacao = (Double)result["fotografia"];
                r.Nome = (String)result["nome"];
                r.Localizacao = (String)result["localizacao"];
                r.Telefone = (String)result["telefone"];
                r.Email = (String)result["email"];
                r.Proprietario = (String)result["proprietario"];
            }

            result.Close();
            
            // Buscar Fotografias
            command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = @"
                select *
                from Fotografia
                where restaurante = @idRestaurante
            ";
            command.Parameters.Add(new SqlParameter("@idRestaurante",r.Proprietario));
            result = command.ExecuteReader();
            List<String> fotos = new List<string>();
            while (result.Read())
            {
                fotos.Add((String)result["fotografia"]);
            }
            r.Fotografias = fotos;
            p.Restaurante = r;

            result.Close();

            // Buscar Classificacoes
            command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = @"
                select *
                from Classificacao
                where prato = @idPrato
            ";
            command.Parameters.Add(new SqlParameter("@idPrato", id));
            result = command.ExecuteReader();
            List<Classificacao> classificacoes = new List<Classificacao>();
            while (result.Read())
            {
                Classificacao c = new Classificacao();
                c.Atribuicao = (int)result["classificacao"];
                c.Comentario = (String)result["comentario"];
                classificacoes.Add(c);
            }
            p.Classificacoes = classificacoes;
            
            connection.Close();
            return p;
        }

        public void Post(Prato p, String restaurante){
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = @"
                INSERT INTO Prato
                    ([designacao],[tipoComida],[preco]
                    ,[restaurante],[fotografia])
                VALUES
                (@designacao,@tipoComida,@preco,
                 @classificacao, @restaurante,@fotografia)
            ";
            command.Parameters.Add(new SqlParameter("@designacao", p.Designacao));
            command.Parameters.Add(new SqlParameter("@tipoComida", p.TipoComida));
            command.Parameters.Add(new SqlParameter("@preco", p.Preco));
            command.Parameters.Add(new SqlParameter("@classificacao", p.Classificacao));
            command.Parameters.Add(new SqlParameter("@restaurante", restaurante));
            command.Parameters.Add(new SqlParameter("@fotografia", p.Fotografia));

            var result = command.ExecuteNonQuery();

            command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = @"SELECT MAX(idPrato) as id FROM Prato";

            var result2 = command.ExecuteReader();
            int id = (int)result2["id"];

            foreach (Ingrediente i in p.Ingredientes){
                command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = @"
                INSERT INTO Prato_possui_Ingrediente
                ([prato] ,[ingrediente],[costumizavel])
                VALUES
                (@prato ,@ingrediente,@costumizavel)
                ";
                command.Parameters.Add(new SqlParameter("@prato", id));
                command.Parameters.Add(new SqlParameter("@ingrediente", i.IdIngrediente));
                command.Parameters.Add(new SqlParameter("@costumizavel", i.Constumizavel));
                result = command.ExecuteNonQuery();
            }

        }

    }
}