using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace PersonnelRecord.BL.Classes.Positions.Tests
{
    /// <summary>
    /// Тестирование "Снять с должности"
    /// </summary>
    [TestClass()]
    public class Position_RemoveFromPosition_Tests
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



        #region RemoveFromPosition (Снять с должности)
        /// <summary>
        /// Правильные параметры
        /// </summary>
        [TestMethod()]
        public void RemoveFromPosition_WithValidArguments_NotBusyAndTrueReterned()
        {
            position.BusyPosition();

            //ACT
            var ret = position.RemoveFromPosition();
            //Assert
            Assert.IsTrue(ret);
            Assert.IsFalse(position.GetIsPositionBusy());
        }
        

        /// <summary>
        /// Когда должность Снята
        /// </summary>
        [TestMethod()]
        public void RemoveFromPosition_WhenPositionIsBusy_FalseReterned()
        {
            position.BusyPosition();
            position.RemoveFromPosition();
            //ACT
            bool ret = position.RemoveFromPosition();

            //Assert
            Assert.IsFalse(ret);
            Assert.IsFalse(position.GetIsDelete());
            Assert.IsFalse(position.GetIsPositionBusy());
        }

        /// <summary>
        /// Когда  удалено
        /// </summary>
        [TestMethod()]
        public void RemoveFromPosition_WhenPositionIsDelete_FalseReterned()
        {
            position.BusyPosition();
            position.RemoveFromPosition();
            position.Delete();
            //ACT

            bool ret = position.RemoveFromPosition();

            //Assert
            Assert.IsFalse(ret);
            Assert.IsTrue(position.GetIsDelete());
            Assert.IsFalse(position.GetIsPositionBusy());
        }
        #endregion

    }
}