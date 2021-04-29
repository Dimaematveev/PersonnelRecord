using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PersonnelRecord.BL.Classes.Employes.Tests
{
    [TestClass()]
    public class EmployeeConstructorTests
    {
        [TestMethod()]
        [DataTestMethod()]
        [DataRow("Name1",-18,0,0)]
        [DataRow("Name1 Fam1 Ph1",-70,0,0)]
        [DataRow("Name1 Fam1 Ph1",-100,0,1)]
        public void ConstructorTest_WithValidArguments_CreateClass(string name, int year, int month, int day)
        {
            //Arrange (настройка) — в этом блоке кода мы настраиваем 
            //тестовое окружение тестируемого юнита;
            Debug.WriteLine("Начало теста. Корректные параметры!");
            var id = 1;
            Debug.WriteLine("id = '1'");
            //var name = "Name1";
            Debug.WriteLine($"name = '{name}'");
            DateTime birthday = DateTime.Now.AddDays(day).AddMonths(month).AddYears(year);
            Debug.WriteLine($"birthday = '{birthday}'");
            var changes = new List<Change>().AsReadOnly();

            // Act — выполнение или вызов тестируемого сценария;
            var employee = new Employee(id, name, birthday);
            Debug.WriteLine("Создали класс employee");

            // Assert — проверка того, что тестируемый вызов ведет себя 
            // определенным образом.
            Debug.WriteLine($"Должно быть='{name}', employee.GetFullName='{employee.GetFullName()}'");
            Assert.AreEqual(name, employee.GetFullName());
            Debug.WriteLine($"Должно быть='{id}', employee.GetID='{employee.GetID()}'");
            Assert.AreEqual(id, employee.GetID());
            Debug.WriteLine($"Должно быть='{birthday}', employee.GetBirthday='{employee.GetBirthday()}'");
            Assert.AreEqual(birthday.Date, employee.GetBirthday());
            Debug.WriteLine($"Должно быть='{changes.Count}', employee.GetChanges='{employee.GetChanges().Count}'");
            CollectionAssert.AreEqual(changes.ToList(), employee.GetChanges().ToList());
        }


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
            Debug.WriteLine("Начало теста. НеКорректные параметры! Имя Null");
            var id = 1;
            Debug.WriteLine("id = '1'");
            Debug.WriteLine($"name = '{name}'");
            var birthday = new DateTime(1994, 12, 2);
            Debug.WriteLine("birthday = '2.12.1994'");
            var changes = new List<Change>().AsReadOnly();

            // Act — выполнение или вызов тестируемого сценария;
            Debug.WriteLine("Попытка создать класс employee");
            var employee = new Employee(id, name, birthday);
            Debug.WriteLine("Создали класс employee");

            // Assert — проверка того, что тестируемый вызов ведет себя 
            // определенным образом.

        }


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
            Debug.WriteLine("Начало теста. Некорректные параметры! Возраст меньше 18 лет! или больше 100 лет!");
            var id = 1;
            Debug.WriteLine("id = '1'");
            string name = "Name1";
            Debug.WriteLine($"name = '{name}'");
            DateTime birthday = DateTime.Today.AddDays(day).AddMonths(month).AddYears(year);
            Debug.WriteLine($"birthday = '{birthday}'");
            var changes = new List<Change>().AsReadOnly();

            // Act — выполнение или вызов тестируемого сценария;
            Debug.WriteLine("Попытка создать класс employee");
            var employee = new Employee(id, name, birthday);
            Debug.WriteLine("Создали класс employee");

            // Assert — проверка того, что тестируемый вызов ведет себя 
            // определенным образом.

        }
    }
}