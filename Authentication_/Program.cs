using System;
using System.Collections.Generic;
using System.IO;

namespace Authentication_
{
    /// <summary>
    /// Укажите автора программы
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            /* Реализовать пользовательский интерфейс 
             * для работы с объектами по заданию вашего варианта. 
             
            Порядок работы программы:
            1. Вывод меню: Авторизация (выдает сообщение: "логин или пароль неверны", "авторизация успешна")
            2. Работа с вариантом после авторизации

            Создавать экземпляры класса может только пользователь со статусом StatusAuth = True
            */

            Console.WriteLine("Автор программы: Кирилл Рогачев");
            Console.WriteLine("Вариант: __");
            Console.WriteLine("Название предметной области варианта");

            // Пишем код здесь.
            // Console.WriteLine("Для демонстрации программы необходимо авторизоваться"); // слабакам можно обойтись без этого
            // Console.Write("Введите логин:");

            bool infinity = true;
            string user_command;

            ushort secretKey = 0x0088; // Секретный ключ

            User currentUser = new User();
            List<User> users = new List<User>();
            
            using StreamReader sr = new StreamReader(
                "C:\\Users\\kiril\\Downloads\\PersonalApp\\Personal\\Authentication_\\userpas.csv");
            while (sr.ReadLine() is { } line)
            {
                var str = line.Split(',');
                for (var i = 0; i < str.Length; i += 2)
                {
                    var decryptedLogin = User.EncodeDecrypt(str[i], secretKey);
                    var decryptedPassword = User.EncodeDecrypt(str[i++], secretKey);
                    users.Add(new User(decryptedLogin, decryptedPassword));
                }
            }
            sr.Close();
            
            
            Console.WriteLine("Меню: ");
            Console.WriteLine("---");
            
            Console.WriteLine("signup - Регистрация. ");
            Console.WriteLine("login - Вход. ");
            Console.WriteLine("signout - Деаутентификация. ");
            Console.WriteLine("exit - Завершить программу. ");
            

            while (infinity)
            {
                Console.Write("Введите команду: ");
                user_command = Console.ReadLine();
                
                switch (user_command)
                {
                    case "signup":
                    {
                        currentUser.StatusAuth = false;

                        string login;
                        string password;
                        User.SignUp(secretKey, out login, out password);

                        currentUser = new User(login, password);
                        users.Add(currentUser);
                        break;
                    }
                    case "login":
                    {
                        currentUser.StatusAuth = false;
                        currentUser.Auth(secretKey);
                        break;
                    }

                    case "signout":
                    {
                        currentUser.StatusAuth = false;
                        Console.WriteLine("Деаутентификация прошла успешно.");
                        break;
                    }

                    case "exit":
                    {
                        infinity = false;
                        break;
                    }

                    case "menu":
                    {
                        Console.WriteLine("Меню:");
                        Console.WriteLine("---");

                        Console.WriteLine("signup - Регистрация. ");
                        Console.WriteLine("login - Вход. ");
                        Console.WriteLine("signout - Деаутентификация. ");
                        Console.WriteLine("exit - Завершить программу. ");
                        Console.Write("Введите команду: ");
                        break;
                    }
                    default:
                    {
                        Console.WriteLine("Ваша команда не определена, пожалуйста, повторите");
                        Console.WriteLine("Для возвращения в меню введите команду menu");
                        Console.WriteLine("Для завершения программы введите команду exit");
                        break;
                    }
                }



            }
            

            // Console.ReadKey();
        }
    }
}
