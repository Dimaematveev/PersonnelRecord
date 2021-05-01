using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersonnelRecord.BL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonnelRecord.BL.Classes.Employes.Tests
{
    [TestClass()]
    public class Employee_ChangeFullName_Tests
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