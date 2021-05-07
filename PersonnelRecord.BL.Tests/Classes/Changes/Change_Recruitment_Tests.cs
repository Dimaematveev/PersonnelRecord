using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersonnelRecord.BL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonnelRecord.BL.Classes.Changes.Tests
{
    /// <summary>
    /// Тестирование "Нанять на должность"
    /// </summary>
    [TestClass()]
    public class Change_Recruitment_Tests
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


        #region Recruitment (Нанять на должность)
        /// <summary>
        /// Правильные параметры на основную должность
        /// </summary>
        [TestMethod()]
        public void Recruitment_WithValidArgumentsMainPos_NewChangeReterned()
        {
            //Arrange
            Change NewChange;
            int NumOrd = 1;
            //ACT
            NewChange = Change.Recruitment(NumOrd, employee1, N1Pos1, false);

            //Assert
            Assert.IsTrue(Math.Abs(DateTime.Now.Ticks - NewChange.GetDateChange().Ticks)< 10000000);

            Assert.IsNull(NewChange.GetPreviousChange());
            Assert.AreEqual(NumOrd, NewChange.GetNumberOrder());
            Assert.AreEqual(N1Pos1, NewChange.GetPosition());
            Assert.AreEqual(employee1, NewChange.GetEmployee());
            Assert.IsFalse(NewChange.GetIsCombination());
            Assert.IsTrue(NewChange.GetStatus());
            Assert.AreEqual(RecordType.Найм, NewChange.GetRecordType());

        }
        /// <summary>
        /// Правильные параметры на совмещенную должность
        /// </summary>
        [TestMethod()]
        public void Recruitment_WithValidArgumentsComPos_NewChangeReterned()
        {
            //Arrange
            Change NewChange;
            int NumOrd = 1;
            //ACT
            NewChange = Change.Recruitment(NumOrd, employee1, N1Pos1, true);

            //Assert 
            Assert.IsTrue(Math.Abs(DateTime.Now.Ticks - NewChange.GetDateChange().Ticks) < 10000000);

            Assert.IsNull(NewChange.GetPreviousChange());
            Assert.AreEqual(NumOrd, NewChange.GetNumberOrder());
            Assert.AreEqual(N1Pos1, NewChange.GetPosition());
            Assert.AreEqual(employee1, NewChange.GetEmployee());
            Assert.IsTrue(NewChange.GetIsCombination());
            Assert.IsTrue(NewChange.GetStatus());
            Assert.AreEqual(RecordType.Найм, NewChange.GetRecordType());

        }
        /// <summary>
        /// Исключение на передачу в номер приказа 0 или меньше
        /// </summary>
        /// <param name="NumOrd">Номер приказа</param>
        [ExpectedException(typeof(ArgumentException), "Исключение на передачу в номер приказа 0 или меньше, не было вызвано.")]
        [DataTestMethod()]
        [DataRow(-1)]
        [DataRow(0)]
        public void Recruitment_WhenNumberOrder0OrLess_ExceptionReterned(int NumOrd)
        {
            //Arrange
            //ACT
            Change.Recruitment(NumOrd, employee1, N1Pos1, false);

        }

        /// <summary>
        /// Исключение на передачу в сотрудника null
        /// </summary>
        [ExpectedException(typeof(ArgumentNullException), "Исключение на передачу в сотрудника null, не было вызвано.")]
        [TestMethod()]
        public void Recruitment_WhenEmployeeIsNullMainPos_ExceptionReterned()
        {
            //Arrange
            int NumOrd = 1;
            //ACT
            Change.Recruitment(NumOrd, null, N1Pos1, false);

            //Assert
        }
        /// <summary>
        /// Исключение на передачу в должность null
        /// </summary>
        [ExpectedException(typeof(ArgumentNullException), "Исключение на передачу в должность null, не было вызвано.")]
        [TestMethod()]
        public void Recruitment_WhenPositionIsNull_ExceptionReterned()
        {
            //Arrange
            int NumOrd = 1;
            //ACT
            Change.Recruitment(NumOrd, employee1, null, false);

            //Assert
        }

        /// <summary>
        /// Исключение на передачу в должность занятой должности
        /// </summary>
        [ExpectedException(typeof(ArgumentException), "Исключение на передачу в должность занятой должности, не было вызвано.")]
        [TestMethod()]
        public void Recruitment_WhenPositionIsBusy_ExceptionReterned()
        {
            //Arrange
            int NumOrd = 1;
            N1Pos1.BusyPosition();
            //ACT
            Change.Recruitment(NumOrd, employee1, N1Pos1, false);

            //Assert
        }

        #endregion



    }
}