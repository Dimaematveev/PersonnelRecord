using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersonnelRecord.BL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonnelRecord.BL.Classes.Employes.Tests
{
    /// <summary>
    /// Тестирование "Уволить с должности"
    /// </summary>
    [TestClass()]
    public class Employee_Dismissal_Tests
    {

        private string name;
        private DateTime birthday;
        private Employee employee;
        private Unit unit1, unit2;
        private Position N1Pos1, N1Pos2, N2Pos1, N2Pos2;

        #region Первоначальная настройка
        /// <summary>
        /// Вызывается перед каждым методом теста
        /// </summary>
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



        #region Dismissal (Увольнение)
        /// <summary>
        /// Правильные параметры
        /// </summary>
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

        /// <summary>
        /// Исключение на передачу в номер приказа 0 или меньше
        /// </summary>
        /// <param name="NumOrd">Номер приказа</param>
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

        /// <summary>
        /// Исключение на передачу в должность null
        /// </summary>
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

        /// <summary>
        /// Исключение на передачу в должность должности которой нет
        /// </summary>
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


    }
}