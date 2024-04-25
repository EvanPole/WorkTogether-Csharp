using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTogether.DB.Class;

namespace WorkTogether.ViewModel
{
    internal class LoginViewModel
    {
        public string? Email { get; set; }

        public string? Password { get; set; }



        /// <summary>
        /// Methode pour vérifier la connexion d'un utilisateur.
        /// </summary>

        public void Login()
        {

        }
    }
}
