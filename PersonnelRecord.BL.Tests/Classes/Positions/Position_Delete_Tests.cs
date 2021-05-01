using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace PersonnelRecord.BL.Classes.Positions.Tests
{
    [TestClass()]
    public class Position_Delete_Tests
    {
        private string namePosition;
        private Position position;
        private Unit unit;
        #region Первоначальная настройка
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


        #region IsPossibleDeletePosition (Возможно ли удалить должность)
        [TestMethod()]
        public void IsPossibleDeletePosition_WithValidArguments_TrueReterned()
        {
            //ACT
            bool ret = position.IsPossibleDeletePosition();

            //Assert
            Assert.IsTrue(ret);
            Assert.IsFalse(position.GetIsDelete());
            Assert.IsFalse(position.GetIsPositionBusy());
        }

        /// <summary>
        /// Когда должность занята
        /// </summary>
        [TestMethod()]
        public void IsPossibleDeletePosition_WhenPositionIsBusy_FalseReterned()
        {
            position.BusyPosition();
            //ACT
            bool ret = position.IsPossibleDeletePosition();

            //Assert
            Assert.IsFalse(ret);
            Assert.IsFalse(position.GetIsDelete());
            Assert.IsTrue(position.GetIsPositionBusy());
        }

        /// <summary>
        /// Когда Уже удалено
        /// </summary>
        [TestMethod()]
        public void IsPossibleDeletePosition_WhenPositionIsDelete_FalseReterned()
        {
            position.Delete();
            //ACT

            bool ret = position.IsPossibleDeletePosition();

            //Assert
            Assert.IsFalse(ret);
            Assert.IsTrue(position.GetIsDelete());
            Assert.IsFalse(position.GetIsPositionBusy());
        }
        #endregion
        
        #region Delete (Удаление должности)
        [TestMethod()]
        public void Delete_WithValidArguments_DeletedAndTrueReterned()
        {
            //ACT
            bool ret = position.Delete();

            //Assert
            Assert.IsTrue(ret);
            Assert.IsTrue(position.GetIsDelete());
            Assert.IsFalse(position.GetIsPositionBusy());
        }

        /// <summary>
        /// Когда должность занята
        /// </summary>
        [TestMethod()]
        public void Delete_WhenPositionIsBusy_FalseReterned()
        {
            position.BusyPosition();
            //ACT
            bool ret = position.Delete();

            //Assert
            Assert.IsFalse(ret);
            Assert.IsFalse(position.GetIsDelete());
            Assert.IsTrue(position.GetIsPositionBusy());
        }

        /// <summary>
        /// Когда Уже удалено
        /// </summary>
        [TestMethod()]
        public void Delete_WhenPositionIsDelete_FalseReterned()
        {
            position.Delete();
            //ACT

            bool ret = position.IsPossibleDeletePosition();

            //Assert
            Assert.IsFalse(ret);
            Assert.IsTrue(position.GetIsDelete());
            Assert.IsFalse(position.GetIsPositionBusy());
        }
        #endregion

       

    }
}