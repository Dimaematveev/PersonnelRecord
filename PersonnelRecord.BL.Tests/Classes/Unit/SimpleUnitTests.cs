using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersonnelRecord.BL.Classes;
using PersonnelRecord.BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelRecord.BL.Classes.Unit.Tests
{
    [TestClass()]
    public class SimpleUnitTests
    {
        
        private string nameUnit;
        private List<string> positionsName;
        private SimpleUnit unit;
        
        #region Первоначальная настройка
        [TestInitialize]
        public void TestInitialize()
        {
            Debug.WriteLine("Настройка Подразделения");
            
            var nameMainUnit = "MainUnit";
            var positionsMainUnit = new List<string>() { "MainPos1", "MainPos2" };
            var mainUnit = new SimpleUnit(nameMainUnit, positionsMainUnit);
           
            var nameSubUnit1 = "SubUnit1";
            var positionsSubUnit1 = new List<string>() { "Sub1Pos1", "Sub1Pos2" };
            var subUnit1 = new SimpleUnit(nameSubUnit1, positionsSubUnit1);

            var nameSubUnit2 = "SubUnit2";
            var positionsSubUnit2 = new List<string>() { "Sub2Pos1", "Sub2Pos2" };
            var subUnit2 = new SimpleUnit(nameSubUnit2, positionsSubUnit2);

            nameUnit = "Unit1";
            positionsName = new List<string>() { "Pos1", "Pos2" };
            unit = new SimpleUnit(nameUnit, positionsName);
            Debug.WriteLine($"Название подразделения = '{nameUnit}'");
            Debug.WriteLine($"Должности = '{positionsName.Aggregate((x,y)=>x+' '+y)}'");
            Debug.WriteLine($"Главное подразделение = '{nameMainUnit}'");
            Debug.WriteLine($"Подчиненные подразделения = '{nameSubUnit1} {nameSubUnit2}'");
            //unit.ChangeMainUnit(mainUnit);
            // unit.AddSubordinateUnit(subUnit1);
            // unit.AddSubordinateUnit(subUnit2);

            Debug.WriteLine("Настройка закончена");
        }
        #endregion

        #region Функция Rename (Переименование)
        [TestMethod()]
        public void Rename_WithValidArguments_RenameAndTrueReterned()
        {
            Debug.WriteLine("Начало теста Переименование. Корректные параметры, возврат true.");
            
            // Arrange(настройка)
            string newName = "Unit2";
            Debug.WriteLine($"Новое имя = '{newName}'.");

            // Act — выполнение 
            Debug.WriteLine("Начали Переименовывать");
            bool ret = unit.Rename(newName);
            Debug.WriteLine("Переименовали");

            // Assert — проверка
            Debug.WriteLine($"Должно быть = '{newName}', unit.GetName() = '{unit.GetName()}'.");
            Assert.AreEqual(newName, unit.GetName());
            Debug.WriteLine($"Должно быть = '{true}', unit.Rename().return = '{ret}'.");
            Assert.IsTrue(ret);

            Debug.WriteLine("Переименование Закончено. ");
        }

        [DataTestMethod()]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow("  ")]
        [DataRow("\n")]
        [DataRow("\t")]
        public void Rename_WhenNameEmpty_NotRenameAndFalseReterned(string newName)
        {
            Debug.WriteLine("Начало теста Переименование. Некорректные параметры, возврат false.");

            // Arrange(настройка)
            var oldName = unit.GetName();
            Debug.WriteLine($"Новое имя = '{newName}'.");

            // Act — выполнение 
            Debug.WriteLine("Начали Переименовывать");
            bool ret = unit.Rename(newName);
            Debug.WriteLine("Переименовали");

            // Assert — проверка
            Debug.WriteLine($"Должно быть = '{oldName}', unit.GetName() = '{unit.GetName()}'.");
            Assert.AreEqual(oldName, unit.GetName());
            Debug.WriteLine($"Должно быть = '{false}', unit.Rename().return = '{ret}'.");
            Assert.IsFalse(ret);

            Debug.WriteLine("Переименование Закончено. ");
        }
        #endregion

        #region Функция AddPosition (Добавить должность)
        [TestMethod()]
        public void AddPosition_WithValidArguments_AddPositionAndTrueReturned()
        {
            Debug.WriteLine("Начало теста Добавление должности. Корректные параметры, возврат true.");

            // Arrange(настройка)
            var NewPosition = "Pos3";
            var Positions = unit.GetPositions().Select(x=>x.GetName()).ToList();
            Positions.Add(NewPosition);
            Debug.WriteLine($"Новая должность = '{NewPosition}'.");

            // Act — выполнение 
            Debug.WriteLine("Начал добавлять");
            var ret = unit.AddPosition(NewPosition);
            Debug.WriteLine("Добавили");

            // Assert — проверка
            Debug.WriteLine($"Должно быть = '{Positions.Count}', unit.GetPositions() = '{unit.GetPositions().Count}'.");
            CollectionAssert.AreEqual(Positions, unit.GetPositions().Select(x=>x.GetName()).ToList());
            Debug.WriteLine($"Должно быть = '{true}', unit.AddPosition().return = '{ret}'.");
            Assert.IsTrue(ret);

            Debug.WriteLine("Добавление Закончено. ");
        }

        [DataTestMethod()]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow("  ")]
        [DataRow("\n")]
        [DataRow("\t")]
        public void AddPosition_WhenPositionEmpty_NotAddPositionAndFalseReturned(string NewPosition)
        {
            Debug.WriteLine($"Начало теста Добавление должности. Некорректные параметры, Должность пуста = '{NewPosition}',возврат False.");

            // Arrange(настройка)
            var Positions = unit.GetPositions().Select(x => x.GetName()).ToList();
            Debug.WriteLine($"Новая должность = '{NewPosition}'.");

            // Act — выполнение 
            Debug.WriteLine("Начал добавлять");
            var ret = unit.AddPosition(NewPosition);
            Debug.WriteLine("Добавили");

            // Assert — проверка
            Debug.WriteLine($"Должно быть = '{Positions.Count}', unit.GetPositions() = '{unit.GetPositions().Count}'.");
            CollectionAssert.AreEqual(Positions, unit.GetPositions().Select(x => x.GetName()).ToList());
            Debug.WriteLine($"Должно быть = '{false}', unit.AddPosition().return = '{ret}'.");
            Assert.IsFalse(ret);

            Debug.WriteLine("Добавление должности Закончено. ");
        }
        #endregion

        #region Функция DeletePosition (Удалить должность)
        [TestMethod()]
        public void DeletePosition_WithValidArguments_DeletePositionAndTrueReturned()
        {
            Debug.WriteLine("Начало теста Удаление должности. Корректные параметры, возврат true.");

            // Arrange(настройка)
            var DeletePositionName = "Pos1";
            var DeletePosition = unit.GetPositions().FirstOrDefault(x => x.GetName() == DeletePositionName);
            var Positions = unit.GetPositions().ToList();
            Positions.Remove(DeletePosition);
            Debug.WriteLine($"Удаляемая должность = '{DeletePosition}'.");

            // Act — выполнение 
            Debug.WriteLine("Начал удалять");
            var ret = unit.DeletePosition(DeletePosition);
            Debug.WriteLine("Удалил");

            // Assert — проверка
            Debug.WriteLine($"Должно быть = '{Positions.Count}', unit.GetPositions() = '{unit.GetPositions().Count}'.");
            CollectionAssert.AreEqual(Positions, unit.GetPositions().ToList());
            Debug.WriteLine($"Должно быть = '{true}', unit.DeletePosition().return = '{ret}'.");
            Assert.IsTrue(ret);

            Debug.WriteLine("Удаление Закончено. ");
        }

        [TestMethod()]
        public void DeletePosition_WhenPositionNotInList_NotDeletePositionAndFalseReturned()
        {
            Debug.WriteLine("Начало теста Удаление должности. Некорректные параметры, нет такой должности, возврат false.");

            // Arrange(настройка)
            var DeletePositionName = "Pos3";
            var unit2 = new SimpleUnit("unit2", new List<string>() { DeletePositionName });
            var DeletePosition = unit2.GetPositions().FirstOrDefault(x => x.GetName() == DeletePositionName);
            var Positions = unit.GetPositions().ToList();
            Debug.WriteLine($"Удаляемая должность = '{DeletePosition}'.");

            // Act — выполнение 
            Debug.WriteLine("Начал удалять");
            var ret = unit.DeletePosition(DeletePosition);
            Debug.WriteLine("Удалил");

            // Assert — проверка
            Debug.WriteLine($"Должно быть = '{Positions.Count}', unit.GetPositions() = '{unit.GetPositions().Count}'.");
            CollectionAssert.AreEqual(Positions, unit.GetPositions().ToList());
            Debug.WriteLine($"Должно быть = '{false}', unit.DeletePosition().return = '{ret}'.");
            Assert.IsFalse(ret);

            Debug.WriteLine("Удаление Закончено. ");
        }

        [TestMethod()]
        public void DeletePosition_WhenPositionNull_NotDeletePositionAndFalseReturned()
        {
            Debug.WriteLine("Начало теста Удаление должности. Некорректные параметры должность = null, возврат false.");

            // Arrange(настройка)
            IPosition DeletePosition = null;
            var Positions = unit.GetPositions().ToList();
            Debug.WriteLine($"Удаляемая должность = '{DeletePosition}'.");

            // Act — выполнение 
            Debug.WriteLine("Начал удалять");
            var ret = unit.DeletePosition(DeletePosition);
            Debug.WriteLine("Удалил");

            // Assert — проверка
            Debug.WriteLine($"Должно быть = '{Positions.Count}', unit.GetPositions() = '{unit.GetPositions().Count}'.");
            CollectionAssert.AreEqual(Positions, unit.GetPositions().ToList());
            Debug.WriteLine($"Должно быть = '{false}', unit.DeletePosition().return = '{ret}'.");
            Assert.IsFalse(ret);

            Debug.WriteLine("Удаление Закончено. ");
        }
        #endregion

        #region Функция AddSubordinateUnit (Добавить подчиненное подразделение) ---- ХЕРНЯ не придумал
        [TestMethod()]
        public void AddSubordinateUnit_WithValidArguments_AddSubordinateUnitAndTrueReturned()
        {
            Debug.WriteLine("Начало теста Добавление подчиненных подразделений. Корректные параметры, возврат true.");

            // Arrange(настройка)
            var NewSubName = "NewSubUnit1";
            var NewListPositions = new List<string>() { "1" };
            var newUnit = new SimpleUnit(NewSubName, NewListPositions);

            var SubUnits = unit.GetSubordinateUnits().ToList();
            SubUnits.Add(newUnit);
            Debug.WriteLine($"Новое подчиненное подразделение = '{NewSubName}'.");

            // Act — выполнение 
            Debug.WriteLine("Начал Добавлять");
            var ret = unit.AddSubordinateUnit(newUnit);
            Debug.WriteLine("Добавить");

            // Assert — проверка
            Debug.WriteLine($"Должно быть = '{SubUnits.Count}', unit.GetPositions() = '{unit.GetSubordinateUnits().Count}'.");
            CollectionAssert.AreEqual(SubUnits, unit.GetSubordinateUnits().ToList());
            Debug.WriteLine($"Должно быть = '{true}', unit.DeletePosition().return = '{ret}'.");
            Assert.IsTrue(ret);

            Debug.WriteLine("Добавление Закончено. ");
        }

        #endregion

        #region Функция DeleteSubordinateUnit (Удалить подчиненное подразделение)
        [TestMethod()]
        public void DeleteSubordinateUnit_WithValidArguments_DeletePositionAndTrueReturned()
        {
            Debug.WriteLine("Начало теста Удаление подчиненных подразделений. Корректные параметры, возврат true.");

            // Arrange(настройка)
            var DeletePositionName = "SubUnit1";
            var DeletePosition = unit.GetSubordinateUnits().FirstOrDefault(x => x.GetName() == DeletePositionName);
            var Positions = unit.GetSubordinateUnits().ToList();
            Positions.Remove(DeletePosition);
            Debug.WriteLine($"Удаляемая должность = '{DeletePosition}'.");

            // Act — выполнение 
            Debug.WriteLine("Начал удалять");
            var ret = unit.DeleteSubordinateUnit(DeletePosition);
            Debug.WriteLine("Удалил");

            // Assert — проверка
            Debug.WriteLine($"Должно быть = '{Positions.Count}', unit.GetPositions() = '{unit.GetPositions().Count}'.");
            CollectionAssert.AreEqual(Positions, unit.GetPositions().ToList());
            Debug.WriteLine($"Должно быть = '{true}', unit.DeletePosition().return = '{ret}'.");
            Assert.IsTrue(ret);

            Debug.WriteLine("Удаление Закончено. ");
        }
        #endregion

        



        #region Функция GetMainUnits (Получить все главные подразделения)
        [TestMethod()]
        public void GetMainUnits_WithValidArguments_ListIUnitReturned()
        {
            Debug.WriteLine("Начало теста Получить все главные подразделения. Корректные параметры, возврат список подразделений.");

            // Arrange(настройка)
            IUnit unit2 = new SimpleUnit("1", new List<string>() { "2" });
            Debug.WriteLine($"Новое главное подразделение = '{unit2.GetName()}'.");

            // Act — выполнение 
            Debug.WriteLine("Начал Изменять");
            var ret = unit.ChangeMainUnit(unit2);
            Debug.WriteLine("Изменил");

            // Assert — проверка
            Debug.WriteLine($"Должно быть = '{unit2.GetName()}', unit.GetMainUnit() = '{unit.GetMainUnit().GetName()}'.");
            Assert.AreEqual(unit2, unit.GetMainUnit());
            Debug.WriteLine($"Должно быть = '{true}', unit.ChangeMainUnit().return = '{ret}'.");
            Assert.IsTrue(ret);

            Debug.WriteLine("Удаление Закончено. ");
        }
        #endregion

        #region Функция ChangeMainUnit (Изменить главное подразделение)
        [TestMethod()]
        public void ChangeMainUnit_WithValidArguments_NewMainUnitAndTrueReturned()
        {
            Debug.WriteLine("Начало теста Изменение Главного подразделения. Корректные параметры, возврат true.");

            // Arrange(настройка)
            IUnit unit2 = new SimpleUnit("1",new List<string>() { "2" });
            Debug.WriteLine($"Новое главное подразделение = '{unit2.GetName()}'.");

            // Act — выполнение 
            Debug.WriteLine("Начал Изменять");
            var ret = unit.ChangeMainUnit(unit2);
            Debug.WriteLine("Изменил");

            // Assert — проверка
            Debug.WriteLine($"Должно быть = '{unit2.GetName()}', unit.GetMainUnit() = '{unit.GetMainUnit().GetName()}'.");
            Assert.AreEqual(unit2, unit.GetMainUnit());
            Debug.WriteLine($"Должно быть = '{true}', unit.ChangeMainUnit().return = '{ret}'.");
            Assert.IsTrue(ret);

            Debug.WriteLine("Удаление Закончено. ");
        }

        [TestMethod()]
        public void ChangeMainUnit_WhenMainUnitNull_NotChangeMainUnitAndFalseReturned()
        {
            Debug.WriteLine("Начало теста Изменение Главного подразделения. Некорректные параметры, главное подразделение = null, возврат false.");

            // Arrange(настройка)
            IUnit unit2 = null;
            IUnit odlMainUnit = unit.GetMainUnit();
            Debug.WriteLine($"Новое главное подразделение = 'null'.");

            // Act — выполнение 
            Debug.WriteLine("Начал Изменять");
            var ret = unit.ChangeMainUnit(unit2);
            Debug.WriteLine("Изменил");

            // Assert — проверка
            Debug.WriteLine($"Должно быть = '{odlMainUnit.GetName()}', unit.GetMainUnit() = '{unit.GetMainUnit().GetName()}'.");
            Assert.AreEqual(odlMainUnit, unit.GetMainUnit());
            Debug.WriteLine($"Должно быть = '{false}', unit.ChangeMainUnit().return = '{ret}'.");
            Assert.IsFalse(ret);

            Debug.WriteLine("Изменение Главного подразделения Закончено. ");
        }

        [TestMethod()]
        public void ChangeMainUnit_WhenMainUnitIsSub_NotChangeMainUnitAndFalseReturned()
        {
            Debug.WriteLine("Начало теста Изменение Главного подразделения. Некорректные параметры, главное подразделение имеется в подчиненном, возврат false.");

            // Arrange(настройка)
            IUnit unit2 = unit.GetSubordinateUnits().FirstOrDefault();
            IUnit odlMainUnit = unit.GetMainUnit();
            Debug.WriteLine($"Новое главное подразделение = '{unit2.GetName()}'.");

            // Act — выполнение 
            Debug.WriteLine("Начал Изменять");
            var ret = unit.ChangeMainUnit(unit2);
            Debug.WriteLine("Изменил");

            // Assert — проверка
            Debug.WriteLine($"Должно быть = '{odlMainUnit.GetName()}', unit.GetMainUnit() = '{unit.GetMainUnit().GetName()}'.");
            Assert.AreEqual(odlMainUnit, unit.GetMainUnit());
            Debug.WriteLine($"Должно быть = '{false}', unit.ChangeMainUnit().return = '{ret}'.");
            Assert.IsFalse(ret);
            Debug.WriteLine("Изменение Главного подразделения Закончено. ");
        }
        #endregion

        #region Функция Delete (Удалить подразделение)
        [TestMethod()]
        public void Delete()
        {
            Assert.Fail();
        }
        #endregion
    }
}