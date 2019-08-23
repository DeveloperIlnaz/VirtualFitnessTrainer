using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

namespace VirtualFitnessTrainer.MVC.Controller
{
    /// <summary>
    /// Сериализуемый контроллер.
    /// </summary>
    public static class SerializableController
    {
        #region Methods
        /// <summary>
        /// Выгружает (десериализует) данные.
        /// </summary>
        /// <typeparam name="TItems">Тип элментов.</typeparam>
        /// <param name="filePath">Путь файла.</param>
        /// <returns>Если true то, возвращает все элменты типа, иначе пустой список.</returns>
        public static List<TItems> Load<TItems>(string filePath)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                if (fileStream.Length > 0 && binaryFormatter.Deserialize(fileStream) is List<TItems> items)
                {
                    return items;
                }
                else
                {
                    return new List<TItems>();
                }
            }
        }
        /// <summary>
        /// Сохраняет (сериализует) данные.
        /// </summary>
        /// <typeparam name="TItems">Тип элементов.</typeparam>
        /// <param name="filePath">Путь файла.</param>
        /// <param name="items">Элементы.</param>
        public static void Save<TItems>(string filePath, List<TItems> items)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                binaryFormatter.Serialize(fileStream, items);
            }
        }
        #endregion
    }
}