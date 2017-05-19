using System;
namespace PickaPrato.Exceptions {

    public class UtilizadorExistsException : Exception {

        public UtilizadorExistsException() : base("Utilizador já existe") {
        }
    }
}
