using System;

namespace VirtualFitnessTrainer.MVC.Models
{
    [Serializable]
    /// <summary>
    /// Упражнение.
    /// </summary>
    public class Exercise
    {
        #region Fields
        /// <summary>
        /// Название.
        /// </summary>
        private string name;
        #endregion

        #region Properties
        /// <summary>
        /// Пользователь.
        /// </summary>
        public User User { get; }
        /// <summary>
        /// Название.
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Название не может быть null или пыстым.", nameof(name));
                }

                name = value;
            }
        }
        /// <summary>
        /// Дата и время добавления.
        /// </summary>
        public DateTime Added { get; }
        #endregion
        
        #region Constructors
        /// <summary>
        /// Создает упражнение.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        /// <param name="name">Название.</param>
        public Exercise(User user, string name)
        {
            User = user;
            Name = name;
            Added = DateTime.Now;
        }
        #endregion
    }
}