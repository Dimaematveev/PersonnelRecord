using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace PersonnelRecord.BL.Classes.Units.Tests
{
    /// <summary>
    /// Тестирование "Удалить подчиненное подразделение"
    /// </summary>
    [TestClass()]
    public class Unit_DeleteSubordinateUnit_Tests
    {

        private string nameUnit;
        private List<string> positionsName;
        private Unit unit, mainUnit, subUnit1, subUnit2;

        #region Первоначальная настройка
        /// <summary>
        /// Вызывается перед каждым методом теста
        /// </summary>
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



        #region Функция IsPossibleDeleteSubordinateUnit (Проверка на Удалить подчиненное подразделение)
        /// <summary>
        /// Правильные параметры
        /// </summary>
        [TestMethod()]
        public void IsPossibleDeleteSubordinateUnit_WithValidArguments_TrueReturned()
        {
            

            // Arrange(настройка)
            var sub = unit.GetSubordinateUnits().ToList();
            // Act — выполнение 
            
            var ret = unit.IsPossibleDeleteSubordinateUnit(subUnit1);
            
            //sub.Remove(subUnit1);

            // Assert — проверка
            
            CollectionAssert.AreEqual(sub, unit.GetSubordinateUnits().ToList());
            
            Assert.IsTrue(ret);

            
        }

        /// <summary>
        /// Нельзя удалить подчиненное подразделение если оно null
        /// </summary>
        [TestMethod()]
        public void IsPossibleDeleteSubordinateUnit_WhenDeleteUnitIsNull_FalseReturned()
        {
            

            // Arrange(настройка)
            var sub = unit.GetSubordinateUnits().ToList();
            // Act — выполнение 
            
            var ret = unit.IsPossibleDeleteSubordinateUnit(null);
            
            //sub.Remove(subUnit1);

            // Assert — проверка
            
            CollectionAssert.AreEqual(sub, unit.GetSubordinateUnits().ToList());
            
            Assert.IsFalse(ret);

            
        }

        /// <summary>
        /// Нельзя удалить подчиненное подразделение если его нет в списке подчиненных
        /// </summary>
        [TestMethod()]
        public void IsPossibleDeleteSubordinateUnit_WhenDeleteUnitNotInSubUnit_FalseReturned()
        {
            

            // Arrange(настройка)
            var sub = unit.GetSubordinateUnits().ToList();
            // Act — выполнение 
            
            var ret = unit.IsPossibleDeleteSubordinateUnit(mainUnit);
            
            //sub.Remove(subUnit1);

            // Assert — проверка
            CollectionAssert.AreEqual(sub, unit.GetSubordinateUnits().ToList());
            Assert.IsFalse(ret);

            
        }
        #endregion

        #region Функция DeleteSubordinateUnit (Удалить подчиненное подразделение)
        /// <summary>
        /// Правильные параметры
        /// </summary>
        [TestMethod()]
        public void DeleteSubordinateUnit_WithValidArguments_DeletePositionAndTrueReturned()
        {
            

            // Arrange(настройка)
            var sub = unit.GetSubordinateUnits().ToList();
            // Act — выполнение 
            
            var ret = unit.DeleteSubordinateUnit(subUnit1);
            
            sub.Remove(subUnit1);

            // Assert — проверка
            
            CollectionAssert.AreEqual(sub, unit.GetSubordinateUnits().ToList());
            
            Assert.IsTrue(ret);

            
        }

        /// <summary>
        /// Нельзя удалить подчиненное подразделение если оно null
        /// </summary>
        [TestMethod()]
        public void DeleteSubordinateUnit_WhenDeleteUnitIsNull_FalseReturned()
        {
            

            // Arrange(настройка)
            var sub = unit.GetSubordinateUnits().ToList();
            // Act — выполнение 
            
            var ret = unit.DeleteSubordinateUnit(null);
            
            //sub.Remove(subUnit1);

            // Assert — проверка
            
            CollectionAssert.AreEqual(sub, unit.GetSubordinateUnits().ToList());
            
            Assert.IsFalse(ret);

            
        }

        /// <summary>
        /// Нельзя удалить подчиненное подразделение если его нет в списке подчиненных
        /// </summary>
        [TestMethod()]
        public void DeleteSubordinateUnit_WhenDeleteUnitNotInSubUnit_FalseReturned()
        {
            

            // Arrange(настройка)
            var sub = unit.GetSubordinateUnits().ToList();
            // Act — выполнение 
            
            var ret = unit.DeleteSubordinateUnit(mainUnit);
            
            //sub.Remove(subUnit1);

            // Assert — проверка
            
            CollectionAssert.AreEqual(sub, unit.GetSubordinateUnits().ToList());
            
            Assert.IsFalse(ret);

            
        }
        #endregion

    }
}