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
            unit2 = new Unit("Name1", new List<string>() { "N1Pos1", "N1Pos2" });
            N1Pos1 = unit1.GetPositions().FirstOrDefault(x => x.GetName() == "N1Pos1");
            N1Pos2 = unit1.GetPositions().FirstOrDefault(x => x.GetName() == "N1Pos2");
            N2Pos1 = unit1.GetPositions().FirstOrDefault(x => x.GetName() == "N2Pos1");
            N2Pos2 = unit1.GetPositions().FirstOrDefault(x => x.GetName() == "N2Pos2");
            // Act — выполнение или вызов тестируемого сценария;
            employee1 = new Employee(1, "Name1", new DateTime(1994, 12, 1));
            employee2 = new Employee(2, "Name2", new DateTime(1994, 12, 2));

        }
        #endregion
        [TestMethod()]
        public void Recruitment_WithValidArgumentsCMainPosition_NewChangeReterned()
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
        public void Recruitment_WithValidArgumentsComPosition_NewChangeReterned()
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
            Assert.AreEqual(OldChange.GetID() + 1, NewChange.GetID());
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
            Assert.AreEqual(OldChange.GetID() + 1, NewChange.GetID());
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
            Assert.AreEqual(OldChange.GetID() + 1, NewChange.GetID());
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
            Assert.AreEqual(OldChange.GetID() + 1, NewChange.GetID());
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
    }
}