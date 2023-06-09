using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Authentication_
{
    internal class User
    {
        /* Описать поля Login, Password, StatusAuth, 
         * реализовать метод Auth для проверки логина и пароля */
        private string login;
        private static string password;
        private bool statusAuth = false;
        
        public User(string login, string password, bool statusAuth)
        {
            this.login = login;
            User.password = password;
            this.statusAuth = statusAuth;
        }

        public User()
        {
        }

        public User(string login, string password)
        {
            this.login = login;
            User.password = password;
        }
        
        public string Login
        {
            get => login;
            set => login = value;
        }

        public string Password
        {
            get => password;
            set => password = value;
        }

        public bool StatusAuth
        {
            get => statusAuth;
            set => statusAuth = value;
        }


        /// <summary>
        /// Проверяет не пустая ли строка
        /// </summary>
        /// <param name="field">проверяемая строка</param>
        /// <returns>true - если строка не пустая, false - если пустая</returns>
        public static bool IsNotmpty(string field)
        {
            if (field == null)
            {
                return false;
            }
            return true;
        }

        public static void SignUp(out string _login, out string _password)
        {
            bool _isExist = false;
            Console.Clear();
            Console.WriteLine("Регистрация");
            Console.Write("Введите логин: ");
            string curLogin = Console.ReadLine();
            string curPassword = null;
            while (!_isExist)
            {
                if (IsNotmpty(curLogin))
                {
                    if (IsNotmpty(curLogin))
                    {
                        if (IsExist(curLogin))
                        {
                            Console.WriteLine("Введенный логин занят. ");
                            _isExist = true;
                            break;
                        }
                        File.AppendAllText("C:\\Users\\kiril\\Downloads\\PersonalApp\\Personal\\Authentication_\\userpas.csv", curLogin + ",");
                        Console.Write("Введите пароль: ");
                        curPassword = Console.ReadLine();
                        if (IsNotmpty(curPassword))
                        {
                            File.AppendAllText("C:\\Users\\kiril\\Downloads\\PersonalApp\\Personal\\Authentication_\\userpas.csv", curPassword);
                        }
                        break;
                    }

                    _isExist = false;
                }

                
            }

            User curUser = new User(curLogin, curPassword);
            _login = curUser.Login;
            _password = curUser.Password;
            Console.WriteLine("Вы успешно зарегистрированы. ");
        }

        public static bool IsExist(string login)
        {
            using (StreamReader sr = new StreamReader("C:\\Users\\kiril\\Downloads\\PersonalApp\\Personal\\Authentication_\\userpas.csv"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] str = line.Split(',');
                    for (int i = 0; i < str.Length; i += 2)
                    {
                        if (str[i] == login)
                        {
                            return true;
                        }
                    }
                    
                }
                sr.Close();
            }
            return false;
        }

        public void Signout()
        {
            // TODO
        }
        

    }
}
