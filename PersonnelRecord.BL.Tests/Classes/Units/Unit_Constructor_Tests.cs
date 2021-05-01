using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonnelRecord.BL.Classes.Units.Tests
{
    [TestClass()]
    public class Unit_Constructor_Tests
    {
        [TestMethod()]
        public void ConstructorTest_WithValidArguments_CreateClass()
        {
            //Arrange (настройка) — в этом блоке кода мы настраиваем 
            //тестовое окружение тестируемого юнита;
            var nameUnit = "Unit1";
            var positionsName = new List<string>() { "Pos1", "Pos2" };

            // Act — выполнение или вызов тестируемого сценария;
            var unit = new Unit(nameUnit, positionsName);

            // Assert — проверка того, что тестируемый вызов ведет себя 
            // определенным образом.
            Assert.AreEqual(nameUnit, unit.GetName());
            Assert.IsNull(unit.GetMainUnit());
            CollectionAssert.AreEqual(new List<Unit>(), (System.Collections.ICollection)unit.GetSubordinateUnits().ToList());
            CollectionAssert.AreEqual(positionsName, unit.GetPositions().Select(x => x.GetName()).ToList());
            Assert.AreEqual(0, unit.GetHierarchyTier());
            Assert.IsFalse(unit.GetIsDelete());
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
            //ACT
            var positionsName = new List<string>() { "Pos1", "Pos2" };
            var unit = new Unit(unitName, positionsName);

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
            string unitName = "Name1";
            string pos2 = "Pos2";
            //ACT

            var positionsName = new List<string>() { pos1, pos2 };

            var unit = new Unit(unitName, positionsName);

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
            string unitName = "Name1";
            string pos1 = "Pos1";
            //ACT

            var positionsName = new List<string>() { pos1, pos2 };

            var unit = new Unit(unitName, positionsName);

        }

        [ExpectedException(typeof(ArgumentNullException), "Исключение на передачу в список должностей null, не было вызвано.")]
        [TestMethod()]
        public void ConstructorTest_WhenListPosIsNull_NotCreateClassAndExceptionReterned()
        {
            //Arrange (настройка) — в этом блоке кода мы настраиваем 
            //тестовое окружение тестируемого юнита;
            string unitName = "Name1";
            //ACT
            List<string> positionsName = null;

            var unit = new Unit(unitName, positionsName);

        }

        [ExpectedException(typeof(ArgumentNullException), "Исключение на передачу в список должностей пустого списка, не было вызвано.")]
        [TestMethod()]
        public void ConstructorTest_WhenListPosIsEmpty_NotCreateClassAndExceptionReterned()
        {
            //Arrange (настройка) — в этом блоке кода мы настраиваем 
            //тестовое окружение тестируемого юнита;
            string unitName = "Name1";
            //ACT
            List<string> positionsName = new List<string>();

            var unit = new Unit(unitName, positionsName);

        }
    }
}