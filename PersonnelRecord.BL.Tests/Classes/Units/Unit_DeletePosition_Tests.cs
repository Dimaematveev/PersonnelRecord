using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace PersonnelRecord.BL.Classes.Units.Tests
{
    /// <summary>
    /// Тестирование "Удалить должность подразделение"
    /// </summary>
    [TestClass()]
    public class Unit_DeletePosition_Tests
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




        #region Функция IsPossibleDeletePosition (проверка на Удалить должность)
        /// <summary>
        /// Правильные параметры
        /// </summary>
        [TestMethod()]
        public void IsPossibleDeletePosition_WithValidArguments_NullReturned()
        {

            // Arrange(настройка)
            var DeletePositionName = "Pos1";
            var DeletePosition = unit.GetPositions().FirstOrDefault(x => x.GetName() == DeletePositionName);
            var Positions = unit.GetPositions().ToList();
            //Positions.Remove(DeletePosition);

            // Act — выполнение 
            var ret = unit.IsPossibleDeletePosition(DeletePosition);

            // Assert — проверка
            CollectionAssert.AreEqual(Positions, unit.GetPositions().ToList());
            Assert.IsNull(ret);

        }

        /// <summary>
        /// нельзя удалить должность равную null
        /// </summary>
        [TestMethod()]
        public void IsPossibleDeletePosition_WhenPositionNull_NotNullReturned()
        {

            // Arrange(настройка)
            Position DeletePosition = null;
            var Positions = unit.GetPositions().ToList();

            // Act — выполнение 
            var ret = unit.IsPossibleDeletePosition(DeletePosition);

            // Assert — проверка
            CollectionAssert.AreEqual(Positions, unit.GetPositions().ToList());
            Assert.IsNotNull(ret);

        }

        /// <summary>
        /// Нельзя удалить должность которой нет в списке
        /// </summary>
        [TestMethod()]
        public void IsPossibleDeletePosition_WhenPositionNotInList_NotNullReturned()
        {

            // Arrange(настройка)
            var DeletePositionName = "Pos3";
            var unit2 = new Unit("unit2", new List<string>() { DeletePositionName });
            var DeletePosition = unit2.GetPositions().FirstOrDefault(x => x.GetName() == DeletePositionName);
            var Positions = unit.GetPositions().ToList();

            // Act — выполнение 
            var ret = unit.IsPossibleDeletePosition(DeletePosition);

            // Assert — проверка
            CollectionAssert.AreEqual(Positions, unit.GetPositions().ToList());
            Assert.IsNotNull(ret);

        }

        /// <summary>
        /// Нельзя удалить должность из списка если ее просто нельзя удалить
        /// </summary>
        [TestMethod()]
        public void IsPossibleDeletePosition_WhenPositionCannotBeDeleted_NotNullReturned()
        {
          
            // Arrange(настройка)
            var DeletePositionName = "Pos1";
            var DeletePosition = unit.GetPositions().FirstOrDefault(x => x.GetName() == DeletePositionName);
            DeletePosition.BusyPosition();
            var Positions = unit.GetPositions().ToList();
           
            // Act — выполнение 
            var ret = unit.IsPossibleDeletePosition(DeletePosition);
            
            // Assert — проверка
            CollectionAssert.AreEqual(Positions, unit.GetPositions().ToList());
            Assert.IsNotNull(ret);

            
        }
        #endregion

        #region Функция DeletePosition (Удалить должность)
        /// <summary>
        /// Правильные параметры
        /// </summary>
        [TestMethod()]
        public void DeletePosition_WithValidArguments_DeletePositionAndTrueReturned()
        {
            

            // Arrange(настройка)
            var DeletePositionName = "Pos1";
            var DeletePosition = unit.GetPositions().FirstOrDefault(x => x.GetName() == DeletePositionName);
            var Positions = unit.GetPositions().ToList();
            Positions.Remove(DeletePosition);
            

            // Act — выполнение 
            
            var ret = unit.DeletePosition(DeletePosition);
            

            // Assert — проверка
            
            CollectionAssert.AreEqual(Positions, unit.GetPositions().ToList());
            
            Assert.IsTrue(ret);

            
        }

        /// <summary>
        /// нельзя удалить должность равную null
        /// </summary>
        [TestMethod()]
        public void DeletePosition_WhenPositionNull_NotDeletePositionAndFalseReturned()
        {


            // Arrange(настройка)
            Position DeletePosition = null;
            var Positions = unit.GetPositions().ToList();


            // Act — выполнение 

            var ret = unit.DeletePosition(DeletePosition);


            // Assert — проверка

            CollectionAssert.AreEqual(Positions, unit.GetPositions().ToList());

            Assert.IsFalse(ret);


        }

        /// <summary>
        /// Нельзя удалить должность которой нет в списке
        /// </summary>
        [TestMethod()]
        public void DeletePosition_WhenPositionNotInList_NotDeletePositionAndFalseReturned()
        {
            

            // Arrange(настройка)
            var DeletePositionName = "Pos3";
            var unit2 = new Unit("unit2", new List<string>() { DeletePositionName });
            var DeletePosition = unit2.GetPositions().FirstOrDefault(x => x.GetName() == DeletePositionName);
            var Positions = unit.GetPositions().ToList();
            

            // Act — выполнение 
            
            var ret = unit.DeletePosition(DeletePosition);
            

            // Assert — проверка
            
            CollectionAssert.AreEqual(Positions, unit.GetPositions().ToList());
            
            Assert.IsFalse(ret);

            
        }

        /// <summary>
        /// Нельзя удалить должность из списка если ее просто нельзя удалить
        /// </summary>
        [TestMethod()]
        public void DeletePosition_WhenPositionCannotBeDeleted_NotNullReturned()
        {
            

            // Arrange(настройка)
            var DeletePositionName = "Pos1";
            var DeletePosition = unit.GetPositions().FirstOrDefault(x => x.GetName() == DeletePositionName);
            DeletePosition.BusyPosition();
            var Positions = unit.GetPositions().ToList();
            

            // Act — выполнение 
            
            var ret = unit.DeletePosition(DeletePosition);
            

            // Assert — проверка
            
            CollectionAssert.AreEqual(Positions, unit.GetPositions().ToList());
            
            Assert.IsFalse(ret);

            
        }
        #endregion

    }
}