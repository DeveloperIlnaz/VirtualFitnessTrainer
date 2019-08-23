using System;

namespace VirtualFitnessTrainer.MVC.Model.UserComponents
{
    /// <summary>
    /// Пол.
    /// </summary>
    public class Gender
    {
        #region Fields
        /// <summary>
        /// Название.
        /// </summary>
        private string name;
        #endregion

        #region Properties
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
                    throw new ArgumentException("Название не может быть null или пустым.", nameof(name));
                }

                name = value;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Создает пол.
        /// </summary>
        /// <param name="name">Назвние.</param>
        public Gender(string name)
        {
            Name = name;
        }
        #endregion
    }
}