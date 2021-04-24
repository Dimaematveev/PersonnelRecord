using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace PersonnelRecord.BL.Classes.Employes.Tests
{
    [TestClass()]
    public class SimpleEmployeeTests
    {

        private string name;
        private Employee employee;

        #region Первоначальная настройка
        [TestInitialize]
        public void TestInitialize()
        {
            Debug.WriteLine("Начало теста. Корректные параметры!");
            var id = 1;
            Debug.WriteLine("id = '1'");
            name = "Name1";
            Debug.WriteLine("name = 'Name1'");
            var birthday = new DateTime(1994, 12, 2);
            Debug.WriteLine("birthday = '2.12.1994'");
            // Act — выполнение или вызов тестируемого сценария;
            employee = new Employee(id, name, birthday);
            Debug.WriteLine("Создали класс employee");

            Debug.WriteLine("Настройка закончена");
        }
        #endregion


        [TestMethod()]
        public void AddPosition()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ChangeFullName()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ChangePosition()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void Dismissal()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetBirthday()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetChanges()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetFullName()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetID()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetListCurrentPositions()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void Recruitment()
        {
            Assert.Fail();
        }
    }
}