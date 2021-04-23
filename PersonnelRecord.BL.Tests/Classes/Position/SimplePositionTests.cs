using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersonnelRecord.BL.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelRecord.BL.Classes.Position.Tests
{
    [TestClass()]
    public class SimplePositionTests
    {
        private string namePosition;
        private SimplePosition position;
        private SimpleUnit unit;
        #region Первоначальная настройка
        [TestInitialize]
        public void TestInitialize()
        {
            Debug.WriteLine("Настройка Должности");
            //Arrange (настройка) — в этом блоке кода мы настраиваем 
            //тестовое окружение тестируемого юнита;
            var nameUnit = "Unit1";
            Debug.WriteLine("nameUnit = 'Unit1'");
            namePosition = "Pos1" ;
            Debug.WriteLine("positionsName = 'Pos1'");
            unit = new SimpleUnit(nameUnit, new List<string>() { namePosition });
            Debug.WriteLine("Создали класс unit");

            // Act — выполнение или вызов тестируемого сценария;
            Debug.WriteLine("Выделяем класс Position");
            position = (SimplePosition)(unit.GetPositions().FirstOrDefault());
            Debug.WriteLine("Выделили класс Position");

            Debug.WriteLine("Настройка закончена");
        }
        #endregion


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
        [TestMethod()]
        public void Delete_WithNotValidArguments_NotDeletedAndFalseReterned()
        {
            position.BusyPosition();
            //ACT
            bool ret = position.Delete();

            //Assert
            Assert.IsFalse(ret);
            Assert.IsFalse(position.GetIsDelete());
            Assert.IsTrue(position.GetIsPositionBusy());
        }


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
        [TestMethod()]
        public void IsPossibleDeletePosition_WithNotValidArguments_FalseReterned()
        {
            position.BusyPosition();
            //ACT
            bool ret = position.IsPossibleDeletePosition();

            //Assert
            Assert.IsFalse(ret);
            Assert.IsFalse(position.GetIsDelete());
            Assert.IsTrue(position.GetIsPositionBusy());
        }

        [TestMethod()]
        public void BusyPosition_WithValidArguments_BusyAndTrueReterned()
        {

            //ACT
            var ret = position.BusyPosition();
            //Assert
            Assert.IsTrue(ret);
            Assert.IsTrue(position.GetIsPositionBusy());
        }
        [TestMethod()]
        public void BusyPosition_WithNotValidArguments_FalseReterned()
        {
            position.BusyPosition();
            //ACT
            var ret = position.BusyPosition();
            //Assert
            Assert.IsFalse(ret);
            Assert.IsTrue(position.GetIsPositionBusy());
        }
        [TestMethod()]
        public void NotBusyPosition_WithValidArguments_NotBusyAndTrueReterned()
        {
            position.BusyPosition();

            //ACT
            var ret = position.NotBusyPosition();
            //Assert
            Assert.IsTrue(ret);
            Assert.IsFalse(position.GetIsPositionBusy());
        }
        [TestMethod()]
        public void NotBusyPosition_WithNotValidArguments_FalseReterned()
        {
            position.BusyPosition();
            position.NotBusyPosition();
            //ACT
            var ret = position.NotBusyPosition();
            //Assert
            Assert.IsFalse(ret);
            Assert.IsFalse(position.GetIsPositionBusy());
        }

    }
}