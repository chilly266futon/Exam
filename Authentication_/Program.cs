using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

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

            Console.WriteLine("Автор программы: Рогачев Кирилл");
            Console.WriteLine("Вариант: __");
            Console.WriteLine("Название предметной области варианта");

            // Пишем код здесь.
            Console.WriteLine("Для демонстрации программы необходимо авторизоваться"); // слабакам можно обойтись без этого
            Console.Write("Введите логин:");

            bool infinity = true;
            string user_command;

            User someUser = null;
            List<User> users = new List<User>();
            

            while (infinity)
            {
                Console.WriteLine("Меню: ");
                Console.WriteLine("1. Регистрация. ");
                Console.WriteLine("2. Вход. ");
                Console.WriteLine("3. Завершить программу. ");
                user_command = Console.ReadLine();
                try
                {
                    byte userCom = Convert.ToByte(user_command);
                    if (userCom > 1 && userCom < 3)
                    {
                        switch (userCom)
                        {
                            case 1:
                            {
                                // Console.Write("Введите логин: ");
                                // string _login = Console.ReadLine();
                                // Console.Write("Введите пароль");
                                
                                break;
                            }
                            case 2:
                            {
                                break;
                            }

                            case 3:
                            {
                                infinity = false;
                                break;
                            }
                        }
                    }
                    

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                


            }
            

            Console.ReadKey();
        }
    }
}
