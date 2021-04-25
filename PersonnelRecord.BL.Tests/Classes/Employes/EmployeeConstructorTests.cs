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
        public void ConstructorTest_Pos1AndUnit1_CreateClass()
        {
            //Arrange (настройка) — в этом блоке кода мы настраиваем 
            //тестовое окружение тестируемого юнита;
            Debug.WriteLine("Начало теста. Корректные параметры!");
            var id = 1;
            Debug.WriteLine("id = '1'");
            var name = "Name1";
            Debug.WriteLine("name = 'Name1'");
            var birthday = new DateTime(1994, 12, 2);
            Debug.WriteLine("birthday = '2.12.1994'");
            var changes = new List<Change>().AsReadOnly();
            // Act — выполнение или вызов тестируемого сценария;
            var employee = new Employee(id, name, birthday);
            Debug.WriteLine("Создали класс employee");

            // Assert — проверка того, что тестируемый вызов ведет себя 
            // определенным образом.
            Debug.WriteLine($"Должно быть='{name}', position.GetName='{employee.GetFullName()}'");
            Assert.AreEqual(name, employee.GetFullName());
            Debug.WriteLine($"Должно быть='{id}', position.GetName='{employee.GetID()}'");
            Assert.AreEqual(id, employee.GetID());
            Debug.WriteLine($"Должно быть='{birthday}', position.GetName='{employee.GetBirthday()}'");
            Assert.AreEqual(birthday, employee.GetBirthday());
            Debug.WriteLine($"Должно быть='{changes.Count}', position.GetName='{employee.GetChanges().Count}'");
            CollectionAssert.AreEqual(changes.ToList(), employee.GetChanges().ToList());
        }
    }
}