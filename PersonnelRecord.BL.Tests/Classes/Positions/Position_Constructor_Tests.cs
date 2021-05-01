using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonnelRecord.BL.Classes.Positions.Tests
{
    [TestClass()]
    public class Position_Constructor_Tests
    {
        [TestMethod()]
        public void ConstructorTest_WithValidArguments_CreateClass()
        {
            //Arrange (настройка) — в этом блоке кода мы настраиваем 
            //тестовое окружение тестируемого юнита;
            var nameUnit = "Unit1";
            var positionsName = new List<string>() { "Pos1" };
            var unit = new Unit(nameUnit, positionsName);

            // Act — выполнение или вызов тестируемого сценария;
            var position = unit.GetPositions().FirstOrDefault();

            // Assert — проверка того, что тестируемый вызов ведет себя 
            // определенным образом.
            Assert.AreEqual(positionsName[0], position.GetName());

            Assert.AreEqual(unit, position.GetUnit());

            Assert.IsFalse(position.GetIsPositionBusy());

            Assert.IsFalse(position.GetIsDelete());
        }

        [ExpectedException(typeof(ArgumentNullException), "Исключение на передачу в Название должности null или пустой строки, не было вызвано.")]
        [DataTestMethod()]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow("  ")]
        [DataRow("\n")]
        [DataRow("\t")]
        public void ConstructorTest_WhenNamePositionIsNull_NotCreateClassAndExceptionReterned(string positionName)
        {
            //Arrange (настройка) — в этом блоке кода мы настраиваем 
            //тестовое окружение тестируемого юнита;
            var nameUnit = "Unit1";
            //ACT
            var positionsName = new List<string>() { positionName };
            var unit = new Unit(nameUnit, positionsName);

        }

    }
}