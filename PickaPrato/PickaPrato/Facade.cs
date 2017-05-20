using System;
using System.Threading.Tasks;

using PickaPrato.Data;
using PickaPrato.Business;
using PickaPrato.Exceptions;

namespace PickaPrato {

    public class Facade {

        private static ServiceEngine server = new ServiceEngine();
        private static UtilizadorDAO Utilizadores;
		public static Cliente atualUserC = null;
		public static Proprietario atualUserP = null;


        public static int IniciarSessao(String Username, String Password) {
            int r = -1;
            Cliente c = server.GetCliente(Username).Result;
            if (c.Username == null) {
                Proprietario p = server.GetProprietario(Username).Result;
                if (p.Username == null) {
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
    }
}
