using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace PersonnelRecord.BL.Classes.Units.Tests
{
    /// <summary>
    /// Тестирование "Удалить подразделение"
    /// </summary>
    [TestClass()]
    public class Unit_Delete_Tests
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



        #region Функция IsPossibleDelete (Проверка на Удалить подразделение)
        /// <summary>
        /// Правильные параметры
        /// </summary>
        [TestMethod()]
        public void IsPossibleDelete_WithValidArguments_TrueReturned()
        {

            // Arrange(настройка)
            var hier = unit.GetHierarchyTier();
            var main = unit.GetMainUnit();
            var sub = unit.GetSubordinateUnits();

            // Act — выполнение 
            var ret = unit.IsPossibleDelete();
            
            // Assert — проверка
            Assert.IsFalse(unit.GetIsDelete());
            Assert.AreEqual(hier, unit.GetHierarchyTier());
            Assert.AreEqual(main,unit.GetMainUnit());
            foreach (var pos in unit.GetPositions())
            {
                Assert.IsFalse(pos.GetIsDelete());
            }
            CollectionAssert.AreEqual(sub.ToList(), unit.GetSubordinateUnits().ToList());
            Assert.AreEqual(unit, subUnit1.GetMainUnit());
            Assert.AreEqual(unit, subUnit2.GetMainUnit());           
            Assert.IsTrue(ret);           
        }

        /// <summary>
        /// Нельзя удалить подразделение из списка пока нельзя реально удалить все должности)
        /// </summary>
        [TestMethod()]
        public void IsPossibleDelete_WhenPosCannotBeDeleted_FalseReturned()
        {
            // Arrange(настройка)
            var hier = unit.GetHierarchyTier();
            var main = unit.GetMainUnit();
            var sub = unit.GetSubordinateUnits();
            unit.GetPositions()[0].BusyPosition();

            // Act — выполнение 
            var ret = unit.IsPossibleDelete();

            // Assert — проверка
            Assert.IsFalse(unit.GetIsDelete());
            Assert.AreEqual(hier, unit.GetHierarchyTier());           
            Assert.AreEqual(main, unit.GetMainUnit());
            foreach (var pos in unit.GetPositions())
            {               
                Assert.IsFalse(pos.GetIsDelete());
            }            
            CollectionAssert.AreEqual(sub.ToList(), unit.GetSubordinateUnits().ToList());            
            Assert.AreEqual(unit, subUnit1.GetMainUnit());            
            Assert.AreEqual(unit, subUnit2.GetMainUnit());            
            Assert.IsFalse(ret);           
        }

        /// <summary>
        /// Нельзя удалить должность когда подчиненные подразделения нельзя переподчинить
        /// </summary>
        /// <remarks>
        /// Здесь пытаюсь удалить главное подразделение но так как у главного у единственного null в главном, 
        /// и соответственно мы не можем переподчинить дочерние из-за этого
        /// </remarks>
        [TestMethod()]
        public void IsPossibleDelete_WhenPosCannotBeReassignmentSub_FalseReturned()
        {
            

            // Arrange(настройка)
            var hier = mainUnit.GetHierarchyTier();
            var main = mainUnit.GetMainUnit();
            var sub = mainUnit.GetSubordinateUnits();
            // Act — выполнение 
            
            var ret = mainUnit.IsPossibleDelete();
            

            // Assert — проверка
            
            Assert.IsFalse(mainUnit.GetIsDelete());

            
            Assert.AreEqual(hier, mainUnit.GetHierarchyTier());
            
            Assert.AreEqual(main, mainUnit.GetMainUnit());

            foreach (var pos in mainUnit.GetPositions())
            {
                
                Assert.IsFalse(pos.GetIsDelete());
            }

            
            CollectionAssert.AreEqual(sub.ToList(), mainUnit.GetSubordinateUnits().ToList());
            
            Assert.AreEqual(mainUnit, unit.GetMainUnit());

            
            Assert.IsFalse(ret);

            
        }
        #endregion

        #region Функция Delete (Удалить подразделение)
        /// <summary>
        /// Правильные параметры
        /// </summary>
        [TestMethod()]
        public void Delete_WithValidArguments_DeleteAndTrueReturned()
        {
            

            // Arrange(настройка)
            // Act — выполнение 
            
            var ret = unit.Delete();
            

            // Assert — проверка
            
            Assert.IsTrue(unit.GetIsDelete());
            
            Assert.AreEqual(0,unit.GetHierarchyTier());
            
            Assert.IsNull(unit.GetMainUnit());

            foreach (var pos in unit.GetPositions())
            {
                
                Assert.IsTrue(pos.GetIsDelete());
            }

            
            Assert.AreEqual(0, unit.GetSubordinateUnits().Count);
            
            Assert.AreEqual(mainUnit, subUnit1.GetMainUnit());
            
            Assert.AreEqual(mainUnit, subUnit2.GetMainUnit());

            
            Assert.IsTrue(ret);

            
        }

        /// <summary>
        /// Нельзя удалить подразделение из списка пока нельзя реально удалить все должности)
        /// </summary>
        [TestMethod()]
        public void Delete_WhenPosCannotBeDeleted_FalseReturned()
        {
            

            // Arrange(настройка)
            var hier = unit.GetHierarchyTier();
            var main = unit.GetMainUnit();
            var sub = unit.GetSubordinateUnits();
            unit.GetPositions()[0].BusyPosition();
            // Act — выполнение 
            
            var ret = unit.Delete();
            

            // Assert — проверка
            
            Assert.IsFalse(unit.GetIsDelete());

            
            Assert.AreEqual(hier, unit.GetHierarchyTier());
            
            Assert.AreEqual(main, unit.GetMainUnit());

            foreach (var pos in unit.GetPositions())
            {
                
                Assert.IsFalse(pos.GetIsDelete());
            }

            
            CollectionAssert.AreEqual(sub.ToList(), unit.GetSubordinateUnits().ToList());
            
            Assert.AreEqual(unit, subUnit1.GetMainUnit());
            
            Assert.AreEqual(unit, subUnit2.GetMainUnit());

            
            Assert.IsFalse(ret);

            
        }

        /// <summary>
        /// Нельзя удалить должность когда подчиненные подразделения нельзя переподчинить
        /// </summary>
        /// <remarks>
        /// Здесь пытаюсь удалить главное подразделение но так как у главного у единственного null в главном, 
        /// и соответственно мы не можем переподчинить дочерние из-за этого
        /// </remarks>
        [TestMethod()]
        public void Delete_WhenPosCannotBeReassignmentSub_FalseReturned()
        {
            

            // Arrange(настройка)
            var hier = mainUnit.GetHierarchyTier();
            var main = mainUnit.GetMainUnit();
            var sub = mainUnit.GetSubordinateUnits();

            // Act — выполнение 
            var ret = mainUnit.Delete();

            // Assert — проверка
            Assert.IsFalse(mainUnit.GetIsDelete());
            Assert.AreEqual(hier, mainUnit.GetHierarchyTier());
            Assert.AreEqual(main, mainUnit.GetMainUnit());
            foreach (var pos in mainUnit.GetPositions())
            {
                Assert.IsFalse(pos.GetIsDelete());
            }
            CollectionAssert.AreEqual(sub.ToList(), mainUnit.GetSubordinateUnits().ToList());
            Assert.AreEqual(mainUnit, unit.GetMainUnit());
            Assert.IsFalse(ret);
        }
        #endregion


    }
}