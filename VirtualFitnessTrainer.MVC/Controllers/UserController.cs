using System;
using System.Linq;
using System.Collections.Generic;
using VirtualFitnessTrainer.MVC.Models;

namespace VirtualFitnessTrainer.MVC.Controllers
{
    /// <summary>
    /// Пользовательский контроллер.
    /// </summary>
    public class UserController
    {
        #region Fields
        /// <summary>
        /// Список пользователей.
        /// </summary>
        private List<User> users;
        #endregion

        #region Constructors
        public UserController()
        {
            users = SerializableSaver.Load<User>("Users.dat");
        }
        #endregion

        ~UserController()
        {
            SerializableSaver.Save("Users.dat", users);
        }

        #region Method
        /// <summary>
        /// Получает всех пользователей.
        /// </summary>
        /// <returns>Возвращает список всех пользователей.</returns>
        public List<User> GetUsers()
        {
            return users;
        }
        /// <summary>
        /// Логинет существующего пользователя.
        /// </summary>
        /// <param name="login">Логин.</param>
        /// <param name="password">Пароль.</param>
        /// <returns>Возвращает существующего пользователя.</returns>
        public User LogIn(string login, string password)
        {
            if (!users.Any(u => u.Login.Equals(login)))
            {
                throw new ArgumentException("Не верный логин.", nameof(login));
            }

            if (!users.Any(u => u.Password.Equals(password)))
            {
                throw new ArgumentException("Не верный пароль.", nameof(password));
            }

            User user = users.Single(u => u.Login.Equals(login));

            return user;
        }
        /// <summary>
        /// Добавляет новго пользователя (основной).
        /// </summary>
        /// <param name="login">Логин.</param>
        /// <param name="password">Пароль.</param>
        /// <returns>Возвращает нового пользователя.</returns>
        ///
        public User Add(string login, string password)
        {
            if (users.Any(u => u.Login.Equals(login)))
            {
                throw new ArgumentException("Данный логин занят другим пользователем.");
            }

            User user = new User(login, password);

            users.Add(user);

            return user;
        }
        /// <summary>
        /// Добавляет новго пользователя (полный).
        /// </summary>
        /// <param name="login">Логин.</param>
        /// <param name="password">Пароль.</param>
        /// <param name="age">Возраст.</param>
        /// <param name="height">Рост.</param>
        /// <param name="weight">Вес.</param>
        /// <returns>Возвращает нового пользователя.</returns>
        public User Add(string login, string password, int age, double height, double weight)
        {
            if (users.Any(u => u.Login.Equals(login)))
            {
                throw new ArgumentException("Данный логин занят другим пользователем.");
            }

            User user = new User(login, password, age, height, weight);

            users.Add(user);

            SerializableSaver.Save("Users.dat", users);

            return user;
        }
        /// <summary>
        /// Удаляет существующего пользователя.
        /// </summary>
        /// <param name="login">Логин.</param>
        public void Remove(string login)
        {
            if (!users.Any(u => u.Login.Equals(login)))
            {
                throw new ArgumentException("Некорректный логин.", nameof(login));
            }

            User user = users.Single(u => u.Login.Equals(login));

            users.Remove(user);
        }
        #endregion
    }
}