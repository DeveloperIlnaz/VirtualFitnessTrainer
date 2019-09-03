using System;
using System.Linq;
using System.Threading;
using System.Resources;
using System.Globalization;
using System.Collections.Generic;
using VirtualFitnessTrainer.MVC.Models;
using VirtualFitnessTrainer.MVC.Controllers;

namespace VirtualFitnessTrainer.CMD
{
    internal class Program
    {
        private static CultureInfo cultureInfo;
        private static ResourceManager resourceManager = new ResourceManager("VirtualFitnessTrainer.CMD.Languages.Language", typeof(Program).Assembly);

        private static UserController userController = new UserController();
        private static ExerciseController exerciseController = new ExerciseController();

        private static string GetLanguageItemValue(string name)
        {
            return resourceManager.GetString(name, cultureInfo);
        }
        private static void ChooseLanguage()
        {
            string language = "";

            while (true)
            {
                Console.WriteLine("[Languages]");

                Console.WriteLine("E - English");
                Console.WriteLine("R - Russian");

                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);

                Console.Clear();

                switch (consoleKeyInfo.Key)
                {
                    case ConsoleKey.E:
                        language = "en-US";
                        break;
                    case ConsoleKey.R:
                        language = "ru-RU";
                        break;
                    default:
                        continue;
                }

                cultureInfo = CultureInfo.CreateSpecificCulture(language);

                break;
            }
        }
        private static void StartMenu(out User user, int endTime = 500)
        {
            while (true)
            {
                Console.WriteLine(GetLanguageItemValue("SMTitle"));

                Console.WriteLine($"R - {GetLanguageItemValue("SMItem1")}");
                Console.WriteLine($"A - {GetLanguageItemValue("SMItem2")}");

                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();

                Console.Clear();

                switch (consoleKeyInfo.Key)
                {
                    case ConsoleKey.R:
                        user = UserRegistration();
                        break;
                    case ConsoleKey.A:
                        user = UserAuthorization();
                        break;
                    default:
                        continue;
                }

                Thread.Sleep(endTime);
                Console.Clear();

                break;
            }
        }
        private static void MainMenu(User user)
        {
            while (true)
            {
                Console.WriteLine("[Главное меню]");

                Console.WriteLine("E - Меню упражнений");

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
        private static void ExerciseMenu(User user)
        {
            while (true)
            {
                Console.WriteLine("[Меню упражнений]");

                Console.WriteLine("S - Показать упражнения");
                Console.WriteLine("A - Добавить упражнения");
                Console.WriteLine("R - Удалить упражнения");
                Console.WriteLine("B - Назад (в главный меню)");

                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);

                Console.Clear();

                ExerciseType exerciseType;

                switch (consoleKeyInfo.Key)
                {
                    case ConsoleKey.S:
                        exerciseType = ChooseExerciseType();
                        ShowUserExercises(user, exerciseType);
                        break;
                    case ConsoleKey.A:
                        AddUserExercise(user);
                        break;
                    case ConsoleKey.R:
                        exerciseType = ChooseExerciseType();
                        RemoveUserExercise(user, exerciseType);
                        break;
                    case ConsoleKey.B:
                        MainMenu(user);
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

            Console.WriteLine("[Регистрация]");

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

            Console.WriteLine("[Авторизация]");

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
        private static ExerciseType ChooseExerciseType()
        {
            ExerciseType exerciseType;

            while (true)
            {
                Console.WriteLine("[Тип]");

                Console.WriteLine("A - За все время");
                Console.WriteLine("F - За сегодня");

                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);

                Console.Clear();

                switch (consoleKeyInfo.Key)
                {
                    case ConsoleKey.A:
                        exerciseType = ExerciseType.ForAllTime;
                        break;
                    case ConsoleKey.F:
                        exerciseType = ExerciseType.ForToday;
                        break;
                    default:
                        continue;
                }

                return exerciseType;
            }
        }
        private static bool ShowUserExercises(User user, ExerciseType exerciseType)
        {
            List<Exercise> exercises = exerciseController.GetUserExercises(user, exerciseType);

            Console.WriteLine("[Список упражнений]");

            if (exercises.Count < 1)
            {
                Console.WriteLine("Пусто.");

                return true;
            }

            for (int index = 0; index < exercises.Count; index++)
            {
                int number = index + 1;

                Exercise exercise = exercises[index];

                string exerciseAddedDate = exercise.Added.ToString("d");

                switch (exerciseType)
                {
                    case ExerciseType.ForAllTime:
                        Console.WriteLine($"{number}.{exercise.Name} - {exerciseAddedDate}");
                        break;
                    case ExerciseType.ForToday:
                        Console.WriteLine($"{number}.{exercise.Name}");
                        break;
                }
            }

            Console.WriteLine();

            return false;
        }
        private static Exercise AddUserExercise(User user)
        {
            Console.Write("Введите название:");
            string name = Console.ReadLine();

            Exercise exercise = exerciseController.Add(user, name);

            Console.WriteLine("Упражнения добавлено.");

            return exercise;
        }
        private static void RemoveUserExercise(User user, ExerciseType exerciseType)
        {
            List<Exercise> exercises = exerciseController.GetUserExercises(user, exerciseType);

            bool isEmpty = ShowUserExercises(user, exerciseType);

            if (isEmpty)
            {
                return;
            }

            switch (exerciseType)
            {
                case ExerciseType.ForAllTime:

                    exerciseController.RemoveRange(user, exercises);

                    Console.WriteLine("Все упражнения удаленны.");

                    break;

                case ExerciseType.ForToday:

                    Console.Write("Введите номер:");

                    int number = int.Parse(Console.ReadLine());

                    exerciseController.Remove(user, exercises.ElementAt(number - 1));

                    Console.WriteLine("Упражнения удаленно.");

                    break;
            }
        }
        private static void Main(string[] args)
        {
            Console.Title = "VirtualFitnessTrainer";

            ChooseLanguage();
            StartMenu(out User user);
            MainMenu(user);
        }
    }
}