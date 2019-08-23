using System;

namespace VirtualFitnessTrainer.MVC.Model
{
    [Serializable]
    /// <summary>
    /// Пользователь.
    /// </summary>
    public class User
    {
        #region Constants
        /// <summary>
        /// Минимальная длина логина.
        /// </summary>
        public const int MinLoginLength = 4;
        /// <summary>
        /// Максимальная длина логина.
        /// </summary>
        public const int MaxLoginLength = 16;

        /// <summary>
        /// Минимальная длина пароля.
        /// </summary>
        public const int MinPasswordLength = 8;
        /// <summary>
        /// Максимальная длина пароля.
        /// </summary>
        public const int MaxPasswordLength = 32;

        /// <summary>
        /// Минимальное значение возраста.
        /// </summary>
        public const int MinAge = 1;
        /// <summary>
        /// Максимальное значение возраста.
        /// </summary>
        public const int MaxAge = 100;

        /// <summary>
        /// Минимальное значение роста.
        /// </summary>
        public const double MinHeight = 1.0;
        /// <summary>
        /// Максимальное значение роста.
        /// </summary>
        public const double MaxHeight = 2.0;

        /// <summary>
        /// Минимальное значение веса.
        /// </summary>
        public const double MinWeight = 1.0;
        /// <summary>
        /// Максимальное значение веса.
        /// </summary>
        public const double MaxWeight = 200.0;
        #endregion

        #region Fields
        /// <summary>
        /// Логин.
        /// </summary>
        private string login;
        /// <summary>
        /// Пароль.
        /// </summary>
        private string password;

        /// <summary>
        /// Возраст.
        /// </summary>
        private int age = MinAge;
        /// <summary>
        /// Рост.
        /// </summary>
        private double height = MinHeight;
        /// <summary>
        /// Вес.
        /// </summary>
        private double weight = MinWeight;
        #endregion

        #region Properties
        /// <summary>
        /// Логин.
        /// </summary>
        public string Login
        {
            get
            {
                return login;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Логин не может быть null или пустой.", nameof(login));
                }

                if (value.Length < MinLoginLength || value.Length > MaxLoginLength)
                {
                    throw new ArgumentException($"Длина логина не может быть меньше {MinLoginLength} или больше {MaxLoginLength}.", nameof(login));
                }

                login = value;
            }
        }
        /// <summary>
        /// Пароль.
        /// </summary>
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Пароль не может быть null или пустой.", nameof(password));
                }

                if (value.Length < MinPasswordLength || value.Length > MaxPasswordLength)
                {
                    throw new ArgumentException($"Длина пароля не может быть меньше {MinPasswordLength} или больше {MaxPasswordLength}.", nameof(password));
                }

                password = value;
            }
        }
        /// <summary>
        /// Возраст.
        /// </summary>
        public int Age
        {
            get
            {
                return age;
            }
            set
            {
                if (value < MinAge || value > MaxAge)
                {
                    throw new ArgumentException($"Возраст не может быть меньше {MinAge} и больше {MaxAge}.", nameof(age));
                }

                age = value;
            }
        }
        /// <summary>
        /// Рост.
        /// </summary>
        public double Height
        {
            get
            {
                return height;
            }
            set
            {
                if (value < MinHeight || value > MaxHeight)
                {
                    throw new ArgumentException($"Рост не может быть меньше {MinHeight} и больше {MaxHeight}.", nameof(height));
                }

                height = value;
            }
        }
        /// <summary>
        /// Вес.
        /// </summary>
        public double Weight
        {
            get
            {
                return weight;
            }
            set
            {
                if (value < MinWeight || value > MaxWeight)
                {
                    throw new ArgumentException($"Вес не может быть меньше {MinWeight} и больше {MaxWeight}.", nameof(weight));
                }

                weight = value;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Создает объект пользователя.
        /// </summary>
        /// <param name="login">Логин.</param>
        /// <param name="password">Пароль.</param>
        public User(string login, string password)
        {
            Login = login;
            Password = password;
        }
        /// <summary>
        /// Создает объект пользователя.
        /// </summary>
        /// <param name="login">Логин.</param>
        /// <param name="password">Пароль.</param>
        /// <param name="age">Возраст.</param>
        /// <param name="height">Рост.</param>
        /// <param name="weight">Вес.</param>
        public User(string login, string password, int age, double height, double weight) : this(login, password)
        {
            Age = age;
            Height = height;
            Weight = weight;
        }
        #endregion
    }
}