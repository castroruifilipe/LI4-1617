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
                    throw new UtilizadorExistsException();
                } else {
                    if (p.Password == Password) {
                        atualUserP = p;
                        r = 2;
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

        public static void RegistarCliente(string Username, string Password, String Foto) {
            Cliente c = new Cliente(Username, Password, Foto);
            Task.Run(() => server.PostCliente(c));
        }

		public static void RegistarRestaurante(String Proprietario, String Password, String Localizacao, String Telefone,
                                               String Email, String Nome, List<string> Fotos) {
            Restaurante r = new Restaurante(Proprietario, Password, Localizacao, Telefone, Email, Nome, Fotos);
            Task.Run(() => server.PostRestaurante(r));
        }

        public static List<String> GetPreferencias() {
            List<String> preferencias = server.GetPreferencias(atualUserC.Username).Result;
            return preferencias;
        }

        public static void AdicionaPrato(string Descricao, string[] Fotos, Ingrediente[] Ingredientes, bool[] Customizavel) { }
        public static void EditarPreferencias(){}
        public static void PesquisaPrato() { }
        public static void EscolhePrato() { }
        public static void GuardaPesquisa() { }
        public static void GuardaPrato() { }
        public static void AvaliaeComenta() { }
    }
}
