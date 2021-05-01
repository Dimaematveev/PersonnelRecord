using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace PersonnelRecord.BL.Classes.Units.Tests
{
    [TestClass()]
    public class Unit_ChangeMainUnit_Tests
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



        #region Функция IsPossibleChangeMainUnit (Проверка на Изменить главное подразделение)
        [TestMethod()]
        public void IsPossibleChangeMainUnit_WithValidArguments_NullReturned()
        {
            

            // Act — выполнение 
            
            var ret = subUnit1.IsPossibleChangeMainUnit(subUnit2);
            

            // Assert — проверка
            
            Assert.AreEqual(unit, subUnit1.GetMainUnit());
            
            Assert.IsNull(ret);

            
        }

        [TestMethod()]
        public void IsPossibleChangeMainUnit_WhenMainUnitNull_NotChangeMainUnitAndFalseReturned()
        {
            

            // Arrange(настройка)
            Unit unit2 = null;

            

            // Act — выполнение 
            
            var ret = unit.IsPossibleChangeMainUnit(unit2);
            

            // Assert — проверка
            
            Assert.AreEqual(mainUnit, unit.GetMainUnit());
            
            Assert.IsNotNull(ret);

            
        }
        [TestMethod()]
        public void IsPossibleChangeMainUnit_WhenMainUnitHierarchyTierEqual0_NotChangeMainUnitAndFalseReturned()
        {
            

            // Arrange(настройка)
            Unit unit2 = new Unit("n1", new List<string>() { "n1" });

            

            // Act — выполнение 
            
            var ret = unit.IsPossibleChangeMainUnit(unit2);
            

            // Assert — проверка
            
            Assert.AreEqual(mainUnit, unit.GetMainUnit());
            
            Assert.IsNotNull(ret);

            
        }

        [TestMethod()]
        public void IsPossibleChangeMainUnit_WhenMainUnitIsDelete_NotChangeMainUnitAndFalseReturned()
        {
            

            // Arrange(настройка)
            subUnit2.Delete();

            

            // Act — выполнение 
            
            var ret = unit.IsPossibleChangeMainUnit(subUnit2);
            

            // Assert — проверка
            
            Assert.AreEqual(mainUnit, unit.GetMainUnit());
            
            Assert.IsNotNull(ret);

            
        }

        [TestMethod()]
        public void IsPossibleChangeMainUnit_WhenMainUnitEqalOurUnit_NotChangeMainUnitAndFalseReturned()
        {
            

            // Arrange(настройка)

            

            // Act — выполнение 
            
            var ret = unit.IsPossibleChangeMainUnit(unit);
            

            // Assert — проверка
            
            Assert.AreEqual(mainUnit, unit.GetMainUnit());
            
            Assert.IsNotNull(ret);

            
        }

        [TestMethod()]
        public void IsPossibleChangeMainUnit_WhenMainUnitIsSub_NotChangeMainUnitAndFalseReturned()
        {
            

            // Arrange(настройка)

            // Act — выполнение 
            
            var ret = unit.IsPossibleChangeMainUnit(subUnit1);
            

            // Assert — проверка
            
            Assert.AreEqual(mainUnit, unit.GetMainUnit());
            
            Assert.IsNotNull(ret);

            
        }
        #endregion

        #region Функция ChangeMainUnit (Изменить главное подразделение)
        [TestMethod()]
        public void ChangeMainUnit_WithValidArguments_NewMainUnitAndTrueReturned()
        {
            

            // Act — выполнение 
            
            var ret = subUnit1.ChangeMainUnit(subUnit2);
            

            // Assert — проверка
            
            Assert.AreEqual(subUnit2, subUnit1.GetMainUnit());
            
            Assert.IsTrue(ret);

            
        }

       
        [TestMethod()]
        public void ChangeMainUnit_WhenMainUnitNull_NotChangeMainUnitAndFalseReturned()
        {
            

            // Arrange(настройка)
            Unit unit2 = null;

            

            // Act — выполнение 
            
            var ret = unit.ChangeMainUnit(unit2);
            

            // Assert — проверка
            
            Assert.AreEqual(mainUnit, unit.GetMainUnit());
            
            Assert.IsFalse(ret);

            
        }
        [TestMethod()]
        public void ChangeMainUnit_WhenMainUnitHierarchyTierEqual0_NotChangeMainUnitAndFalseReturned()
        {
            

            // Arrange(настройка)
            Unit unit2 = new Unit("n1", new List<string>() { "n1" });

            

            // Act — выполнение 
            
            var ret = unit.ChangeMainUnit(unit2);
            

            // Assert — проверка
            
            Assert.AreEqual(mainUnit, unit.GetMainUnit());
            
            Assert.IsFalse(ret);

            
        }

        [TestMethod()]
        public void ChangeMainUnit_WhenMainUnitIsDelete_NotChangeMainUnitAndFalseReturned()
        {
            

            // Arrange(настройка)
            subUnit2.Delete();

            

            // Act — выполнение 
            
            var ret = unit.ChangeMainUnit(subUnit2);
            

            // Assert — проверка
            
            Assert.AreEqual(mainUnit, unit.GetMainUnit());
            
            Assert.IsFalse(ret);

            
        }

        [TestMethod()]
        public void ChangeMainUnit_WhenMainUnitEqalOurUnit_NotChangeMainUnitAndFalseReturned()
        {
            

            // Arrange(настройка)

            

            // Act — выполнение 
            
            var ret = unit.ChangeMainUnit(unit);
            

            // Assert — проверка
            
            Assert.AreEqual(mainUnit, unit.GetMainUnit());
            
            Assert.IsFalse(ret);

            
        }

        [TestMethod()]
        public void ChangeMainUnit_WhenMainUnitIsSub_NotChangeMainUnitAndFalseReturned()
        {
            

            // Arrange(настройка)

            // Act — выполнение 
            
            var ret = unit.ChangeMainUnit(subUnit1);
            

            // Assert — проверка
            
            Assert.AreEqual(mainUnit, unit.GetMainUnit());
            
            Assert.IsFalse(ret);

            
        }
        #endregion

    }
}