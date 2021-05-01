using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace PersonnelRecord.BL.Classes.Units.Tests
{
    [TestClass()]
    public class Unit_AddPosition_Tests
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



        #region Функция IsPossibleAddPosition (Проверка на Добавить должность)
        [TestMethod()]
        public void IsPossibleAddPosition_WithValidArguments_NullReturned()
        {

            // Arrange(настройка)
            var NewPosition = "Pos3";
            var Positions = unit.GetPositions().Select(x => x.GetName()).ToList();
            

            // Act — выполнение 
            var ret = unit.IsPossibleAddPosition(NewPosition);

            // Assert — проверка
            CollectionAssert.AreEqual(Positions, unit.GetPositions().Select(x => x.GetName()).ToList());
            Assert.IsNull(ret);

        }

        [DataTestMethod()]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow("  ")]
        [DataRow("\n")]
        [DataRow("\t")]
        public void IsPossibleAddPosition_WhenPositionEmpty_NotNullReturned(string NewPosition)
        {

            // Arrange(настройка)
            var Positions = unit.GetPositions().Select(x => x.GetName()).ToList();


            // Act — выполнение 
            var ret = unit.IsPossibleAddPosition(NewPosition);

            // Assert — проверка
            CollectionAssert.AreEqual(Positions, unit.GetPositions().Select(x => x.GetName()).ToList());
            Assert.IsNotNull(ret);

            
        }
        #endregion

        #region Функция AddPosition (Добавить должность)
        [TestMethod()]
        public void AddPosition_WithValidArguments_AddPositionAndTrueReturned()
        {
            

            // Arrange(настройка)
            var NewPosition = "Pos3";
            var Positions = unit.GetPositions().Select(x => x.GetName()).ToList();
            Positions.Add(NewPosition);
           

            // Act — выполнение 
           
            var ret = unit.AddPosition(NewPosition);
            

            // Assert — проверка
            CollectionAssert.AreEqual(Positions, unit.GetPositions().Select(x => x.GetName()).ToList());
            Assert.IsTrue(ret);

            
        }

        [DataTestMethod()]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow("  ")]
        [DataRow("\n")]
        [DataRow("\t")]
        public void AddPosition_WhenPositionEmpty_NotAddPositionAndFalseReturned(string NewPosition)
        {

            // Arrange(настройка)
            var Positions = unit.GetPositions().Select(x => x.GetName()).ToList();
           

            // Act — выполнение 
           
            var ret = unit.AddPosition(NewPosition);
            

            // Assert — проверка
            CollectionAssert.AreEqual(Positions, unit.GetPositions().Select(x => x.GetName()).ToList());
            Assert.IsFalse(ret);

        }
        #endregion

    }
}