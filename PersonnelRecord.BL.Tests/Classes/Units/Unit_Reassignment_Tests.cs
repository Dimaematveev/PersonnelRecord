using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace PersonnelRecord.BL.Classes.Units.Tests
{
    /// <summary>
    /// Тестирование "Переподчинить подразделение"
    /// </summary>
    [TestClass()]
    public class Unit_Reassignment_Tests
    {

        private string nameUnit;
        private List<string> positionsName;
        private Unit unit, mainUnit, subUnit1, subUnit2;

        #region Первоначальная настройка
        /// <summary>
        /// Вызывается перед каждым методом теста
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            var nameMainUnit = "MainUnit";
            var positionsMainUnit = new List<string>() { "MainPos1", "MainPos2" };
            mainUnit = new Unit(nameMainUnit, positionsMainUnit, true);

            var nameSubUnit1 = "SubUnit1";
            var positionsSubUnit1 = new List<string>() { "Sub1Pos1", "Sub1Pos2" };
            subUnit1 = new Unit(nameSubUnit1, positionsSubUnit1);

            var nameSubUnit2 = "SubUnit2";
            var positionsSubUnit2 = new List<string>() { "Sub2Pos1", "Sub2Pos2" };
            subUnit2 = new Unit(nameSubUnit2, positionsSubUnit2);

            nameUnit = "Unit1";
            positionsName = new List<string>() { "Pos1", "Pos2" };
            unit = new Unit(nameUnit, positionsName);
            
            unit.Reassignment(mainUnit);
            subUnit1.Reassignment(unit);
            subUnit2.Reassignment(unit);

        }
        #endregion



        #region IsPossibleReassignment (Возможно ли переподчинить подразделение)
        /// <summary>
        /// Правильные параметры
        /// </summary>
        [TestMethod()]
        public void IsPossibleReassignment_WithValidArguments_TrueReturned()
        {
            
            var subMainUnit = mainUnit.GetSubordinateUnits();
            var subSubUnit = subUnit1.GetSubordinateUnits();
            var unitSubUnit = unit.GetSubordinateUnits();
            // Arrange(настройка)
            // Act — выполнение 
            
            var ret = subUnit1.IsPossibleReassignment(mainUnit);
            

            // Assert — проверка
            //Проверяем что у главного ничего не поменялось
            CollectionAssert.AreEqual(subMainUnit.ToList(), mainUnit.GetSubordinateUnits().ToList());
            //Проверяем что у подчиненных главное не поменялось
            CollectionAssert.AreEqual(subSubUnit.ToList(), subUnit1.GetSubordinateUnits().ToList());
            //ПРоверяем что у нашего главное не поменялось
            Assert.AreEqual(unit, subUnit1.GetMainUnit());
            CollectionAssert.AreEqual(unitSubUnit.ToList(), unit.GetSubordinateUnits().ToList());
            //ПРоверяем что возможно это сделать
            Assert.IsTrue(ret);
            
        }

        /// <summary>
        /// Нельзя Переподчинить подразделение если главное null
        /// </summary>
        [TestMethod()]
        public void IsPossibleReassignment_WhenNewMainisNull_FalseReturned()
        {
            
            var subMainUnit = mainUnit.GetSubordinateUnits();
            var subSubUnit = subUnit1.GetSubordinateUnits();
            var unitSubUnit = unit.GetSubordinateUnits();
            // Arrange(настройка)
            // Act — выполнение 
            
            var ret = subUnit1.IsPossibleReassignment(null);
            

            // Assert — проверка
            //Проверяем что у главного ничего не поменялось
            CollectionAssert.AreEqual(subMainUnit.ToList(), mainUnit.GetSubordinateUnits().ToList());
            //Проверяем что у подчиненных главное не поменялось
            CollectionAssert.AreEqual(subSubUnit.ToList(), subUnit1.GetSubordinateUnits().ToList());
            //ПРоверяем что у нашего главное не поменялось
            Assert.AreEqual(unit, subUnit1.GetMainUnit());
            CollectionAssert.AreEqual(unitSubUnit.ToList(), unit.GetSubordinateUnits().ToList());
            //ПРоверяем что возможно это сделать
            Assert.IsFalse(ret);
            
        }

        /// <summary>
        /// Нельзя Переподчинить подразделение если главное Есть в подчиненных
        /// </summary>
        [TestMethod()]
        public void IsPossibleReassignment_WhenNewMainIsSub_FalseReturned()
        {
            
            var subMainUnit = mainUnit.GetSubordinateUnits();
            var subSubUnit = subUnit1.GetSubordinateUnits();
            var unitSubUnit = unit.GetSubordinateUnits();
            // Arrange(настройка)
            // Act — выполнение 
            
            var ret = unit.IsPossibleReassignment(subUnit1);
            

            // Assert — проверка
            //Проверяем что у главного ничего не поменялось
            CollectionAssert.AreEqual(subMainUnit.ToList(), mainUnit.GetSubordinateUnits().ToList());
            //Проверяем что у подчиненных главное не поменялось
            CollectionAssert.AreEqual(subSubUnit.ToList(), subUnit1.GetSubordinateUnits().ToList());
            //ПРоверяем что у нашего главное не поменялось
            Assert.AreEqual(unit, subUnit1.GetMainUnit());
            CollectionAssert.AreEqual(unitSubUnit.ToList(), unit.GetSubordinateUnits().ToList());
            //ПРоверяем что возможно это сделать
            Assert.IsFalse(ret);
            
        }

        /// <summary>
        /// Нельзя Переподчинить подразделение если главное есть ты
        /// </summary>
        [TestMethod()]
        public void IsPossibleReassignment_WhenNewMainIsYou_FalseReturned()
        {
            
            var subMainUnit = mainUnit.GetSubordinateUnits();
            var subSubUnit = subUnit1.GetSubordinateUnits();
            var unitSubUnit = unit.GetSubordinateUnits();
            // Arrange(настройка)
            // Act — выполнение 
            
            var ret = unit.IsPossibleReassignment(unit);
            

            // Assert — проверка
            //Проверяем что у главного ничего не поменялось
            CollectionAssert.AreEqual(subMainUnit.ToList(), mainUnit.GetSubordinateUnits().ToList());
            //Проверяем что у подчиненных главное не поменялось
            CollectionAssert.AreEqual(subSubUnit.ToList(), subUnit1.GetSubordinateUnits().ToList());
            //ПРоверяем что у нашего главное не поменялось
            Assert.AreEqual(unit, subUnit1.GetMainUnit());
            CollectionAssert.AreEqual(unitSubUnit.ToList(), unit.GetSubordinateUnits().ToList());
            //ПРоверяем что возможно это сделать
            Assert.IsFalse(ret);
            
        }

        /// <summary>
        /// Нельзя Переподчинить подразделение если главное Удалено
        /// </summary>
        [TestMethod()]
        public void IsPossibleReassignment_WhenNewMainIsDelete_FalseReturned()
        {
            
            Unit u1 = new Unit("ss", new List<string>() { "ss" });
            subUnit1.Delete();
            var subMainUnit = subUnit1.GetSubordinateUnits();
            var unitSubUnit = u1.GetSubordinateUnits();
           
           
            // Arrange(настройка)
            // Act — выполнение 
            
            var ret = u1.IsPossibleReassignment(subUnit1);
            

            // Assert — проверка
            //Проверяем что у главного ничего не поменялось
            CollectionAssert.AreEqual(subMainUnit.ToList(), subUnit1.GetSubordinateUnits().ToList());
            //Проверяем что у подчиненных главное не поменялось
            //ПРоверяем что у нашего главное не поменялось
            CollectionAssert.AreEqual(unitSubUnit.ToList(), u1.GetSubordinateUnits().ToList());
            Assert.AreEqual(null, u1.GetMainUnit());
            //ПРоверяем что возможно это сделать
            Assert.IsFalse(ret);
            
        }

        #endregion

        #region Reassignment (Переподчинение подразделения)
        /// <summary>
        /// Правильные параметры
        /// </summary>
        [TestMethod()]
        public void Reassignment_WithValidArguments_TrueReturned()
        {
            
            var subMainUnit = mainUnit.GetSubordinateUnits().ToList();
            var subSubUnit = subUnit1.GetSubordinateUnits().ToList();
            var unitSubUnit = unit.GetSubordinateUnits().ToList();
            // Arrange(настройка)
            // Act — выполнение 
            
            var ret = subUnit1.Reassignment(mainUnit);
            
            subMainUnit.Add(subUnit1);
            unitSubUnit.Remove(subUnit1);

            // Assert — проверка
            //Проверяем что у главного ничего не поменялось
            CollectionAssert.AreEqual(subMainUnit, mainUnit.GetSubordinateUnits().ToList());
            //Проверяем что у подчиненных главное не поменялось
            CollectionAssert.AreEqual(subSubUnit, subUnit1.GetSubordinateUnits().ToList());
            //ПРоверяем что у нашего главное не поменялось
            Assert.AreEqual(mainUnit, subUnit1.GetMainUnit());
            CollectionAssert.AreEqual(unitSubUnit, unit.GetSubordinateUnits().ToList());
            //ПРоверяем что возможно это сделать
            Assert.IsTrue(ret);
            
        }

        /// <summary>
        /// Нельзя Переподчинить подразделение если главное null
        /// </summary>
        [TestMethod()]
        public void Reassignment_WhenNewMainisNull_FalseReturned()
        {
            
            var subMainUnit = mainUnit.GetSubordinateUnits();
            var subSubUnit = subUnit1.GetSubordinateUnits();
            var unitSubUnit = unit.GetSubordinateUnits();
            // Arrange(настройка)
            // Act — выполнение 
            
            var ret = subUnit1.Reassignment(null);
            

            // Assert — проверка
            //Проверяем что у главного ничего не поменялось
            CollectionAssert.AreEqual(subMainUnit.ToList(), mainUnit.GetSubordinateUnits().ToList());
            //Проверяем что у подчиненных главное не поменялось
            CollectionAssert.AreEqual(subSubUnit.ToList(), subUnit1.GetSubordinateUnits().ToList());
            //ПРоверяем что у нашего главное не поменялось
            Assert.AreEqual(unit, subUnit1.GetMainUnit());
            CollectionAssert.AreEqual(unitSubUnit.ToList(), unit.GetSubordinateUnits().ToList());
            //ПРоверяем что возможно это сделать
            Assert.IsFalse(ret);
            
        }

        /// <summary>
        /// Нельзя Переподчинить подразделение если главное  есть в подчиненных
        /// </summary>
        [TestMethod()]
        public void Reassignment_WhenNewMainIsSub_FalseReturned()
        {
            
            var subMainUnit = mainUnit.GetSubordinateUnits();
            var subSubUnit = subUnit1.GetSubordinateUnits();
            var unitSubUnit = unit.GetSubordinateUnits();
            // Arrange(настройка)
            // Act — выполнение 
            
            var ret = unit.Reassignment(subUnit1);
            

            // Assert — проверка
            //Проверяем что у главного ничего не поменялось
            CollectionAssert.AreEqual(subMainUnit.ToList(), mainUnit.GetSubordinateUnits().ToList());
            //Проверяем что у подчиненных главное не поменялось
            CollectionAssert.AreEqual(subSubUnit.ToList(), subUnit1.GetSubordinateUnits().ToList());
            //ПРоверяем что у нашего главное не поменялось
            Assert.AreEqual(unit, subUnit1.GetMainUnit());
            CollectionAssert.AreEqual(unitSubUnit.ToList(), unit.GetSubordinateUnits().ToList());
            //ПРоверяем что возможно это сделать
            Assert.IsFalse(ret);
            
        }

        /// <summary>
        /// Нельзя Переподчинить подразделение если главное есть ты
        /// </summary>
        [TestMethod()]
        public void Reassignment_WhenNewMainIsYou_FalseReturned()
        {
            
            var subMainUnit = mainUnit.GetSubordinateUnits();
            var subSubUnit = subUnit1.GetSubordinateUnits();
            var unitSubUnit = unit.GetSubordinateUnits();
            // Arrange(настройка)
            // Act — выполнение 
            
            var ret = unit.Reassignment(unit);
            

            // Assert — проверка
            //Проверяем что у главного ничего не поменялось
            CollectionAssert.AreEqual(subMainUnit.ToList(), mainUnit.GetSubordinateUnits().ToList());
            //Проверяем что у подчиненных главное не поменялось
            CollectionAssert.AreEqual(subSubUnit.ToList(), subUnit1.GetSubordinateUnits().ToList());
            //ПРоверяем что у нашего главное не поменялось
            Assert.AreEqual(unit, subUnit1.GetMainUnit());
            CollectionAssert.AreEqual(unitSubUnit.ToList(), unit.GetSubordinateUnits().ToList());
            //ПРоверяем что возможно это сделать
            Assert.IsFalse(ret);
            
        }

        /// <summary>
        /// Нельзя Переподчинить подразделение если главное Удалено
        /// </summary>
        [TestMethod()]
        public void Reassignment_WhenNewMainIsDelete_FalseReturned()
        {

            Unit u1 = new Unit("ss", new List<string>() { "ss" });
            subUnit1.Delete();
            var subMainUnit = subUnit1.GetSubordinateUnits();
            var unitSubUnit = u1.GetSubordinateUnits();


            // Arrange(настройка)
            // Act — выполнение 

            var ret = u1.Reassignment(subUnit1);


            // Assert — проверка
            //Проверяем что у главного ничего не поменялось
            CollectionAssert.AreEqual(subMainUnit.ToList(), subUnit1.GetSubordinateUnits().ToList());
            //Проверяем что у подчиненных главное не поменялось
            //ПРоверяем что у нашего главное не поменялось
            CollectionAssert.AreEqual(unitSubUnit.ToList(), u1.GetSubordinateUnits().ToList());
            Assert.AreEqual(null, u1.GetMainUnit());
            //ПРоверяем что возможно это сделать
            Assert.IsFalse(ret);

        }
        #endregion
    }
}