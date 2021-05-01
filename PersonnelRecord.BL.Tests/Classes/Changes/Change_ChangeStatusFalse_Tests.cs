using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonnelRecord.BL.Classes.Changes.Tests
{
    /// <summary>
    /// Тестирование "Изменить статус на false"
    /// </summary>
    [TestClass()]
    public class Change_ChangeStatusFalse_Tests
    {
        private Employee employee1, employee2;

        private Position N1Pos1, N1Pos2, N2Pos1, N2Pos2;

        #region Первоначальная настройка
        /// <summary>
        /// Вызывается перед каждым методом теста
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            Unit unit1, unit2;
            unit1 = new Unit("Name1", new List<string>() { "N1Pos1", "N1Pos2" });
            unit2 = new Unit("Name1", new List<string>() { "N2Pos1", "N2Pos2" });
            N1Pos1 = unit1.GetPositions().FirstOrDefault(x => x.GetName() == "N1Pos1");
            N1Pos2 = unit1.GetPositions().FirstOrDefault(x => x.GetName() == "N1Pos2");
            N2Pos1 = unit2.GetPositions().FirstOrDefault(x => x.GetName() == "N2Pos1");
            N2Pos2 = unit2.GetPositions().FirstOrDefault(x => x.GetName() == "N2Pos2");
            // Act — выполнение или вызов тестируемого сценария;
            employee1 = new Employee(1, "Name1", new DateTime(1994, 12, 1));
            employee2 = new Employee(2, "Name2", new DateTime(1994, 12, 2));

        }
        #endregion

        #region IsPossibleChangeStatusToFalse (Проверка возможно ли изменить статус на false)
        /// <summary>
        /// Правильные параметры
        /// </summary>
        [TestMethod()]
        public void IsPossibleChangeStatusToFalse_WithValidArguments_TrueReterned()
        {
            //Arrange
            Change NewChange;
            int NumOrd = 1;
            NewChange = Change.Recruitment(NumOrd, employee1, N1Pos1, false);
            //ACT
            var OldStatus = NewChange.GetStatus();
            var ret = NewChange.IsPossibleChangeStatusToFalse();

            //Assert
            Assert.IsTrue(ret);
            Assert.IsTrue(OldStatus);
            Assert.IsTrue(NewChange.GetStatus());
        }

        /// <summary>
        /// Нельзя поменять статус когда он уже False
        /// </summary>
        [TestMethod()]
        public void IsPossibleChangeStatusToFalse_WhenStatusFalse_FalseReterned()
        {
            //Arrange
            Change NewChange;
            int NumOrd = 1;
            NewChange = Change.Recruitment(NumOrd, employee1, N1Pos1, false);
            NewChange.ChangeStatusFalse();
            //ACT
            var OldStatus = NewChange.GetStatus();
            var ret = NewChange.IsPossibleChangeStatusToFalse();

            //Assert
            Assert.IsFalse(ret);
            Assert.IsFalse(OldStatus);
            Assert.IsFalse(NewChange.GetStatus());
        }

        #endregion

        #region ChangeStatusFalse (Изменить статус на false)
        /// <summary>
        /// Правильные параметры
        /// </summary>
        [TestMethod()]
        public void ChangeStatusFalse_WithValidArguments_TrueReterned()
        {
            //Arrange
            Change NewChange;
            int NumOrd = 1;
            NewChange = Change.Recruitment(NumOrd, employee1, N1Pos1, false);
            //ACT
            var OldStatus = NewChange.GetStatus();
            var ret = NewChange.ChangeStatusFalse();

            //Assert
            Assert.IsTrue(ret);
            Assert.IsTrue(OldStatus);
            Assert.IsFalse(NewChange.GetStatus());
        }

        /// <summary>
        /// Нельзя поменять статус когда он и так False
        /// </summary>
        [TestMethod()]
        public void ChangeStatusFalse_WhenStatusFalse_FalseReterned()
        {
            //Arrange
            Change NewChange;
            int NumOrd = 1;
            NewChange = Change.Recruitment(NumOrd, employee1, N1Pos1, false);
            NewChange.ChangeStatusFalse();
            //ACT
            var OldStatus = NewChange.GetStatus();
            var ret = NewChange.ChangeStatusFalse();

            //Assert
            Assert.IsFalse(ret);
            Assert.IsFalse(OldStatus);
            Assert.IsFalse(NewChange.GetStatus());
        }
        #endregion


    }
}