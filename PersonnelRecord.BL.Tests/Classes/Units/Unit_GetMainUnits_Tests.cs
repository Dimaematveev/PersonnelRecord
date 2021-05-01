using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace PersonnelRecord.BL.Classes.Units.Tests
{
    /// <summary>
    /// Тестирование "Получить все главные подразделения"
    /// </summary>
    [TestClass()]
    public class Unit_GetMainUnits_Tests
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


        #region Функция GetMainUnits (Получить все главные подразделения)
        /// <summary>
        /// Правильные параметры
        /// </summary>
        [TestMethod()]
        public void GetMainUnits_WithValidArguments_ListIUnitReturned()
        {

            // Arrange(настройка)
            Unit MainUnit1 = new Unit("1", new List<string>() { "2" }, true);
            Unit unit1 = new Unit("2", new List<string>() { "2" });
            Unit Subunit1 = new Unit("3", new List<string>() { "2" });

            unit1.Reassignment(MainUnit1);
            Subunit1.Reassignment(unit1);
            List<Unit> MainUnits = new List<Unit>() { unit1, MainUnit1 };

            // Act — выполнение 
            var ret = Subunit1.GetMainUnits();

            // Assert — проверка
            CollectionAssert.AreEqual(MainUnits, ret.ToList());
        }
        #endregion


    }
}