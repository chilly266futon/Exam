using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

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
        /// <param name="secretKey"></param>
        /// <param name="_login"> Возвращает логин, заданный пользоватлем</param>
        /// <param name="_password">Возвращает пароль, заданный пользователем</param>
        public static void SignUp(ushort secretKey, out string _login, out string _password)
        {
            Console.Clear();
            Console.WriteLine("Регистрация");
            Console.Write("Введите логин: ");
            var curLogin = Console.ReadLine();
            string curPassword = null;
            bool isSignedUp = false;
            bool isSuitable = false;
            
            string encryptedLogin = "";
            string encryptedPassword = "";

            while (!isSignedUp)
            {
                if (IsNotEmpty(curLogin))
                {
                    if (!IsExist(curLogin, secretKey))
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

                                encryptedLogin = EncodeDecrypt(curLogin, secretKey);
                                encryptedPassword = EncodeDecrypt(curPassword, secretKey);
                                
                                File.AppendAllText(
                                    "C:\\Users\\kiril\\Downloads\\PersonalApp\\Personal\\Authentication_\\userpas.csv",
                                    encryptedLogin + "," + encryptedPassword + "\n");
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
        /// <param name="secretKey"></param>
        /// <returns>true - если пользователь с таким логином существует, false - если не существует</returns>
        private static bool IsExist(string login, ushort secretKey)
        {
            using StreamReader sr = new StreamReader(
                "C:\\Users\\kiril\\Downloads\\PersonalApp\\Personal\\Authentication_\\userpas.csv");
            while (sr.ReadLine() is { } line)
            {
                var str = line.Split(',');
                for (var i = 0; i < str.Length; i += 2)
                {
                    var decryptedLogin = EncodeDecrypt(str[i], secretKey);
                    if (decryptedLogin == login)
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
        /// <param name="secretKey"></param>
        public void Auth(ushort secretKey)
        {
            Console.Clear();
            Console.WriteLine("Вход");
            Console.Write("Введите логин: ");
            var _login = Console.ReadLine();
            while (true)
            {
                if (!IsSuitableLoginForAuth(_login, secretKey))
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
                    if (!IsSuitablePasswordForAuth(_login, _password, secretKey))
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


        private static bool IsSuitableLoginForAuth(string _login, ushort secretKey)
        {
            return IsNotEmpty(_login) && IsExist(_login, secretKey);
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

        private static bool IsSuitablePasswordForAuth(string _login, string _password, ushort secretKey)
        {
            using StreamReader sr = new StreamReader(
                "C:\\Users\\kiril\\Downloads\\PersonalApp\\Personal\\Authentication_\\userpas.csv");
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                var str = line.Split(',');
                for (var i = 0; i < str.Length; i += 2)
                {
                    var decryptedLogin = EncodeDecrypt(str[i], secretKey);
                    var secryptedPassword = EncodeDecrypt(str[i + 1], secretKey);
                    if (decryptedLogin != _login) continue;
                    if (secryptedPassword == _password)
                    {
                        return true;
                    }
                }
            }
            sr.Close();
            return false;
        }


        public static string EncodeDecrypt(string str, ushort secretKey)
        {
            var ch = str.ToArray();
            string newStr = "";
            foreach (var c in ch)
            {
                newStr += TopSecret(c, secretKey);
            }

            return newStr;
        }


        public static char TopSecret(char character, ushort secretKey)
        {
            character = (char)(character ^ secretKey);
            return character;
        }

    }
}
