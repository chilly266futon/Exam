using System;
using System.IO;

namespace Authentication_
{
    internal class User
    {
        /* Описать поля Login, Password, StatusAuth, 
         * реализовать метод Auth для проверки логина и пароля */
        private string login;
        private string password;
        private bool statusAuth;

        public User(string login, string password)
        {
            this.login = login;
            this.password = password;
        }

        public User()
        {
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
        /// <param name="field">Проверяемая строка</param>
        /// <returns>true - если строка не пустая, false - если пустая</returns>
        private static bool IsNotEmpty(string field)
        {
            return field != null;
        }


        /// <summary>
        /// Регистрирует пользователя в системе
        /// </summary>
        /// <param name="_login"> Возвращает логин, заданный пользоватлем</param>
        /// <param name="_password">Возвращает пароль, заданный пользователем</param>
        public static void SignUp(out string _login, out string _password)
        {
            Console.Clear();
            Console.WriteLine("Регистрация");
            Console.Write("Введите логин: ");
            var curLogin = Console.ReadLine();
            string curPassword = null;
            bool isSignedUp = false;
            bool isSuitable = false;
            
            while (!isSignedUp)
            {
                if (IsNotEmpty(curLogin))
                {
                    if (!IsExist(curLogin))
                    {
                        while (!isSuitable)
                        {
                            Console.Write("Введите пароль: ");
                            curPassword = Console.ReadLine();
                            while (!isSignedUp)
                            {
                                if (!IsSuitablePasswordForSignup(curPassword))
                                {
                                    break; 
                                }
                                Console.Write("Повторите ввод пароля: ");
                                var confirmationPassword = Console.ReadLine();
                                if (!IsNotEmpty(confirmationPassword)) continue;
                                if (curPassword != confirmationPassword)
                                {
                                    Console.WriteLine("Пароли не совпадают. ");
                                    break;
                                }

                                File.AppendAllText(
                                    "C:\\Users\\kiril\\Downloads\\PersonalApp\\Personal\\Authentication_\\userpas.csv",
                                    curLogin + "," + curPassword + "\n");
                                User curUser = new User(curLogin, curPassword);

                                _login = curUser.Login;
                                _password = curUser.Password;
                                Console.WriteLine("Вы успешно зарегистрированы. Для входа введите команду login");
                                isSignedUp = true;
                                isSuitable = true;
                                break;
                            }
                        }
                        continue;
                    }
                }
                
                Console.WriteLine("Введенный логин занят.");
                Console.Write("Введите логин: ");
                curLogin = Console.ReadLine();
            }
            _login = curLogin;
            _password = curPassword;
        }

        
        /// <summary>
        /// Проверяет существует ли пользователь с таким логином
        /// </summary>
        /// <param name="login">Логин</param>
        /// <returns>true - если пользователь с таким логином существует, false - если не существует</returns>
        private static bool IsExist(string login)
        {
            using StreamReader sr = new StreamReader(
                "C:\\Users\\kiril\\Downloads\\PersonalApp\\Personal\\Authentication_\\userpas.csv");
            while (sr.ReadLine() is { } line)
            {
                var str = line.Split(',');
                for (var i = 0; i < str.Length; i += 2)
                {
                    if (str[i] == login)
                    {
                        return true;
                    }
                }
            }
            sr.Close();
            return false;
        }
        
        
        /// <summary>
        /// Аутентификация пользователя
        /// </summary>
        public void Auth()
        {
            Console.Clear();
            Console.WriteLine("Вход");
            Console.Write("Введите логин: ");
            var _login = Console.ReadLine();
            while (true)
            {
                if (!IsSuitableLoginForAuth(_login))
                {
                    Console.WriteLine("Пользователь с таким логином не найден.");
                    Console.Write("Введите логин: ");
                    _login = Console.ReadLine();
                    continue;
                }

                while (true)
                {
                    Console.Write("Введите пароль: ");
                    var _password = Console.ReadLine();
                    if (!IsSuitablePasswordForAuth(_login, _password))
                    {
                        Console.WriteLine("Неверный пароль. Попробуйте снова...");
                        continue;
                    }
                    Console.WriteLine("Авторизация прошла успешно.");
                    statusAuth = true;
                    break;
                }
                break;
            }
        }


        private static bool IsSuitableLoginForAuth(string _login)
        {
            return IsNotEmpty(_login) && IsExist(_login);
        }

        
        private static bool IsSuitablePasswordForSignup(string _password)
        {
            if (!IsNotEmpty(_password)) return false;
            switch (_password.Length)
            {
                case < 8:
                    Console.WriteLine("Пароль слишком короткий. Длина пароля должна быть не менее 8 символов.");
                    return false;
                case <= 20:
                    return true;
                default:
                    Console.WriteLine("Пароль слишком длинный. Длина пароля должна быть не более 20 символов.");
                    return false;
            }
        }

        private static bool IsSuitablePasswordForAuth(string _login, string _password)
        {
            using StreamReader sr = new StreamReader(
                "C:\\Users\\kiril\\Downloads\\PersonalApp\\Personal\\Authentication_\\userpas.csv");
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                var str = line.Split(',');
                for (var i = 0; i < str.Length; i += 2)
                {
                    if (str[i] != _login) continue;
                    if (str[i + 1] == _password)
                    {
                        return true;
                    }
                }
            }
            sr.Close();
            return false;
        }

    }
}
