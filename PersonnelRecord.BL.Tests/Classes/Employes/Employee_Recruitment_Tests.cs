using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersonnelRecord.BL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonnelRecord.BL.Classes.Employes.Tests
{
    /// <summary>
    /// Тестирование "Принять на основную должность"
    /// </summary>
    [TestClass()]
    public class Employee_Recruitment_Tests
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


        #region Recruitment (Принятие на работу)
        /// <summary>
        /// Правильные параметры
        /// </summary>
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
            Change NewChange;

            //ACT
            NewChange = employee.Recruitment(NumOrd, N1Pos1);

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
            Change NewChange;
            int NumOrd = 1;
            //ACT
            NewChange = employee.Recruitment(NumOrd, null);

            //Assert
        }

        /// <summary>
        /// Исключение на передачу должности в основную если уже есть основная
        /// </summary>
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

        /// <summary>
        /// Исключение на передачу в должность занятой должности
        /// </summary>
        [ExpectedException(typeof(ArgumentException), "Исключение на передачу в должность занятой должности, не было вызвано.")]
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



    }
}