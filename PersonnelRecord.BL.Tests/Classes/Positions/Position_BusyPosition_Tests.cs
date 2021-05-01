using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace PersonnelRecord.BL.Classes.Positions.Tests
{
    /// <summary>
    /// Тестирование "Занять должность"
    /// </summary>
    [TestClass()]
    public class Position_BusyPosition_Tests
    {
        private string namePosition;
        private Position position;
        private Unit unit;
        #region Первоначальная настройка
        /// <summary>
        /// Вызывается перед каждым методом теста
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            //Arrange (настройка) — в этом блоке кода мы настраиваем 
            //тестовое окружение тестируемого юнита;
            var nameUnit = "Unit1";
            namePosition = "Pos1";
            unit = new Unit(nameUnit, new List<string>() { namePosition });

            // Act — выполнение или вызов тестируемого сценария;
            position = (Position)(unit.GetPositions().FirstOrDefault());

        }
        #endregion



        #region BusyPosition (Занять должность)
        /// <summary>
        /// Правильные параметры
        /// </summary>
        [TestMethod()]
        public void BusyPosition_WithValidArguments_BusyAndTrueReterned()
        {

            //ACT
            var ret = position.BusyPosition();
            //Assert
            Assert.IsTrue(ret);
            Assert.IsTrue(position.GetIsPositionBusy());
        }
        
        /// <summary>
        /// Когда должность занята
        /// </summary>
        [TestMethod()]
        public void BusyPosition_WhenPositionIsBusy_FalseReterned()
        {
            position.BusyPosition();
            //ACT
            bool ret = position.BusyPosition();

            //Assert
            Assert.IsFalse(ret);
            Assert.IsFalse(position.GetIsDelete());
            Assert.IsTrue(position.GetIsPositionBusy());
        }

        /// <summary>
        /// Когда Уже удалено
        /// </summary>
        [TestMethod()]
        public void BusyPosition_WhenPositionIsDelete_FalseReterned()
        {
            position.Delete();
            //ACT

            bool ret = position.BusyPosition();

            //Assert
            Assert.IsFalse(ret);
            Assert.IsTrue(position.GetIsDelete());
            Assert.IsFalse(position.GetIsPositionBusy());
        }
        #endregion

       

    }
}