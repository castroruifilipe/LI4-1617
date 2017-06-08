using PickaPratoServer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace PickaPratoServer.Data
{
    public class PratoDAO
    {
        SqlConnection connection = new SqlConnection("Server=DIOGO-PC\\SQLEXPRESS; Database=PickPrato; Trusted_Connection=True;");
        String vazia= "R0lGODlhkAGQAaIAAJycnOzs7Pr6 + vPz88vLy9vb2 + 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";

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
            if (result.Read()) {
                p.IdPrato = id;
                p.Preco = (Double)result["preco"];
                p.Designacao = (String)result["designacao"];
                p.TipoComida = (String)result["tipoComida"];
                if(result["classificacao"] != System.DBNull.Value)
                    p.Classificacao = (Double)result["classificacao"];
                else { p.Classificacao = 0; }
                if (result["fotografia"] != System.DBNull.Value)
                {
                    p.Fotografia = (String)result["fotografia"];
                }
                else { p.Fotografia = vazia; }
                r.Nome = (String)result["nome"];
                r.Localizacao = (String)result["localizacao"];
                r.Telefone = (String)result["telefone"];
                r.Email = (String)result["email"];

                p.Restaurante = r;
            }

            connection.Close();
            return p;
        }

        public List<Classificacao> GetClassificacoes(int id) {
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = @"
                select *
                from Classificacao C
                join Cliente Cl on C.cliente = Cl.username
                where prato = @idPrato
            ";
            command.Parameters.Add(new SqlParameter("@idPrato", id));

            var result = command.ExecuteReader();
            List<Classificacao> classificacoes = new List<Classificacao>();
            Classificacao c;
            while (result.Read()) {
                c = new Classificacao();
                c.Atribuicao = (int)result["classificacao"];
                c.Comentario = (String)result["comentario"];
                c.Utilizador = (String)result["cliente"];
                c.Foto = (String)result["fotografia"];
                classificacoes.Add(c);
            }
            return classificacoes;
        }

        public int Put(Prato p) {
            int id = 0;
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = @"
                INSERT INTO Prato
                    ([designacao],[tipoComida],[preco],[restaurante],[fotografia])
                VALUES
                (@designacao,@tipoComida,@preco,@restaurante,@fotografia)
            ";
            command.Parameters.Add(new SqlParameter("@designacao", p.Designacao));
            command.Parameters.Add(new SqlParameter("@tipoComida", p.TipoComida));
            command.Parameters.Add(new SqlParameter("@preco", p.Preco));
            command.Parameters.Add(new SqlParameter("@classificacao", p.Classificacao));
            command.Parameters.Add(new SqlParameter("@restaurante", p.Restaurante.Proprietario));
            command.Parameters.Add(new SqlParameter("@fotografia", p.Fotografia));

            var result = command.ExecuteNonQuery();

            command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = @"SELECT MAX(idPrato) as id FROM Prato";

            var result2 = command.ExecuteReader();

            if (result2.Read())
            {
                id = (int)result2["id"];
            }
            connection.Close();
            return id;
        }

        public void PutIngrediente(int id, Ingrediente i)
        {
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = @"
                INSERT INTO Prato_possui_Ingrediente
                ([prato],[ingrediente],[customizavel])
                VALUES
                (@prato,@ingrediente,@customizavel)
                ";
            command.Parameters.Add(new SqlParameter("@prato", id));
            command.Parameters.Add(new SqlParameter("@ingrediente", i.Designacao));
            command.Parameters.Add(new SqlParameter("@customizavel", i.Customizavel));
            command.ExecuteNonQuery();

            connection.Close();
        }

        public void Delete(int id){
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = @"
                DELETE FROM [dbo].[Prato]
                WHERE idPrato=@idPrato
            ";
            command.Parameters.Add(new SqlParameter("@idPrato", id));
            var result = command.ExecuteNonQuery();
        }

        public List<Prato> Pesquisa(string pesquisa){
            List<Prato> lista = new List<Prato>();
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = @"
            SELECT * 
            FROM Prato
            join Restaurante
            on Prato.restaurante = Restaurante.proprietario
            WHERE Prato.designacao LIKE @prato
            ORDER BY classificacao DESC
            ";
            command.Parameters.Add(new SqlParameter("@prato","%"+pesquisa+"%"));
            var result = command.ExecuteReader();
            Prato p;
            Restaurante r;
            while (result.Read())
            {
                p = new Prato();
                r = new Restaurante();
                p.IdPrato = (int)result["idPrato"];
                p.Designacao = (String)result["designacao"];
                if (result["fotografia"] != System.DBNull.Value)
                    p.Fotografia = (String)result["fotografia"];
                else
                {
                    p.Fotografia = vazia;
                }
                r.Nome = (String)result["nome"];
                p.Restaurante = r;
                lista.Add(p);
            }
            connection.Close();
            return lista;
        }

        public void InsereClassificacao(Classificacao c){
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = @"
            INSERT INTO Classificacao
                ([cliente],[prato],[classificacao],[comentario])
            VALUES
                (@cli,@prato,@classificacao,@comentario)
            ";
            command.Parameters.Add(new SqlParameter("@cli", c.Utilizador));
            command.Parameters.Add(new SqlParameter("@prato", c.idPrato));
            command.Parameters.Add(new SqlParameter("@classificacao",c.Atribuicao));
            command.Parameters.Add(new SqlParameter("@comentario", c.Comentario));
            ;

            var result = command.ExecuteNonQuery();

            connection.Close();

        }

    }
}