using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersonnelRecord.BL.Classes;
using PersonnelRecord.BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelRecord.BL.Classes.Unit.Tests
{
    [TestClass()]
    public class SimpleUnitConstructorTests
    {
        [TestMethod()]
        public void ConstructorTest_Unit1AndPos1Pos2_CreateClass()
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
            var unit = new SimpleUnit(nameUnit, positionsName);
            Debug.WriteLine("Создали класс unit");

            // Assert — проверка того, что тестируемый вызов ведет себя 
            // определенным образом.
            Debug.WriteLine($"Должно быть='{nameUnit}', unit.GetName='{unit.GetName()}'");
            Assert.AreEqual(nameUnit, unit.GetName());

            Debug.WriteLine($"Должно быть='{null}', unit.GetMainUnit='{unit.GetMainUnit()}'");
            Assert.IsNull(unit.GetMainUnit());

            Debug.WriteLine($"Должно быть='0', unit.GetSubordinateUnits().Count='{unit.GetSubordinateUnits().Count}'");
            CollectionAssert.AreEqual(new List<IUnit>(), unit.GetSubordinateUnits().ToList());


            Debug.WriteLine($"Должно быть='{positionsName.Count}', unit.GetPositions='{unit.GetPositions().Count}'");
            CollectionAssert.AreEqual(positionsName, unit.GetPositions().Select(x => x.GetName()).ToList());

            Debug.WriteLine($"Должно быть='0', unit.GetHierarchyTier='{unit.GetHierarchyTier()}'");
            Assert.AreEqual(0, unit.GetHierarchyTier());

            Debug.WriteLine($"Должно быть='{false}', unit.GetIsDelete='{unit.GetIsDelete()}'");
            Assert.IsFalse(unit.GetIsDelete());

            Debug.WriteLine("Конец теста");
        }
    }
}