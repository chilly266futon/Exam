using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication_
{
    internal class User
    {
        /* Описать поля Login, Password, StatusAuth, 
         * реализовать метод Auth для проверки логина и пароля */
        private string Login;
        private string Password;
        private bool StatusAuth = false;

        public User(string login, string password, bool statusAuth)
        {
            Login = login;
            Password = password;
            StatusAuth = statusAuth;
        }

        public void Auth(string _login, string _password)
        {

        }

        // public bool IsEmpty(string str)
        // {
        //     if (str is null)
        //     {
        //         
        //     }
        // }

    }
}
