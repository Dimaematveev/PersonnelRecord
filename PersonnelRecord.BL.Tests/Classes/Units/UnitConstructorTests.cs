using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PersonnelRecord.BL.Classes.Units.Tests
{
    [TestClass()]
    public class UnitConstructorTests
    {
        [TestMethod()]
        public void ConstructorTest_WithValidArguments_CreateClass()
        {
            //Arrange (настройка) — в этом блоке кода мы настраиваем 
            //тестовое окружение тестируемого юнита;
            Debug.WriteLine("Начало теста. Корректные параметры!");
            var nameUnit = "Unit1";
            Debug.WriteLine("nameUnit = 'Unit1'");
            var positionsName = new List<string>() { "Pos1", "Pos2" };
            Debug.WriteLine("positionsName = 'Pos1' and 'Pos2'");

            // Act — выполнение или вызов тестируемого сценария;
            Debug.WriteLine("Создаем класс unit");
            var unit = new Unit(nameUnit, positionsName);
            Debug.WriteLine("Создали класс unit");

            // Assert — проверка того, что тестируемый вызов ведет себя 
            // определенным образом.
            Debug.WriteLine($"Должно быть='{nameUnit}', unit.GetName='{unit.GetName()}'");
            Assert.AreEqual(nameUnit, unit.GetName());

            Debug.WriteLine($"Должно быть='{null}', unit.GetMainUnit='{unit.GetMainUnit()}'");
            Assert.IsNull(unit.GetMainUnit());

            Debug.WriteLine($"Должно быть='0', unit.GetSubordinateUnits().Count='{unit.GetSubordinateUnits().Count}'");
            CollectionAssert.AreEqual(new List<Unit>(), (System.Collections.ICollection)unit.GetSubordinateUnits().ToList());


            Debug.WriteLine($"Должно быть='{positionsName.Count}', unit.GetPositions='{unit.GetPositions().Count}'");
            CollectionAssert.AreEqual(positionsName, unit.GetPositions().Select(x => x.GetName()).ToList());

            Debug.WriteLine($"Должно быть='0', unit.GetHierarchyTier='{unit.GetHierarchyTier()}'");
            Assert.AreEqual(0, unit.GetHierarchyTier());

            Debug.WriteLine($"Должно быть='{false}', unit.GetIsDelete='{unit.GetIsDelete()}'");
            Assert.IsFalse(unit.GetIsDelete());

            Debug.WriteLine("Конец теста");
        }


        [ExpectedException(typeof(ArgumentNullException), "Исключение на передачу в Название подразделения null или пустой строки, не было вызвано.")]
        [DataTestMethod()]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow("  ")]
        [DataRow("\n")]
        [DataRow("\t")]
        public void ConstructorTest_WhenNameUnitIsNull_NotCreateClassAndExceptionReterned(string unitName)
        {
            //Arrange (настройка) — в этом блоке кода мы настраиваем 
            //тестовое окружение тестируемого юнита;
            Debug.WriteLine("Начало теста. Некорректные параметры!");
            
            Debug.WriteLine($"nameUnit = '{unitName}'");
            //ACT
            var positionsName = new List<string>() { "Pos1", "Pos2" };
            Debug.Write("positionsName = {");
            foreach (var posName in positionsName)
            {
                Debug.Write($"'{posName}', ");
            }
            Debug.WriteLine("}.");

            var unit = new Unit(unitName, positionsName);
            Debug.WriteLine("Создали класс unit");

        }


        [ExpectedException(typeof(ArgumentNullException), "Исключение на передачу в Название должностей null или пустой строки, не было вызвано.")]
        [DataTestMethod()]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow("  ")]
        [DataRow("\n")]
        [DataRow("\t")]
        public void ConstructorTest_WhenPos1IsNull_NotCreateClassAndExceptionReterned(string pos1)
        {
            //Arrange (настройка) — в этом блоке кода мы настраиваем 
            //тестовое окружение тестируемого юнита;
            Debug.WriteLine("Начало теста. Некорректные параметры!");
            string unitName = "Name1";
            Debug.WriteLine($"nameUnit = '{unitName}'");
            string pos2 = "Pos2";
            //ACT

            var positionsName = new List<string>() { pos1, pos2 };
            Debug.Write("positionsName = {");
            foreach (var posName in positionsName)
            {
                Debug.Write($"'{posName}', ");
            }
            Debug.WriteLine("}.");

            var unit = new Unit(unitName, positionsName);
            Debug.WriteLine("Создали класс unit");

        }

        [ExpectedException(typeof(ArgumentNullException), "Исключение на передачу в Название должностей null или пустой строки, не было вызвано.")]
        [DataTestMethod()]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow("  ")]
        [DataRow("\n")]
        [DataRow("\t")]
        public void ConstructorTest_WhenPos2IsNull_NotCreateClassAndExceptionReterned(string pos2)
        {
            //Arrange (настройка) — в этом блоке кода мы настраиваем 
            //тестовое окружение тестируемого юнита;
            Debug.WriteLine("Начало теста. Некорректные параметры!");
            string unitName = "Name1";
            Debug.WriteLine($"nameUnit = '{unitName}'");
            string pos1 = "Pos1";
            //ACT

            var positionsName = new List<string>() { pos1, pos2 };
            Debug.Write("positionsName = {");
            foreach (var posName in positionsName)
            {
                Debug.Write($"'{posName}', ");
            }
            Debug.WriteLine("}.");

            var unit = new Unit(unitName, positionsName);
            Debug.WriteLine("Создали класс unit");

        }

        [ExpectedException(typeof(ArgumentNullException), "Исключение на передачу в список должностей null, не было вызвано.")]
        [TestMethod()]
        public void ConstructorTest_WhenListPosIsNull_NotCreateClassAndExceptionReterned()
        {
            //Arrange (настройка) — в этом блоке кода мы настраиваем 
            //тестовое окружение тестируемого юнита;
            Debug.WriteLine("Начало теста. Некорректные параметры!");
            string unitName = "Name1";
            Debug.WriteLine($"nameUnit = '{unitName}'");
            //ACT
            List<string> positionsName = null;
            Debug.Write("positionsName = {null}.");

            var unit = new Unit(unitName, positionsName);
            Debug.WriteLine("Создали класс unit");

        }

        [ExpectedException(typeof(ArgumentNullException), "Исключение на передачу в список должностей пустого списка, не было вызвано.")]
        [TestMethod()]
        public void ConstructorTest_WhenListPosIsEmpty_NotCreateClassAndExceptionReterned()
        {
            //Arrange (настройка) — в этом блоке кода мы настраиваем 
            //тестовое окружение тестируемого юнита;
            Debug.WriteLine("Начало теста. Некорректные параметры!");
            string unitName = "Name1";
            Debug.WriteLine($"nameUnit = '{unitName}'");
            //ACT
            List<string> positionsName = new List<string>();
            Debug.Write("positionsName.Count = {0}.");

            var unit = new Unit(unitName, positionsName);
            Debug.WriteLine("Создали класс unit");

        }
    }
}