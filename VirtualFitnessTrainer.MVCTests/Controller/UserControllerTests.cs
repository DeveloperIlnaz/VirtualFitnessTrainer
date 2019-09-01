using Microsoft.VisualStudio.TestTools.UnitTesting;
using VirtualFitnessTrainer.MVC.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualFitnessTrainer.MVC.Models;

namespace VirtualFitnessTrainer.MVC.Controllers.Tests
{
    [TestClass()]
    public class UserControllerTests
    {
        [TestMethod()]
        public void SaveUserDataTest()
        {
            // Arrange

            string login = "TestUserLogin";
            string password = "TestUserPassword";
            int age = 20;
            double height = 1.75;
            double weight = 75.0;

            // Act

            UserController userController = new UserController();

            var user = userController.Add(login, password, age, height, weight);

            userController.SaveUserData();

            UserController newUserController = new UserController();

            var newUsers = newUserController.Users.Select(u => u).ToList();

            var newUser = newUsers.Single(u => u.Login.Equals(login));

            newUserController.Remove(newUser.Login);

            newUserController.SaveUserData();

            // Assert

            Assert.AreEqual(login, newUser.Login);
            Assert.AreEqual(password, newUser.Password);
            Assert.AreEqual(age, newUser.Age);
            Assert.AreEqual(height, newUser.Height);
            Assert.AreEqual(weight, newUser.Weight);
        }

        [TestMethod()]
        public void AddTest()
        {
            // Arrange

            string login = "TestUserLogin";
            string password = "TestUserPassword";
            int age = 20;
            double height = 1.75;
            double weight = 75.0;

            // Act

            UserController userController = new UserController();

            var user = userController.Add(login, password, age, height, weight);

            userController.Remove(user.Login);

            userController.SaveUserData();

            // Assert

            Assert.AreEqual(login, user.Login);
            Assert.AreEqual(password, user.Password);
            Assert.AreEqual(age, user.Age);
            Assert.AreEqual(height, user.Height);
            Assert.AreEqual(weight, user.Weight);
        }

        [TestMethod()]
        public void AddTest1()
        {
            // Arrange

            string login = "TestUserLogin";
            string password = "TestUserPassword";
            int age = 20;
            double height = 1.75;
            double weight = 75.0;

            // Act

            UserController userController = new UserController();

            var user = userController.Add(login, password, age, height, weight);

            userController.Remove(user.Login);

            userController.SaveUserData();

            // Assert

            Assert.AreEqual(login, user.Login);
            Assert.AreEqual(password, user.Password);
            Assert.AreEqual(age, user.Age);
            Assert.AreEqual(height, user.Height);
            Assert.AreEqual(weight, user.Weight);
        }

        [TestMethod()]
        public void LogInTest()
        {
            // Arrange

            string login = "TestUserLogin";
            string password = "TestUserPassword";

            // Act

            UserController userController = new UserController();

            userController.Add(login, password);

            var user = userController.LogIn(login, password);

            userController.Remove(user.Login);

            userController.SaveUserData();

            // Assert

            Assert.AreEqual(login, user.Login);
            Assert.AreEqual(password, user.Password);
        }
    }
}