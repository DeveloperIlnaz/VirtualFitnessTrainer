using System;
using System.Linq;
using System.Collections.Generic;
using VirtualFitnessTrainer.MVC.Model;

namespace VirtualFitnessTrainer.MVC.Controller
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
        private static List<User> users;
        #endregion

        #region Properties
        /// <summary>
        /// Список пользователей.
        /// </summary>
        public List<User> Users
        {
            get
            {
                return users;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Инициализиует пользовательский контроллер.
        /// </summary>
        public UserController()
        {
            users = SerializableController.Load<User>("Users.bin");
        }
        #endregion

        #region Methods
        /// <summary>
        /// Сохраняет данные пользователей.
        /// </summary>
        public void SaveUserData()
        {
            SerializableController.Save("Users.bin", users);
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