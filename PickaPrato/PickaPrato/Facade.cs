using System;
using System.Threading.Tasks;
using PickaPrato.Data;
using PickaPrato.Business;

namespace PickaPrato {

    public class Facade {

        private static ServiceEngine server = new ServiceEngine();

        public static void RegistarCliente(string Username, string Password, String Foto) {
            Cliente c = new Cliente(Username, Password, Foto);
            Task.Run(() => server.PostCliente(c));
        }
    }
}
