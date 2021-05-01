using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace PersonnelRecord.BL.Classes.Units.Tests
{
    [TestClass()]
    public class Unit_AddSubordinateUnit_Tests
    {

        private string nameUnit;
        private List<string> positionsName;
        private Unit unit, mainUnit, subUnit1, subUnit2;

        #region Первоначальная настройка
        [TestInitialize]
        public void TestInitialize()
        {
            var nameMainUnit = "MainUnit";
            var positionsMainUnit = new List<string>() { "MainPos1", "MainPos2" };
            mainUnit = new Unit(nameMainUnit, positionsMainUnit, true);

            var nameSubUnit1 = "SubUnit1";
            var positionsSubUnit1 = new List<string>() { "Sub1Pos1", "Sub1Pos2" };
            subUnit1 = new Unit(nameSubUnit1, positionsSubUnit1);

            var nameSubUnit2 = "SubUnit2";
            var positionsSubUnit2 = new List<string>() { "Sub2Pos1", "Sub2Pos2" };
            subUnit2 = new Unit(nameSubUnit2, positionsSubUnit2);

            nameUnit = "Unit1";
            positionsName = new List<string>() { "Pos1", "Pos2" };
            unit = new Unit(nameUnit, positionsName);
            
            unit.Reassignment(mainUnit);
            subUnit1.Reassignment(unit);
            subUnit2.Reassignment(unit);

        }
        #endregion


        #region Функция IsPossibleAddSubordinateUnit (Проверка на Добавить подчиненное подразделение)
        [TestMethod()]
        public void IsPossibleAddSubordinateUnit_WithValidArguments_TrueReturned()
        {
            

            // Arrange(настройка)
            var NewSubName = "NewSubUnit1";
            var NewListPositions = new List<string>() { "1" };
            var newUnit = new Unit(NewSubName, NewListPositions);

            var SubUnits = unit.GetSubordinateUnits().ToList();
            //SubUnits.Add(newUnit);
            

            // Act — выполнение 
            
            var ret = unit.IsPossibleAddSubordinateUnit(newUnit);
            

            // Assert — проверка
            
            CollectionAssert.AreEqual(SubUnits, unit.GetSubordinateUnits().ToList());
            
            Assert.IsTrue(ret);

            
        }

        [TestMethod()]
        public void IsPossibleAddSubordinateUnit_WhenSubIsNull_FalseReturned()
        {
            

            // Arrange(настройка)
            Unit newUnit = null;

            var SubUnits = unit.GetSubordinateUnits().ToList();
            //SubUnits.Add(newUnit);
            

            // Act — выполнение 
            
            var ret = unit.IsPossibleAddSubordinateUnit(newUnit);
            

            // Assert — проверка
            
            CollectionAssert.AreEqual(SubUnits, unit.GetSubordinateUnits().ToList());
            
            Assert.IsFalse(ret);

            
        }

        [TestMethod()]
        public void IsPossibleAddSubordinateUnit_WhenSubIsOurUnit_FalseReturned()
        {
            

            // Arrange(настройка)
            var SubUnits = unit.GetSubordinateUnits().ToList();
            //SubUnits.Add(newUnit);
            

            // Act — выполнение 
            
            var ret = unit.IsPossibleAddSubordinateUnit(unit);
            

            // Assert — проверка
            
            CollectionAssert.AreEqual(SubUnits, unit.GetSubordinateUnits().ToList());
            
            Assert.IsFalse(ret);

            
        }

        [TestMethod()]
        public void IsPossibleAddSubordinateUnit_WhenSubIsSubUnit_FalseReturned()
        {
            

            // Arrange(настройка)
            var SubUnits = unit.GetSubordinateUnits().ToList();
            //SubUnits.Add(newUnit);
            

            // Act — выполнение 
            
            var ret = unit.IsPossibleAddSubordinateUnit(subUnit1);
            

            // Assert — проверка
            
            CollectionAssert.AreEqual(SubUnits, unit.GetSubordinateUnits().ToList());
            
            Assert.IsFalse(ret);

            
        }

        [TestMethod()]
        public void IsPossibleAddSubordinateUnit_WhenSubIsMainOur_FalseReturned()
        {
            

            // Arrange(настройка)
            var SubUnits = subUnit1.GetSubordinateUnits().ToList();
            //SubUnits.Add(newUnit);
            

            // Act — выполнение 
            
            var ret = subUnit1.IsPossibleAddSubordinateUnit(mainUnit);
            

            // Assert — проверка
            
            CollectionAssert.AreEqual(SubUnits, subUnit1.GetSubordinateUnits().ToList());
            
            Assert.IsFalse(ret);

            
        }

        #endregion

        #region Функция AddSubordinateUnit (Добавить подчиненное подразделение)
        [TestMethod()]
        public void AddSubordinateUnit_WithValidArguments_AddSubordinateUnitAndTrueReturned()
        {
            

            // Arrange(настройка)
            var NewSubName = "NewSubUnit1";
            var NewListPositions = new List<string>() { "1" };
            var newUnit = new Unit(NewSubName, NewListPositions);

            var SubUnits = unit.GetSubordinateUnits().ToList();
            SubUnits.Add(newUnit);
            

            // Act — выполнение 
            
            var ret = unit.AddSubordinateUnit(newUnit);
            

            // Assert — проверка
            
            CollectionAssert.AreEqual(SubUnits, unit.GetSubordinateUnits().ToList());
            
            Assert.IsTrue(ret);

            
        }

        [TestMethod()]
        public void AddSubordinateUnit_WhenSubIsNull_FalseReturned()
        {
            

            // Arrange(настройка)
            Unit newUnit = null;

            var SubUnits = unit.GetSubordinateUnits().ToList();
            //SubUnits.Add(newUnit);
            

            // Act — выполнение 
            
            var ret = unit.AddSubordinateUnit(newUnit);
            

            // Assert — проверка
            
            CollectionAssert.AreEqual(SubUnits, unit.GetSubordinateUnits().ToList());
            
            Assert.IsFalse(ret);

            
        }

        [TestMethod()]
        public void AddSubordinateUnit_WhenSubIsOurUnit_FalseReturned()
        {
            

            // Arrange(настройка)
            var SubUnits = unit.GetSubordinateUnits().ToList();
            //SubUnits.Add(newUnit);
            

            // Act — выполнение 
            
            var ret = unit.AddSubordinateUnit(unit);
            

            // Assert — проверка
            
            CollectionAssert.AreEqual(SubUnits, unit.GetSubordinateUnits().ToList());
            
            Assert.IsFalse(ret);

            
        }

        [TestMethod()]
        public void AddSubordinateUnit_WhenSubIsSubUnit_FalseReturned()
        {
            

            // Arrange(настройка)
            var SubUnits = unit.GetSubordinateUnits().ToList();
            //SubUnits.Add(newUnit);
            

            // Act — выполнение 
            
            var ret = unit.AddSubordinateUnit(subUnit1);
            

            // Assert — проверка
            
            CollectionAssert.AreEqual(SubUnits, unit.GetSubordinateUnits().ToList());
            
            Assert.IsFalse(ret);

            
        }

        [TestMethod()]
        public void AddSubordinateUnit_WhenSubIsMainOur_FalseReturned()
        {
            

            // Arrange(настройка)
            var SubUnits = subUnit1.GetSubordinateUnits().ToList();
            //SubUnits.Add(newUnit);
            

            // Act — выполнение 
            
            var ret = subUnit1.AddSubordinateUnit(mainUnit);
            

            // Assert — проверка
            
            CollectionAssert.AreEqual(SubUnits, subUnit1.GetSubordinateUnits().ToList());
            
            Assert.IsFalse(ret);

            
        }

        #endregion


    }
}