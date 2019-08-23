using System;
using VirtualFitnessTrainer.MVC.Model;
using VirtualFitnessTrainer.MVC.Controller;

namespace VirtualFitnessTrainer.CMD
{
    enum MainMenuCommands
    {
        Registration = 1,
        Authorization = 2
    }

    class Program
    {
        static UserController userController = new UserController();

        static void MainMenu(out User user)
        {
            user = null;

            while (true)
            {
                try
                {
                    Console.WriteLine("1. Регистрация\n2. Авторизация");

                    Console.Write("Введите номер команды: ");

                    MainMenuCommands mainMenuCommands = (MainMenuCommands)Enum.GetValues(typeof(MainMenuCommands)).GetValue(int.Parse(Console.ReadLine()) - 1);

                    Console.Clear();

                    switch (mainMenuCommands)
                    {
                        case MainMenuCommands.Registration:
                            user = UserRegistration();
                            break;
                        case MainMenuCommands.Authorization:
                            user = UserAuthorization();
                            break;
                    }

                    break;
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"Ошибка {exception.Message}");
                }
            }
        }
        static User UserRegistration()
        {
            string login = null;
            string password = null;
            int age = 0;
            double height = 0.0;
            double weight = 0.0;

            Console.WriteLine("Регистрация");

            while (true)
            {
                try
                {
                    if (login is null)
                    {
                        Console.Write("Введите логин: ");
                        login = Console.ReadLine();
                    }

                    if (password is null)
                    {
                        Console.Write("Введите пароль: ");
                        password = Console.ReadLine();
                    }

                    if (age is 0)
                    {
                        Console.Write("Введите возраст: ");
                        int.TryParse(Console.ReadLine(), out age);
                    }
                    if (height is 0.0)
                    {
                        Console.Write("Введите рост: ");
                        double.TryParse(Console.ReadLine(), out height);
                    }

                    if (weight is 0.0)
                    {
                        Console.Write("Введите вес: ");
                        double.TryParse(Console.ReadLine(), out weight);
                    }

                    User user = userController.Registration(login, password, age, height, weight);

                    Console.WriteLine("Вы успешно зарегистрировались.");

                    return user;
                }
                catch (ArgumentException argumentException)
                {
                    switch (argumentException.ParamName)
                    {
                        case "login":
                            login = null;
                            break;
                        case "password":
                            password = null;
                            break;
                        case "age":
                            age = 0;
                            break;
                        case "height":
                            height = 0.0;
                            break;
                        case "weight":
                            weight = 0.0;
                            break;
                    }

                    Console.WriteLine($"Ошибка {argumentException.Message.Split('.')[0]}.");
                }
            }
        }
        static User UserAuthorization()
        {
            string login = null;
            string password = null;

            Console.WriteLine("Авторизация");

            while (true)
            {
                try
                {
                    if (login is null)
                    {
                        Console.Write("Введите логин: ");
                        login = Console.ReadLine();
                    }

                    if (password is null)
                    {
                        Console.Write("Введите пароль: ");
                        password = Console.ReadLine();
                    }

                    User user = userController.Authorization(login, password);

                    Console.WriteLine("Вы успешно авторизовались.");

                    return user;
                }
                catch (ArgumentException argumentException)
                {
                    switch (argumentException.ParamName)
                    {
                        case "login":
                            login = null;
                            break;
                        case "password":
                            password = null;
                            break;
                    }

                    Console.WriteLine($"Ошибка {argumentException.Message.Split('.')[0]}.");
                }
            }
        }
        static void Main(string[] args)
        {
            MainMenu(out User user);
            
            userController.SaveUserData();
        }
    }
}