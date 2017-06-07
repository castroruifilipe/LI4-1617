using System;
using System.Threading.Tasks;

using PickaPrato.Data;
using PickaPrato.Business;
using PickaPrato.Exceptions;
using System.Collections.Generic;

namespace PickaPrato {

    public class Facade {

        private static ServiceEngine server = new ServiceEngine();
        private static UtilizadorDAO Utilizadores;
        public static Cliente atualUserC = null;
        public static Restaurante atualUserP = null;

        public static int IniciarSessao(String Username, String Password) {
            int r = -1;
            Cliente c = server.GetCliente(Username).Result;
            if (c.Username == null) {
                Restaurante p = server.GetRestaurante(Username).Result;
                if (p.Proprietario == null) {
                    r = -2;
                } else {
                    if (p.Password == Password) {
                        if (p.Estado == 1) {
	                        atualUserP = p;
	                        r = 2;
                        } else {
                            r = -3;
                        }
                    }
                }
            } else {
                if (c.Password == Password) {
                    atualUserC = c;
                    r = 1;
                }
            }
            return r;
        }

        public static bool RegistarCliente(string Username, string Password, String Foto) {
            Cliente teste = server.GetCliente(Username).Result;
            if (teste.Username != null) {
                return false;
            }
            Cliente c = new Cliente(Username, Password, Foto);
            Task.Run(() => server.PostCliente(c));
            return true;
        }

		public static bool RegistarRestaurante(String Proprietario, String Password, String Localizacao, String Telefone,
                                               String Email, String Nome, List<string> Fotos) {
            Restaurante teste = server.GetRestaurante(Proprietario).Result;
            if (teste.Proprietario != null) {
                return false;
            }
            Restaurante r = new Restaurante(Proprietario, Password, Localizacao, Telefone, Email, Nome, Fotos);
            Task.Run(() => server.PostRestaurante(r));
            return true;
        }

        public static List<String> GetPreferencias() {
            List<String> preferencias = server.GetPreferencias(atualUserC.Username).Result;
            return preferencias;
        }

        public static List<String> GetIngredientes() {
            List<String> ingredientes = server.GetIngredientes().Result;
            return ingredientes;
        }
		
        public static void EditarPreferencias(List<String> preferencias) {
            Task.Run(() => server.PostPreferencias(atualUserC.Username, preferencias));
        }

		public static void AdicionaPrato(string Designacao, string TipoComida, double Preco, string Fotografia,
					                     List<Ingrediente> Ingredientes) {
            Prato p = new Prato(Designacao, TipoComida, Preco, Fotografia, atualUserP, Ingredientes);
            Task.Run(() => server.PostPrato(p));
        }

        public static List<Prato> PesquisaPrato(string pesquisa, Boolean preferencias) {
            List<Prato> pratos;
            if (preferencias == false) {
                pratos = server.GetPratos(pesquisa, "-1").Result;
            } else {
                pratos = server.GetPratos(pesquisa, atualUserC.Username).Result;
            }
            return pratos;
        }


        public static void EscolhePrato() { }
        public static void GuardaPesquisa() { }
        public static void GuardaPrato() { }
        public static void AvaliaeComenta() { }
    }
}
