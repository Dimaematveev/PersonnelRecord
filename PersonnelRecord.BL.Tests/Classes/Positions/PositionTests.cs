using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PersonnelRecord.BL.Classes.Positions.Tests
{
    [TestClass()]
    public class PositionTests
    {
        private string namePosition;
        private Position position;
        private Unit unit;
        #region Первоначальная настройка
        [TestInitialize]
        public void TestInitialize()
        {
            Debug.WriteLine("Настройка Должности");
            //Arrange (настройка) — в этом блоке кода мы настраиваем 
            //тестовое окружение тестируемого юнита;
            var nameUnit = "Unit1";
            Debug.WriteLine("nameUnit = 'Unit1'");
            namePosition = "Pos1";
            Debug.WriteLine("positionsName = 'Pos1'");
            unit = new Unit(nameUnit, new List<string>() { namePosition });
            Debug.WriteLine("Создали класс unit");

            // Act — выполнение или вызов тестируемого сценария;
            Debug.WriteLine("Выделяем класс Position");
            position = (Position)(unit.GetPositions().FirstOrDefault());
            Debug.WriteLine("Выделили класс Position");

            Debug.WriteLine("Настройка закончена");
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

        #region BusyPosition (Занять должность)
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

        #region RemoveFromPosition (Снять с должности)
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
        /// Когда Уже удалено
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