using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace PersonnelRecord.BL.Classes.Units.Tests
{
    [TestClass()]
    public class Unit_Rename_Tests
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



        #region Функция Rename (Переименование)
        [TestMethod()]
        public void Rename_WithValidArguments_RenameAndTrueReterned()
        {

            // Arrange(настройка)
            string newName = "Unit2";

            // Act — выполнение 
            bool ret = unit.Rename(newName);

            // Assert — проверка
            Assert.AreEqual(newName, unit.GetName());
            Assert.IsTrue(ret);

        }

        [DataTestMethod()]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow("  ")]
        [DataRow("\n")]
        [DataRow("\t")]
        public void Rename_WhenNameEmpty_NotRenameAndFalseReterned(string newName)
        {

            // Arrange(настройка)
            var oldName = unit.GetName();

            // Act — выполнение 
            bool ret = unit.Rename(newName);

            // Assert — проверка
            Assert.AreEqual(oldName, unit.GetName());
            Assert.IsFalse(ret);

        }

        [TestMethod()]
        public void Rename_WhenOldNameEqalNewName_NotRenameAndFalseReterned()
        {

            // Arrange(настройка)
            var oldName = unit.GetName();
            var newName = unit.GetName();

            // Act — выполнение 
            bool ret = unit.Rename(newName);

            // Assert — проверка
            Assert.AreEqual(oldName, unit.GetName());
            Assert.IsFalse(ret);

        }
        #endregion

    }
}