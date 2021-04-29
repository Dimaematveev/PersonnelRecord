using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersonnelRecord.BL.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PersonnelRecord.BL.Classes.Employes.Tests
{
    [TestClass()]
    public class EmployeeTests
    {

        private string name;
        private DateTime birthday;
        private Employee employee;
        private Unit unit1, unit2;
        private Position N1Pos1, N1Pos2, N2Pos1, N2Pos2;

        #region Первоначальная настройка
        [TestInitialize]
        public void TestInitialize()
        {
            var id = 1;
            name = "Name1";
            birthday = new DateTime(1994, 12, 2);
            unit1 = new Unit("Name1", new List<string>() { "N1Pos1", "N1Pos2" });
            unit2 = new Unit("Name1", new List<string>() { "N2Pos1", "N2Pos2" });
            N1Pos1 = unit1.GetPositions().FirstOrDefault(x => x.GetName() == "N1Pos1");
            N1Pos2 = unit1.GetPositions().FirstOrDefault(x => x.GetName() == "N1Pos2");
            N2Pos1 = unit2.GetPositions().FirstOrDefault(x => x.GetName() == "N2Pos1");
            N2Pos2 = unit2.GetPositions().FirstOrDefault(x => x.GetName() == "N2Pos2");
            // Act — выполнение или вызов тестируемого сценария;
            employee = new Employee(id, name, birthday);
           
        }
        #endregion

        
        #region Recruitment (Принятие на работу)
        [TestMethod()]
        public void Recruitment_WithValidArguments_NewChangeAddChangesReterned()
        {
            //Arrange
            var Changes = new List<Change>();
            Change NewChange;
            List<Position> ListPositions = new List<Position>();
            int NumOrd = 1;

            //ACT
            NewChange = employee.Recruitment(NumOrd, N1Pos1);
            Changes.Add(NewChange);
            ListPositions.Add(N1Pos1);
          
            //Assert
            CollectionAssert.AreEqual(ListPositions, employee.GetListCurrentPositions().ToList());
            CollectionAssert.AreEqual(Changes, employee.GetChanges().ToList());

            Assert.IsNull(NewChange.GetPreviousChange());
            Assert.AreEqual(NumOrd, NewChange.GetNumberOrder());
            Assert.AreEqual(N1Pos1, NewChange.GetPosition());
            Assert.AreEqual(employee, NewChange.GetEmployee());
            Assert.IsFalse(NewChange.GetIsCombination());
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
            Change NewChange;

            //ACT
            NewChange = employee.Recruitment(NumOrd, N1Pos1);

            //Assert
        }

        [ExpectedException(typeof(ArgumentNullException), "Исключение на передачу в должность null, не было вызвано.")]
        [TestMethod()]
        public void Recruitment_WhenPositionIsNull_ExceptionReterned()
        {
            //Arrange
            Change NewChange;
            int NumOrd = 1;
            //ACT
            NewChange = employee.Recruitment(NumOrd, null);

            //Assert
        }

        [ExpectedException(typeof(ArgumentException), "Исключение на передачу должности в основную если уже есть основная, не было вызвано.")]
        [TestMethod()]
        public void Recruitment_WhenMainPositionBusy_ExceptionReterned()
        {
            //Arrange
            Change NewChange;
            int NumOrd = 1;
            NewChange = employee.Recruitment(NumOrd, N1Pos1);
            NumOrd = 2;
            //ACT
            NewChange = employee.Recruitment(NumOrd, N1Pos2);

            //Assert
        }

        [ExpectedException(typeof(ArgumentNullException), "Исключение на передачу в должность занятой должности, не было вызвано.")]
        [TestMethod()]
        public void Recruitment_WhenPositionIsBusy_ExceptionReterned()
        {
            //Arrange
            Change NewChange;
            int NumOrd = 1;
            N1Pos1.BusyPosition();
            //ACT
            NewChange = employee.Recruitment(NumOrd, N1Pos1);

            //Assert
        }
        #endregion


        #region AddPosition (Принятие на должность по совмещению)
        [TestMethod()]
        public void AddPosition_WithValidArguments_NewChangeAddChangesReterned()
        {
            //Arrange
            var Changes = new List<Change>();
            Change NewChange;
            List<Position> ListPositions = new List<Position>();
            int NumOrd = 1;
            NewChange = employee.Recruitment(NumOrd, N1Pos1);
            Changes.Add(NewChange);
            ListPositions.Add(N1Pos1);
            NumOrd = 2;
            //ACT
            NewChange = employee.AddPosition(NumOrd, N2Pos1);
            Changes.Add(NewChange);
            ListPositions.Add(N2Pos1);

            //Assert
            CollectionAssert.AreEqual(ListPositions, employee.GetListCurrentPositions().ToList());
            CollectionAssert.AreEqual(Changes, employee.GetChanges().ToList());

            Assert.IsNull(NewChange.GetPreviousChange());
            Assert.AreEqual(NumOrd, NewChange.GetNumberOrder());
            Assert.AreEqual(N2Pos1, NewChange.GetPosition());
            Assert.AreEqual(employee, NewChange.GetEmployee());
            Assert.IsTrue(NewChange.GetIsCombination());
            Assert.IsTrue(NewChange.GetStatus());
            Assert.AreEqual(RecordType.Найм, NewChange.GetRecordType());

        }

        [ExpectedException(typeof(ArgumentException), "Исключение на передачу в номер приказа 0 или меньше, не было вызвано.")]
        [DataTestMethod()]
        [DataRow(-1)]
        [DataRow(0)]
        public void AddPosition_WhenNumberOrder0OrLess_ExceptionReterned(int NumOrd)
        {
            //Arrange
            Change NewChange;
            NewChange = employee.Recruitment(1, N1Pos1);

            //ACT
            NewChange = employee.Recruitment(NumOrd, N2Pos2);

            //Assert
        }

        [ExpectedException(typeof(ArgumentNullException), "Исключение на передачу в должность null, не было вызвано.")]
        [TestMethod()]
        public void AddPosition_WhenPositionIsNull_ExceptionReterned()
        {
            //Arrange
            Change NewChange;
            int NumOrd = 1;
            NewChange = employee.Recruitment(NumOrd, N1Pos1);
            NumOrd = 2;
            //ACT
            NewChange = employee.AddPosition(NumOrd, null);

            //Assert
        }

        [ExpectedException(typeof(ArgumentException), "Исключение на передачу должности в совмещенную если нет основной, не было вызвано.")]
        [TestMethod()]
        public void AddPosition_WhenMainPositionNotBusy_ExceptionReterned()
        {
            //Arrange
            Change NewChange;
            int NumOrd = 1;
            //ACT
            NewChange = employee.AddPosition(NumOrd, N1Pos2);

            //Assert
        }

        [ExpectedException(typeof(ArgumentNullException), "Исключение на передачу в должность занятой должности, не было вызвано.")]
        [TestMethod()]
        public void AddPosition_WhenPositionIsBusy_ExceptionReterned()
        {
            //Arrange
            Change NewChange;
            int NumOrd = 1;
            NewChange = employee.Recruitment(NumOrd, N1Pos1);
            NumOrd = 2;
            N1Pos2.BusyPosition();
            //ACT
            NewChange = employee.AddPosition(NumOrd, N1Pos2);

            //Assert
        }
        #endregion


        #region ChangePosition (Изменить должность)
        [TestMethod()]
        public void ChangePosition_WithValidArgumentsMainPosition_NewChangeAddChangesReterned()
        {
            //Arrange
            var Changes = new List<Change>();
            Change NewChange, OldChange;
            List<Position> ListPositions = new List<Position>();
            int NumOrd = 1;
            OldChange = employee.Recruitment(NumOrd, N1Pos1);
            Changes.Add(OldChange);
            NumOrd = 2;

            //ACT
            NewChange = employee.ChangePosition(NumOrd, N1Pos1, N1Pos2);
            Changes.Add(NewChange);
            ListPositions.Add(N1Pos2);

            //Assert
            CollectionAssert.AreEqual(ListPositions, employee.GetListCurrentPositions().ToList());
            CollectionAssert.AreEqual(Changes, employee.GetChanges().ToList());

            Assert.AreEqual(OldChange, NewChange.GetPreviousChange());
            Assert.AreEqual(NumOrd, NewChange.GetNumberOrder());
            Assert.AreEqual(N1Pos2, NewChange.GetPosition());
            Assert.AreEqual(employee, NewChange.GetEmployee());
            Assert.IsFalse(NewChange.GetIsCombination());
            Assert.IsTrue(NewChange.GetStatus());
            Assert.AreEqual(RecordType.Изменение, NewChange.GetRecordType());

            Assert.IsFalse(OldChange.GetStatus());
        }

        [TestMethod()]
        public void ChangePosition_WithValidArgumentsComPosition_NewChangeAddChangesReterned()
        {
            //Arrange
            var Changes = new List<Change>();
            Change NewChange, OldChange;
            List<Position> ListPositions = new List<Position>();
            int NumOrd = 1;
            OldChange = employee.Recruitment(NumOrd, N1Pos1);
            Changes.Add(OldChange);
            ListPositions.Add(N1Pos1);

            NumOrd = 2;
            OldChange = employee.AddPosition(NumOrd, N2Pos1);
            Changes.Add(OldChange);
            NumOrd = 3;
            //ACT
            NewChange = employee.ChangePosition(NumOrd, N2Pos1, N2Pos2);
            Changes.Add(NewChange);
            ListPositions.Add(N2Pos2);

            //Assert
            CollectionAssert.AreEqual(ListPositions, employee.GetListCurrentPositions().ToList());
            CollectionAssert.AreEqual(Changes, employee.GetChanges().ToList());

            Assert.AreEqual(OldChange, NewChange.GetPreviousChange());
            Assert.AreEqual(NumOrd, NewChange.GetNumberOrder());
            Assert.AreEqual(N2Pos2, NewChange.GetPosition());
            Assert.AreEqual(employee, NewChange.GetEmployee());
            Assert.IsTrue(NewChange.GetIsCombination());
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
            Change NewChange, OldChange;
            OldChange = employee.Recruitment(1, N1Pos1);
            //ACT
            NewChange = employee.ChangePosition(NumOrd, N1Pos1, N1Pos2);

            //Assert
        }

        [ExpectedException(typeof(ArgumentNullException), "Исключение на передачу в новую должность null, не было вызвано.")]
        [TestMethod()]
        public void ChangePosition_WhenNewPositionIsNull_ExceptionReterned()
        {
            //Arrange
            Change NewChange, OldChange;
            int NumOrd = 1;
            OldChange = employee.Recruitment(NumOrd, N1Pos1);
            NumOrd = 2;
            //ACT
            NewChange = employee.ChangePosition(NumOrd, N1Pos1, null);

            //Asser
        }

        [ExpectedException(typeof(ArgumentNullException), "Исключение на передачу в новую должность null, не было вызвано.")]
        [TestMethod()]
        public void ChangePosition_WhenOldPositionIsNull_ExceptionReterned()
        {
            //Arrange
            Change NewChange, OldChange;
            int NumOrd = 1;
            OldChange = employee.Recruitment(NumOrd, N1Pos1);
            NumOrd = 2;
            //ACT
            NewChange = employee.ChangePosition(NumOrd, null, N1Pos2);

            //Asser
        }

        [ExpectedException(typeof(ArgumentNullException), "Исключение на передачу в новую должность занятой должности, не было вызвано.")]
        [TestMethod()]
        public void ChangePosition_WhenNewPositionIsBusy_ExceptionReterned()
        {
            //Arrange
            Change NewChange, OldChange;
            int NumOrd = 1;
            OldChange = employee.Recruitment(NumOrd, N1Pos1);
            NumOrd = 2;
            N1Pos2.BusyPosition();
            //ACT
            NewChange = employee.ChangePosition(NumOrd, N1Pos1, N1Pos2);
            //Assert
        }

        [ExpectedException(typeof(ArgumentException), "Исключение на передачу в старую должность должность которой нет, не было вызвано.")]
        [TestMethod()]
        public void ChangePosition_WhenOldPositionIsNot_ExceptionReterned()
        {
            //Arrange
            Change NewChange, OldChange;
            int NumOrd = 1;
            OldChange = employee.Recruitment(NumOrd, N1Pos1);
            NumOrd = 2;
            //ACT
            NewChange = employee.ChangePosition(NumOrd, N2Pos1, N1Pos2);
            //Assert
        }
        #endregion


        #region Dismissal (Увольнение)
        [TestMethod()]
        public void Dismissal_WithValidArguments_NewChangeAddChangesReterned()
        {
            //Arrange
            var Changes = new List<Change>();
            Change NewChange, OldChange;
            List<Position> ListPositions = new List<Position>();
            int NumOrd = 1;
            OldChange = employee.Recruitment(NumOrd, N1Pos1);
            Changes.Add(OldChange);
            NumOrd = 2;

            //ACT
            NewChange = employee.Dismissal(NumOrd, N1Pos1);
            Changes.Add(NewChange);

            //Assert
            CollectionAssert.AreEqual(ListPositions, employee.GetListCurrentPositions().ToList());
            CollectionAssert.AreEqual(Changes, employee.GetChanges().ToList());

            Assert.AreEqual(OldChange, NewChange.GetPreviousChange());
            Assert.AreEqual(NumOrd, NewChange.GetNumberOrder());
            Assert.IsNull(NewChange.GetPosition());
            Assert.AreEqual(employee, NewChange.GetEmployee());
            Assert.IsFalse(NewChange.GetIsCombination());
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
            Change NewChange, OldChange;
            OldChange = employee.Recruitment(1, N1Pos1);
            //ACT
            NewChange = employee.Dismissal(NumOrd, N1Pos1);

            //Assert
        }

        [ExpectedException(typeof(ArgumentNullException), "Исключение на передачу в должность null, не было вызвано.")]
        [TestMethod()]
        public void Dismissal_WhenPositionIsNull_ExceptionReterned()
        {
            //Arrange
            Change NewChange, OldChange;
            int NumOrd = 1;
            OldChange = employee.Recruitment(NumOrd, N1Pos1);
            NumOrd = 2;
            //ACT
            NewChange = employee.Dismissal(NumOrd, null);

            //Asser
        }

        [ExpectedException(typeof(ArgumentException), "Исключение на передачу в должность должности которой нет, не было вызвано.")]
        [TestMethod()]
        public void Dismissal_WhenPositionIsBusy_ExceptionReterned()
        {
            //Arrange
            Change NewChange, OldChange;
            int NumOrd = 1;
            OldChange = employee.Recruitment(NumOrd, N1Pos1);
            NumOrd = 2;
            N1Pos2.BusyPosition();
            //ACT
            NewChange = employee.Dismissal(NumOrd, N1Pos2);
            //Assert
        }
        #endregion


        #region ChangeFullName (Изменить имя)
        [TestMethod()]
        public void ChangeFullName_WithValidArguments_NewFullNameAndTrueReterned()
        {
            //ACT
            string NewName = "Name2";
            var ret = employee.ChangeFullName(NewName);

            //Assert
            Assert.IsTrue(ret);
            Assert.AreEqual(NewName, employee.GetFullName());
            
        }


        [DataTestMethod()]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow("  ")]
        [DataRow("\n")]
        [DataRow("\t")]
        public void ChangeFullName_WhenNameIsEmpty_OldFullNameAndFalseReterned(string NewName)
        {
            var oldName = employee.GetFullName();
            //ACT
            var ret = employee.ChangeFullName(NewName);

            //Assert
            Assert.IsFalse(ret);
            Assert.AreEqual(oldName, employee.GetFullName());
        }


        [TestMethod()]
        public void ChangeFullName_WhenNewNameIsOldName_OldFullNameAndFalseReterned()
        {
            var oldName = employee.GetFullName();
            //ACT
            string NewName = "Name1";
            var ret = employee.ChangeFullName(NewName);

            //Assert
            Assert.IsFalse(ret);
            Assert.AreEqual(oldName, employee.GetFullName());
        }
        #endregion

    }
}