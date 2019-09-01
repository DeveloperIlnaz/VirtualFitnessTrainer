using System;
using System.Collections.Generic;
using System.Linq;
using VirtualFitnessTrainer.MVC.Models;

namespace VirtualFitnessTrainer.MVC.Controllers
{
    public enum ExerciseShowType
    {
        /// <summary>
        /// За все время.
        /// </summary>
        ForAllTime = 1,
        /// <summary>
        /// За сегодня.
        /// </summary>
        ForToday = 2
    }

    /// <summary>
    /// Контроллер упражнений.
    /// </summary>
    public class ExerciseController
    {
        #region Fields
        /// <summary>
        /// Список упражнений.
        /// </summary>
        private List<Exercise> exercises;
        #endregion

        #region Constructors
        /// <summary>
        /// Создает контроллер упражнений.
        /// </summary>
        public ExerciseController()
        {
            exercises = SerializableSaver.Load<Exercise>("Exercises.dat");
        }
        #endregion

        ~ExerciseController()
        {
            SerializableSaver.Save("Exercises.dat", exercises);
        }

        #region Methods
        /// <summary>
        /// Получает все упражнения.
        /// </summary>
        /// <returns>Возвращает список всех упражнений.</returns>
        public List<Exercise> GetExercises()
        {
            return exercises;
        }
        /// <summary>
        /// Получает все упражнения пользователя.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        /// <param name="type">Тип сортировки упражнений.</param>
        /// <returns>Возвращает список упражнений пользователя (по типу сортировки).</returns>
        public List<Exercise> GetUserExercises(User user, ExerciseShowType type)
        {
            List<Exercise> exercises = new List<Exercise>();

            switch (type)
            {
                case ExerciseShowType.ForAllTime:
                    exercises.AddRange(this.exercises.Where(e => e.User.Equals(user)));
                    break;
                case ExerciseShowType.ForToday:
                    exercises.AddRange(this.exercises.Where(e => e.User.Equals(user) && e.Added.Date.Equals(DateTime.Now.Date)));
                    break;
            }

            return exercises;
        }
        /// <summary>
        /// Добавить упражнение пользователю.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        /// <param name="name">Название упражнения.</param>
        /// <returns>Возвращает добавленное упражнения пользователю.</returns>
        public Exercise Add(User user, string name)
        {
            if (user is null)
            {
                throw new ArgumentNullException("Пользователь не может быть null.");
            }

            if (exercises.Any(e => e.User.Equals(user) && e.Name.Equals(name) && e.Added.Date.Equals(DateTime.Now.Date)))
            {
                throw new ArgumentException("");
            }

            Exercise exercise = new Exercise(user, name);

            exercises.Add(exercise);

            SerializableSaver.Save("Exercises.dat", exercises);

            return exercise;
        }
        /// <summary>
        /// Удаляет упражнение у пользователя.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        /// <param name="exercise">Упражнения.</param>
        public void Remove(User user, Exercise exercise)
        {
            if (!exercises.Any(e => e.User.Equals(user) && e.Name.Equals(exercise.Name)))
            {
                throw new ArgumentException("Упражнения с данным название не найдено (пользователь).");
            }

            exercises.Remove(exercise);

            SerializableSaver.Save("Exercises.dat", exercises);
        }
        /// <summary>
        /// Удаляет упражнение у пользователя (List).
        /// </summary>
        /// <param name="user">Пользователь.</param>
        /// <param name="exercise">Упражнения.</param>
        public void RemoveRange(User user, List<Exercise> exercises)
        {
            foreach (Exercise exercise in exercises)
            {
                if (!exercises.Any(e => e.User.Equals(user) && e.Name.Equals(exercise.Name)))
                {
                    throw new ArgumentException("Упражнения с данным название не найдено (пользователь).");
                }

                this.exercises.Remove(exercise);
            }

            SerializableSaver.Save("Exercises.dat", exercises);
        }
        #endregion
    }
}