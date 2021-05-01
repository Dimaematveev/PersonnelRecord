using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersonnelRecord.BL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonnelRecord.BL.Classes.Changes.Tests
{
    [TestClass()]
    public class ChangeConstructorTests
    {

        private Employee employee1, employee2;

        private Position N1Pos1, N1Pos2, N2Pos1, N2Pos2;

        #region Первоначальная настройка
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
        [TestMethod()]
        public void Recruitment_WithValidArgumentsMainPos_NewChangeReterned()
        {
            //Arrange
            Change NewChange;
            int NumOrd = 1;
            //ACT
            NewChange = Change.Recruitment(NumOrd, employee1, N1Pos1, false);

            //Assert
            //TODO:Что делать с ID
            // Assert.AreEqual(1,NewChange.GetID());
            Assert.IsTrue(Math.Abs(DateTime.Now.Ticks - NewChange.GetDateChange().Ticks)< 10000000);

            Assert.IsNull(NewChange.GetPreviousChange());
            Assert.AreEqual(NumOrd, NewChange.GetNumberOrder());
            Assert.AreEqual(N1Pos1, NewChange.GetPosition());
            Assert.AreEqual(employee1, NewChange.GetEmployee());
            Assert.IsFalse(NewChange.GetIsCombination());
            Assert.IsTrue(NewChange.GetStatus());
            Assert.AreEqual(RecordType.Найм, NewChange.GetRecordType());

        }
        [TestMethod()]
        public void Recruitment_WithValidArgumentsComPos_NewChangeReterned()
        {
            //Arrange
            Change NewChange;
            int NumOrd = 1;
            //ACT
            NewChange = Change.Recruitment(NumOrd, employee1, N1Pos1, true);

            //Assert 
            //TODO:Что делать с ID
            // Assert.AreEqual(1, NewChange.GetID());
            Assert.IsTrue(Math.Abs(DateTime.Now.Ticks - NewChange.GetDateChange().Ticks) < 10000000);

            Assert.IsNull(NewChange.GetPreviousChange());
            Assert.AreEqual(NumOrd, NewChange.GetNumberOrder());
            Assert.AreEqual(N1Pos1, NewChange.GetPosition());
            Assert.AreEqual(employee1, NewChange.GetEmployee());
            Assert.IsTrue(NewChange.GetIsCombination());
            Assert.IsTrue(NewChange.GetStatus());
            Assert.AreEqual(RecordType.Найм, NewChange.GetRecordType());

        }

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

       
        [ExpectedException(typeof(ArgumentNullException), "Исключение на передачу в должность занятой должности, не было вызвано.")]
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


        #region Transfer (Изменить должность)
        [TestMethod()]
        public void Transfer_WithValidArgumentsComPosition_NewChangeReterned()
        {
            //Arrange
            Change OldChange, NewChange;
            int NumOrd = 1;
            OldChange = Change.Recruitment(NumOrd, employee1, N1Pos1, true);

            //ACT
            NumOrd = 2;
            NewChange = Change.Transfer(NumOrd, employee1, OldChange, N1Pos2);

            //Assert
            Assert.IsTrue(Math.Abs(DateTime.Now.Ticks - NewChange.GetDateChange().Ticks) < 10000000);

            Assert.AreEqual(OldChange, NewChange.GetPreviousChange());
            Assert.AreEqual(NumOrd, NewChange.GetNumberOrder());
            Assert.AreEqual(N1Pos2, NewChange.GetPosition());
            Assert.AreEqual(employee1, NewChange.GetEmployee());
            Assert.IsTrue(NewChange.GetIsCombination());
            Assert.IsTrue(NewChange.GetStatus());
            Assert.AreEqual(RecordType.Изменение, NewChange.GetRecordType());

            Assert.IsFalse(OldChange.GetStatus());

        }

        [TestMethod()]
        public void Transfer_WithValidArgumentsMainPosition_NewChangeReterned()
        {
            //Arrange
            Change OldChange, NewChange;
            int NumOrd = 1;
            OldChange = Change.Recruitment(NumOrd, employee1, N1Pos1, false);

            //ACT
            NumOrd = 2;
            NewChange = Change.Transfer(NumOrd, employee1, OldChange, N1Pos2);

            //Assert
            Assert.IsTrue(Math.Abs(DateTime.Now.Ticks - NewChange.GetDateChange().Ticks) < 10000000);

            Assert.AreEqual(OldChange, NewChange.GetPreviousChange());
            Assert.AreEqual(NumOrd, NewChange.GetNumberOrder());
            Assert.AreEqual(N1Pos2, NewChange.GetPosition());
            Assert.AreEqual(employee1, NewChange.GetEmployee());
            Assert.IsFalse(NewChange.GetIsCombination());
            Assert.IsTrue(NewChange.GetStatus());
            Assert.AreEqual(RecordType.Изменение, NewChange.GetRecordType());

            Assert.IsFalse(OldChange.GetStatus());

        }

        [ExpectedException(typeof(ArgumentException), "Исключение на передачу в номер приказа 0 или меньше, не было вызвано.")]
        [DataTestMethod()]
        [DataRow(-1)]
        [DataRow(0)]
        public void ChangePosition_WhenNumberOrder0OrLess_ExceptionReterned(int NumOrd)
        {
            //Arrange
            Change OldChange, NewChange;
            OldChange = Change.Recruitment(1, employee1, N1Pos1, false);

            //ACT
            NewChange = Change.Transfer(NumOrd, employee1, OldChange, N1Pos2);


            //Assert
        }

        [ExpectedException(typeof(ArgumentNullException), "Исключение на передачу сотрудник не может быть null, не было вызвано.")]
        [TestMethod()]
        public void ChangePosition_WhenEmployeeIsNull_ExceptionReterned()
        {
            //Arrange
            Change OldChange, NewChange;
            int NumOrd = 1;
            OldChange = Change.Recruitment(1, employee1, N1Pos1, false);
            NumOrd = 2;
            //ACT
            NewChange = Change.Transfer(NumOrd, null, OldChange, N1Pos2);


            //Assert
        }

        [ExpectedException(typeof(ArgumentNullException), "Исключение на передачу старая динамика не может быть null, не было вызвано.")]
        [TestMethod()]
        public void ChangePosition_WhenOldChangeIsNull_ExceptionReterned()
        {
            //Arrange
            Change OldChange, NewChange;
            int NumOrd = 1;
            OldChange = Change.Recruitment(1, employee1, N1Pos1, false);
            NumOrd = 2;
            //ACT
            NewChange = Change.Transfer(NumOrd, employee1, null, N1Pos2);


            //Assert
        }


        [ExpectedException(typeof(ArgumentNullException), "Исключение на передачу в новую должность null, не было вызвано.")]
        [TestMethod()]
        public void ChangePosition_WhenNewPositionIsNull_ExceptionReterned()
        {
            //Arrange
            Change OldChange, NewChange;
            int NumOrd = 1;
            OldChange = Change.Recruitment(1, employee1, N1Pos1, false);
            NumOrd = 2;
            //ACT
            NewChange = Change.Transfer(NumOrd, employee1, OldChange, null);


            //Assert
        }
        [ExpectedException(typeof(ArgumentException), "Исключение на передачу в старую динамику динамики другого сотрудника, не было вызвано.")]
        [TestMethod()]
        public void ChangePosition_WhenOldChangeNoEmployee_ExceptionReterned()
        {
            //Arrange
            Change OldChange, NewChange;
            int NumOrd = 1;
            OldChange = Change.Recruitment(1, employee2, N1Pos1, false);
            NumOrd = 2;
            //ACT
            NewChange = Change.Transfer(NumOrd, employee1, OldChange, N2Pos2);


            //Assert
        }


        [ExpectedException(typeof(ArgumentException), "Исключение на передачу в новую должность старой должности, не было вызвано.")]
        [TestMethod()]
        public void ChangePosition_WhenNewPositionEqualOldPosition_ExceptionReterned()
        {
            //Arrange
            Change OldChange, NewChange;
            int NumOrd = 1;
            OldChange = Change.Recruitment(1, employee1, N1Pos1, false);
            NumOrd = 2;
            //ACT
            NewChange = Change.Transfer(NumOrd, employee1, OldChange, N1Pos1);


            //Assert
        }

        [ExpectedException(typeof(ArgumentException), "Исключение на передачу в новую должность занятой должности, не было вызвано.")]
        [TestMethod()]
        public void ChangePosition_WhenNewPositionIsBusy_ExceptionReterned()
        {
            //Arrange
            Change OldChange, NewChange;
            int NumOrd = 1;
            OldChange = Change.Recruitment(1, employee1, N1Pos1, false);
            NumOrd = 2;
            N1Pos2.BusyPosition();
            //ACT
            NewChange = Change.Transfer(NumOrd, employee1, OldChange, N1Pos2);


            //Assert
        }

        #endregion


        #region Dismissal (Уволить с должности)
        [TestMethod()]
        public void Dismissal_WithValidArgumentsMainPosition_NewChangeReterned()
        {
            //Arrange
            Change OldChange, NewChange;
            int NumOrd = 1;
            OldChange = Change.Recruitment(NumOrd, employee1, N1Pos1, false);

            //ACT
            NumOrd = 2;
            NewChange = Change.Dismissal(NumOrd, employee1, OldChange);

            //Assert
            Assert.IsTrue(Math.Abs(DateTime.Now.Ticks - NewChange.GetDateChange().Ticks) < 10000000);

            Assert.AreEqual(OldChange, NewChange.GetPreviousChange());
            Assert.AreEqual(NumOrd, NewChange.GetNumberOrder());
            Assert.IsNull(NewChange.GetPosition());
            Assert.AreEqual(employee1, NewChange.GetEmployee());
            Assert.IsFalse(NewChange.GetIsCombination());
            Assert.IsFalse(NewChange.GetStatus());
            Assert.AreEqual(RecordType.Увольнение, NewChange.GetRecordType());

            Assert.IsFalse(OldChange.GetStatus());

        }


        [TestMethod()]
        public void Dismissal_WithValidArgumentsSubPosition_NewChangeReterned()
        {
            //Arrange
            Change OldChange, NewChange;
            int NumOrd = 1;
            OldChange = Change.Recruitment(NumOrd, employee1, N1Pos1, true);

            //ACT
            NumOrd = 2;
            NewChange = Change.Dismissal(NumOrd, employee1, OldChange);

            //Assert
            Assert.IsTrue(Math.Abs(DateTime.Now.Ticks - NewChange.GetDateChange().Ticks) < 10000000);

            Assert.AreEqual(OldChange, NewChange.GetPreviousChange());
            Assert.AreEqual(NumOrd, NewChange.GetNumberOrder());
            Assert.IsNull(NewChange.GetPosition());
            Assert.AreEqual(employee1, NewChange.GetEmployee());
            Assert.IsTrue(NewChange.GetIsCombination());
            Assert.IsFalse(NewChange.GetStatus());
            Assert.AreEqual(RecordType.Увольнение, NewChange.GetRecordType());

            Assert.IsFalse(OldChange.GetStatus());

        }




        [ExpectedException(typeof(ArgumentException), "Исключение на передачу в номер приказа 0 или меньше, не было вызвано.")]
        [DataTestMethod()]
        [DataRow(-1)]
        [DataRow(0)]
        public void Dismissal_WhenNumberOrder0OrLess_ExceptionReterned(int NumOrd)
        {
            //Arrange
            Change OldChange, NewChange;
            OldChange = Change.Recruitment(1, employee1, N1Pos1, false);

            //ACT
            NewChange = Change.Dismissal(NumOrd, employee1, OldChange);


            //Assert
        }

        [ExpectedException(typeof(ArgumentNullException), "Исключение на передачу сотрудник не может быть null, не было вызвано.")]
        [TestMethod()]
        public void Dismissal_WhenEmployeeIsNull_ExceptionReterned()
        {
            //Arrange
            Change OldChange, NewChange;
            int NumOrd = 1;
            OldChange = Change.Recruitment(1, employee1, N1Pos1, false);
            NumOrd = 2;
            //ACT
            NewChange = Change.Dismissal(NumOrd, null, OldChange);


            //Assert
        }

        [ExpectedException(typeof(ArgumentNullException), "Исключение на передачу старая динамика не может быть null, не было вызвано.")]
        [TestMethod()]
        public void Dismissal_WhenOldChangeIsNull_ExceptionReterned()
        {
            //Arrange
            Change OldChange, NewChange;
            int NumOrd = 1;
            OldChange = Change.Recruitment(1, employee1, N1Pos1, false);
            NumOrd = 2;
            //ACT
            NewChange = Change.Dismissal(NumOrd, employee1, null);


            //Assert
        }

        [ExpectedException(typeof(ArgumentException), "Исключение на передачу в старую динамику динамики другого сотрудника, не было вызвано.")]
        [TestMethod()]
        public void Dismissal_WhenOldChangeNoEmployee_ExceptionReterned()
        {
            //Arrange
            Change OldChange, NewChange;
            int NumOrd = 1;
            OldChange = Change.Recruitment(1, employee2, N1Pos1, false);
            NumOrd = 2;
            //ACT
            NewChange = Change.Dismissal(NumOrd, employee1, OldChange);


            //Assert
        }



        #endregion


    }
}