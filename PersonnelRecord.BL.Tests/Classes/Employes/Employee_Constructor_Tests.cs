using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonnelRecord.BL.Classes.Employes.Tests
{
    /// <summary>
    /// Тестирование "Конструктора Сотрудника"
    /// </summary>
    [TestClass()]
    public class Employee_Constructor_Tests
    {
        /// <summary>
        /// Правильные параметры
        /// </summary>
        /// <param name="name">ФИО сотрудника</param>
        /// <param name="year">Изменение года от сегодняшней даты</param>
        /// <param name="month">Изменение месяца от сегодняшней даты</param>
        /// <param name="day">Изменение дня от сегодняшней даты</param>
        [TestMethod()]
        [DataTestMethod()]
        [DataRow("Name1",-18,0,0)]
        [DataRow("Name1 Fam1 Ph1",-70,0,0)]
        [DataRow("Name1 Fam1 Ph1",-100,0,1)]
        public void ConstructorTest_WithValidArguments_CreateClass(string name, int year, int month, int day)
        {
            //Arrange (настройка) — в этом блоке кода мы настраиваем 
            //тестовое окружение тестируемого юнита;
            var id = 1;
            //var name = "Name1";
            DateTime birthday = DateTime.Now.AddDays(day).AddMonths(month).AddYears(year);
            var changes = new List<Change>().AsReadOnly();

            // Act — выполнение или вызов тестируемого сценария;
            var employee = new Employee(id, name, birthday);

            // Assert — проверка того, что тестируемый вызов ведет себя 
            // определенным образом.
            Assert.AreEqual(name, employee.GetFullName());
            Assert.AreEqual(id, employee.GetID());
            Assert.AreEqual(birthday.Date, employee.GetBirthday());
            CollectionAssert.AreEqual(changes.ToList(), employee.GetChanges().ToList());
        }

        /// <summary>
        /// Исключение на передачу в ФИО null или пустой строки
        /// </summary>
        /// <param name="name">ФИО</param>
        [ExpectedException(typeof(ArgumentNullException), "Исключение на передачу в имя null или пустой строки, не было вызвано.")]
        [DataTestMethod()]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow("  ")]
        [DataRow("\n")]
        [DataRow("\t")]
        public void ConstructorTest_WhenNameEmpty_NotCreateClassAndExceptionReterned(string name)
        {
            //Arrange (настройка) — в этом блоке кода мы настраиваем 
            //тестовое окружение тестируемого юнита;
            var id = 1;
            var birthday = new DateTime(1994, 12, 2);
            var changes = new List<Change>().AsReadOnly();

            // Act — выполнение или вызов тестируемого сценария;
            var employee = new Employee(id, name, birthday);

            // Assert — проверка того, что тестируемый вызов ведет себя 
            // определенным образом.

        }

        /// <summary>
        /// Исключение возраст человека меньше 18 лет или больше 100 лет
        /// </summary>
        /// <param name="year">Изменение года от сегодняшней даты</param>
        /// <param name="month">Изменение месяца от сегодняшней даты</param>
        /// <param name="day">Изменение дня от сегодняшней даты</param>
        [ExpectedException(typeof(ArgumentException), "Исключение возраст человека меньше 18 лет или больше 100 лет, не было вызвано.")]
        [DataTestMethod()]
        [DataRow(0,0,0)]
        [DataRow(-18,0,1)]
        [DataRow(1,-1,0)]
        [DataRow(-100,0,0)]
        public void ConstructorTest_WhenBirthdayLess18OrMore100_NotCreateClassAndExceptionReterned(int year, int month, int day)
        {
            //Arrange (настройка) — в этом блоке кода мы настраиваем 
            //тестовое окружение тестируемого юнита;
            var id = 1;
            string name = "Name1";
            DateTime birthday = DateTime.Today.AddDays(day).AddMonths(month).AddYears(year);
            var changes = new List<Change>().AsReadOnly();

            // Act — выполнение или вызов тестируемого сценария;
            var employee = new Employee(id, name, birthday);

            // Assert — проверка того, что тестируемый вызов ведет себя 
            // определенным образом.

        }
    }
}