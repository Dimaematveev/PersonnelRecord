﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PersonnelRecord.BL.Classes.Positions.Tests
{
    [TestClass()]
    public class PositionConstructorTests
    {
        [TestMethod()]
        public void ConstructorTest_WithValidArguments_CreateClass()
        {
            //Arrange (настройка) — в этом блоке кода мы настраиваем 
            //тестовое окружение тестируемого юнита;
            Debug.WriteLine("Начало теста. Корректные параметры!");
            var nameUnit = "Unit1";
            Debug.WriteLine("nameUnit = 'Unit1'");
            var positionsName = new List<string>() { "Pos1" };
            Debug.WriteLine("positionsName = 'Pos1'");
            var unit = new Unit(nameUnit, positionsName);
            Debug.WriteLine("Создали класс unit");

            // Act — выполнение или вызов тестируемого сценария;
            Debug.WriteLine("Выделяем класс Position");
            var position = unit.GetPositions().FirstOrDefault();
            Debug.WriteLine("Выделили класс Position");

            // Assert — проверка того, что тестируемый вызов ведет себя 
            // определенным образом.
            Debug.WriteLine($"Должно быть='{positionsName[0]}', position.GetName='{position.GetName()}'");
            Assert.AreEqual(positionsName[0], position.GetName());

            Debug.WriteLine($"Должно быть='{unit.GetName()}', position.GetUnit='{position.GetUnit().GetName()}'");
            Assert.AreEqual(unit, position.GetUnit());

            Debug.WriteLine($"Должно быть='False' position.GetIsPositionBusy='{position.GetIsPositionBusy()}'");
            Assert.IsFalse(position.GetIsPositionBusy());

            Debug.WriteLine($"Должно быть='False' position.GetIsDelete='{position.GetIsDelete()}'");
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
            Debug.WriteLine("Начало теста. Некорректные параметры!");
            var nameUnit = "Unit1";
            Debug.WriteLine("nameUnit = 'Unit1'");
            //ACT
            var positionsName = new List<string>() { positionName };
            Debug.WriteLine($"positionsName = '{positionName}'");
            var unit = new Unit(nameUnit, positionsName);
            Debug.WriteLine("Создали класс unit");

        }

    }
}