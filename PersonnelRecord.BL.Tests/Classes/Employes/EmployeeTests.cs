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
            unit2 = new Unit("Name1", new List<string>() { "N1Pos1", "N1Pos2" });
            N1Pos1 = unit1.GetPositions().FirstOrDefault(x => x.GetName() == "N1Pos1");
            N1Pos2 = unit1.GetPositions().FirstOrDefault(x => x.GetName() == "N1Pos2");
            N2Pos1 = unit1.GetPositions().FirstOrDefault(x => x.GetName() == "N2Pos1");
            N2Pos2 = unit1.GetPositions().FirstOrDefault(x => x.GetName() == "N2Pos2");
            // Act — выполнение или вызов тестируемого сценария;
            employee = new Employee(id, name, birthday);
           
        }
        #endregion


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


        [TestMethod()]
        public void ChangeFullName_WithValidArguments_NewFullNameReterned()
        {
            //ACT
            string NewName = "Name2";
            employee.ChangeFullName(NewName);

            //Assert
            Assert.AreEqual(NewName, employee.GetFullName());
        }


    }
}