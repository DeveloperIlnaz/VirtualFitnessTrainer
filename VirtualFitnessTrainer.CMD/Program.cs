using System;
using System.Linq;
using System.Resources;
using System.Globalization;
using VirtualFitnessTrainer.MVC.Models;
using VirtualFitnessTrainer.MVC.Controllers;
using System.Collections.Generic;

namespace VirtualFitnessTrainer.CMD
{
    internal enum MainMenuCommands
    {
        Registration = 1,
        Authorization = 2
    }

    internal class Program
    {
        private static UserController userController = new UserController();
        private static ExerciseController exerciseController = new ExerciseController();

        private static void StartMenu(out User user)
        {
            user = null;

            while (true)
            {
                try
                {
                    Console.WriteLine($"1. Регистрация\n2. Авторизация");

                    Console.Write("Введите номер команды:");

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
        private static void MainMenu(User user)
        {
            while (true)
            {
                Console.WriteLine("E - Меню управления за упражнениями (за сегодня)");

                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);

                Console.Clear();

                switch (consoleKeyInfo.Key)
                {
                    case ConsoleKey.E:
                        ExerciseMenu(user);
                        break;
                }
            }
        }
        private static User UserRegistration()
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
                        Console.Write("Введите логин:");
                        login = Console.ReadLine();
                    }

                    if (password is null)
                    {
                        Console.Write("Введите пароль:");
                        password = Console.ReadLine();
                    }

                    if (age is 0)
                    {
                        Console.Write("Введите возраст:");
                        int.TryParse(Console.ReadLine(), out age);
                    }

                    if (height is 0.0)
                    {
                        Console.Write("Введите рост:");
                        double.TryParse(Console.ReadLine(), out height);
                    }

                    if (weight is 0.0)
                    {
                        Console.Write("Введите вес:");
                        double.TryParse(Console.ReadLine(), out weight);
                    }

                    User user = userController.Add(login, password, age, height, weight);

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
        private static User UserAuthorization()
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
                        Console.Write("Введите логин:");
                        login = Console.ReadLine();
                    }

                    if (password is null)
                    {
                        Console.Write("Введите пароль:");
                        password = Console.ReadLine();
                    }

                    User user = userController.LogIn(login, password);

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
        private static void ExerciseMenu(User user)
        {
            while (true)
            {
                Console.WriteLine("S - Показать все упражнения");
                Console.WriteLine("A - Добавить упражнения");
                Console.WriteLine("R - Удалить упражнения");
                Console.WriteLine("B - Назад (в главный меню)");

                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);

                Console.Clear();

                switch (consoleKeyInfo.Key)
                {
                    case ConsoleKey.S:
                        ShowUserAllExercisesForToday(user);
                        break;
                    case ConsoleKey.A:
                        AddUserExercise(user);
                        break;
                    case ConsoleKey.R:
                        RemoveUserExercise(user);
                        break;
                    case ConsoleKey.B:
                        MainMenu(user);
                        break;
                }
            }
        }
        private static void ShowUserAllExercisesForToday(User user)
        {
            Console.WriteLine("Список всех упражнений");

            List<Exercise> exercises = exerciseController.GetUserExercises(user, ExerciseShowType.ForToday);

            if (exercises.Count < 1)
            {
                Console.WriteLine("Пусто.");

                return;
            }

            for (int index = 0; index < exercises.Count; index++)
            {
                int number = index + 1;
                Exercise exercise = exercises[index];

                Console.WriteLine($"{number}.{exercise.Name}");
            }
        }
        private static Exercise AddUserExercise(User user)
        {
            Console.Write("Введите название:");
            string name = Console.ReadLine();

            Exercise exercise = exerciseController.Add(user, name);

            Console.WriteLine("Упражнения добавлено.");

            return exercise;
        }
        private static void RemoveUserExercise(User user)
        {
            try
            {
                ShowUserAllExercisesForToday(user);

                Console.Write("Введите номер (-1 - удалить все упражнения):");

                int number = int.Parse(Console.ReadLine());

                if (number == -1)
                {
                    List<Exercise> exercises = exerciseController.GetUserExercises(user, ExerciseShowType.ForToday);

                    exerciseController.RemoveRange(user, exercises);

                    Console.WriteLine("Все упражнения удаленны.");
                }
                else
                {
                    Exercise exercise = exerciseController.GetUserExercises(user, ExerciseShowType.ForToday).ElementAt(number - 1);

                    exerciseController.Remove(user, exercise);

                    Console.WriteLine("Упражнения удаленно.");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Ошибка {exception.Message}");
            }
        }
        private static void Main(string[] args)
        {
            StartMenu(out User user);
            MainMenu(user);
        }
    }
}