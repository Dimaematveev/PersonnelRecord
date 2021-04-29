using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PersonnelRecord.BL.Classes.Units.Tests
{
    [TestClass()]
    public class UnitTests
    {

        private string nameUnit;
        private List<string> positionsName;
        private Unit unit, mainUnit, subUnit1, subUnit2;

        #region Первоначальная настройка
        [TestInitialize]
        public void TestInitialize()
        {
            Debug.WriteLine("Настройка Подразделения");

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
            Debug.WriteLine($"Название подразделения = '{nameUnit}'");
            Debug.WriteLine($"Должности = '{positionsName.Aggregate((x, y) => x + ' ' + y)}'");
            Debug.WriteLine($"Главное подразделение = '{nameMainUnit}'");
            Debug.WriteLine($"Подчиненные подразделения = '{nameSubUnit1} {nameSubUnit2}'");
            
            unit.Reassignment(mainUnit);
            subUnit1.Reassignment(unit);
            subUnit2.Reassignment(unit);

            Debug.WriteLine("Настройка закончена");
        }
        #endregion


        #region Функция GetMainUnits (Получить все главные подразделения)
        [TestMethod()]
        public void GetMainUnits_WithValidArguments_ListIUnitReturned()
        {
            Debug.WriteLine("Начало теста Получить все главные подразделения. Корректные параметры, возврат список подразделений.");

            // Arrange(настройка)
            Unit MainUnit1 = new Unit("1", new List<string>() { "2" }, true);
            Unit unit1 = new Unit("2", new List<string>() { "2" });
            Unit Subunit1 = new Unit("3", new List<string>() { "2" });

            unit1.Reassignment(MainUnit1);
            Subunit1.Reassignment(unit1);
            List<Unit> MainUnits = new List<Unit>() { unit1, MainUnit1 };

            // Act — выполнение 
            Debug.WriteLine("Начал Изменять");
            var ret = Subunit1.GetMainUnits();
            Debug.WriteLine("Изменил");

            // Assert — проверка
            Debug.WriteLine($"Должно быть = '{MainUnits.Count}', unit.GetMainUnit() = '{ret.Count}'.");
            CollectionAssert.AreEqual(MainUnits, ret.ToList());
            Debug.WriteLine("Удаление Закончено. ");
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


        #region Функция IsPossibleAddPosition (Проверка на Добавить должность)
        [TestMethod()]
        public void IsPossibleAddPosition_WithValidArguments_NullReturned()
        {
            Debug.WriteLine("Начало теста Проверить возможно ли добавление должности. Корректные параметры, возврат true.");

            // Arrange(настройка)
            var NewPosition = "Pos3";
            var Positions = unit.GetPositions().Select(x => x.GetName()).ToList();
            
            Debug.WriteLine($"Новая должность = '{NewPosition}'.");

            // Act — выполнение 
            Debug.WriteLine("Начал добавлять");
            var ret = unit.IsPossibleAddPosition(NewPosition);
            Debug.WriteLine("Добавили");

            // Assert — проверка
            Debug.WriteLine($"Должно быть = '{Positions.Count}', unit.GetPositions() = '{unit.GetPositions().Count}'.");
            CollectionAssert.AreEqual(Positions, unit.GetPositions().Select(x => x.GetName()).ToList());
            Debug.WriteLine($"Должно быть = '{null}', unit.AddPosition().return = '{ret}'.");
            Assert.IsNull(ret);

            Debug.WriteLine("Добавление Закончено. ");
        }

        [DataTestMethod()]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow("  ")]
        [DataRow("\n")]
        [DataRow("\t")]
        public void IsPossibleAddPosition_WhenPositionEmpty_NotNullReturned(string NewPosition)
        {
            Debug.WriteLine("Начало теста Проверить возможно ли добавление должности. Корректные параметры, возврат true.");

            // Arrange(настройка)
            var Positions = unit.GetPositions().Select(x => x.GetName()).ToList();

            Debug.WriteLine($"Новая должность = '{NewPosition}'.");

            // Act — выполнение 
            Debug.WriteLine("Начал добавлять");
            var ret = unit.IsPossibleAddPosition(NewPosition);
            Debug.WriteLine("Добавили");

            // Assert — проверка
            Debug.WriteLine($"Должно быть = '{Positions.Count}', unit.GetPositions() = '{unit.GetPositions().Count}'.");
            CollectionAssert.AreEqual(Positions, unit.GetPositions().Select(x => x.GetName()).ToList());
            Debug.WriteLine($"Должно быть = 'Какая-то ошибка', unit.AddPosition().return = '{ret}'.");
            Assert.IsNotNull(ret);

            Debug.WriteLine("Добавление Закончено. ");
        }
        #endregion

        #region Функция AddPosition (Добавить должность)
        [TestMethod()]
        public void AddPosition_WithValidArguments_AddPositionAndTrueReturned()
        {
            Debug.WriteLine("Начало теста Добавление должности. Корректные параметры, возврат true.");

            // Arrange(настройка)
            var NewPosition = "Pos3";
            var Positions = unit.GetPositions().Select(x => x.GetName()).ToList();
            Positions.Add(NewPosition);
            Debug.WriteLine($"Новая должность = '{NewPosition}'.");

            // Act — выполнение 
            Debug.WriteLine("Начал добавлять");
            var ret = unit.AddPosition(NewPosition);
            Debug.WriteLine("Добавили");

            // Assert — проверка
            Debug.WriteLine($"Должно быть = '{Positions.Count}', unit.GetPositions() = '{unit.GetPositions().Count}'.");
            CollectionAssert.AreEqual(Positions, unit.GetPositions().Select(x => x.GetName()).ToList());
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


        #region Функция IsPossibleDeletePosition (проверка на Удалить должность)
        [TestMethod()]
        public void IsPossibleDeletePosition_WithValidArguments_NullReturned()
        {
            Debug.WriteLine("Начало теста Удаление должности. Корректные параметры, возврат true.");

            // Arrange(настройка)
            var DeletePositionName = "Pos1";
            var DeletePosition = unit.GetPositions().FirstOrDefault(x => x.GetName() == DeletePositionName);
            var Positions = unit.GetPositions().ToList();
            //Positions.Remove(DeletePosition);
            Debug.WriteLine($"Удаляемая должность = '{DeletePosition}'.");

            // Act — выполнение 
            Debug.WriteLine("Начал удалять");
            var ret = unit.IsPossibleDeletePosition(DeletePosition);
            Debug.WriteLine("Удалил");

            // Assert — проверка
            Debug.WriteLine($"Должно быть = '{Positions.Count}', unit.GetPositions() = '{unit.GetPositions().Count}'.");
            CollectionAssert.AreEqual(Positions, unit.GetPositions().ToList());
            Debug.WriteLine($"Должно быть = '{null}', unit.DeletePosition().return = '{ret}'.");
            Assert.IsNull(ret);

            Debug.WriteLine("Удаление Закончено. ");
        }

        [TestMethod()]
        public void IsPossibleDeletePosition_WhenPositionNotInList_NotNullReturned()
        {
            Debug.WriteLine("Начало теста Удаление должности. Некорректные параметры, нет такой должности, возврат false.");

            // Arrange(настройка)
            var DeletePositionName = "Pos3";
            var unit2 = new Unit("unit2", new List<string>() { DeletePositionName });
            var DeletePosition = unit2.GetPositions().FirstOrDefault(x => x.GetName() == DeletePositionName);
            var Positions = unit.GetPositions().ToList();
            Debug.WriteLine($"Удаляемая должность = '{DeletePosition}'.");

            // Act — выполнение 
            Debug.WriteLine("Начал удалять");
            var ret = unit.IsPossibleDeletePosition(DeletePosition);
            Debug.WriteLine("Удалил");

            // Assert — проверка
            Debug.WriteLine($"Должно быть = '{Positions.Count}', unit.GetPositions() = '{unit.GetPositions().Count}'.");
            CollectionAssert.AreEqual(Positions, unit.GetPositions().ToList());
            Debug.WriteLine($"Должно быть = 'что-то написано', unit.DeletePosition().return = '{ret}'.");
            Assert.IsNotNull(ret);

            Debug.WriteLine("Удаление Закончено. ");
        }

        [TestMethod()]
        public void IsPossibleDeletePosition_WhenPositionNull_NotNullReturned()
        {
            Debug.WriteLine("Начало теста Удаление должности. Некорректные параметры должность = null, возврат false.");

            // Arrange(настройка)
            Position DeletePosition = null;
            var Positions = unit.GetPositions().ToList();
            Debug.WriteLine($"Удаляемая должность = '{DeletePosition}'.");

            // Act — выполнение 
            Debug.WriteLine("Начал удалять");
            var ret = unit.IsPossibleDeletePosition(DeletePosition);
            Debug.WriteLine("Удалил");

            // Assert — проверка
            Debug.WriteLine($"Должно быть = '{Positions.Count}', unit.GetPositions() = '{unit.GetPositions().Count}'.");
            CollectionAssert.AreEqual(Positions, unit.GetPositions().ToList());
            Debug.WriteLine($"Должно быть = 'что-то написано', unit.DeletePosition().return = '{ret}'.");
            Assert.IsNotNull(ret);

            Debug.WriteLine("Удаление Закончено. ");
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
            var unit2 = new Unit("unit2", new List<string>() { DeletePositionName });
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
            Position DeletePosition = null;
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


        #region Функция IsPossibleChangeMainUnit (Проверка на Изменить главное подразделение)
        [TestMethod()]
        public void IsPossibleChangeMainUnit_WithValidArguments_NullReturned()
        {
            Debug.WriteLine("Начало теста Изменение Главного подразделения. Корректные параметры, возврат true.");

            // Act — выполнение 
            Debug.WriteLine("Начал Изменять");
            var ret = subUnit1.IsPossibleChangeMainUnit(subUnit2);
            Debug.WriteLine("Изменил");

            // Assert — проверка
            Debug.WriteLine($"Должно быть = '{unit.GetName()}', unit.GetMainUnit() = '{subUnit1.GetMainUnit().GetName()}'.");
            Assert.AreEqual(unit, subUnit1.GetMainUnit());
            Debug.WriteLine($"Должно быть = '{true}', unit.ChangeMainUnit().return = '{ret}'.");
            Assert.IsNull(ret);

            Debug.WriteLine("Удаление Закончено. ");
        }

        [TestMethod()]
        public void IsPossibleChangeMainUnit_WhenMainUnitNull_NotChangeMainUnitAndFalseReturned()
        {
            Debug.WriteLine("Начало теста Изменение Главного подразделения. Некорректные параметры, главное подразделение = null, возврат false.");

            // Arrange(настройка)
            Unit unit2 = null;

            Debug.WriteLine($"Новое главное подразделение = 'null'.");

            // Act — выполнение 
            Debug.WriteLine("Начал Изменять");
            var ret = unit.ChangeMainUnit(unit2);
            Debug.WriteLine("Изменил");

            // Assert — проверка
            Debug.WriteLine($"Должно быть = '{mainUnit.GetName()}', unit.GetMainUnit() = '{unit.GetMainUnit().GetName()}'.");
            Assert.AreEqual(mainUnit, unit.GetMainUnit());
            Debug.WriteLine($"Должно быть = 'Что-то должно быть написано', unit.ChangeMainUnit().return = '{ret}'.");
            Assert.IsNotNull(ret);

            Debug.WriteLine("Изменение Главного подразделения Закончено. ");
        }

        [TestMethod()]
        public void IsPossibleChangeMainUnit_WhenMainUnitIsSub_NotChangeMainUnitAndFalseReturned()
        {
            Debug.WriteLine("Начало теста Изменение Главного подразделения. Некорректные параметры, главное подразделение имеется в подчиненном, возврат false.");

            // Arrange(настройка)

            // Act — выполнение 
            Debug.WriteLine("Начал Изменять");
            var ret = unit.ChangeMainUnit(subUnit1);
            Debug.WriteLine("Изменил");

            // Assert — проверка
            Debug.WriteLine($"Должно быть = '{mainUnit.GetName()}', unit.GetMainUnit() = '{unit.GetMainUnit().GetName()}'.");
            Assert.AreEqual(mainUnit, unit.GetMainUnit());
            Debug.WriteLine($"Должно быть = 'Что-то должно быть написано', unit.ChangeMainUnit().return = '{ret}'.");
            Assert.IsNotNull(ret);

            Debug.WriteLine("Изменение Главного подразделения Закончено. ");
        }
        #endregion

        #region Функция ChangeMainUnit (Изменить главное подразделение)
        [TestMethod()]
        public void ChangeMainUnit_WithValidArguments_NewMainUnitAndTrueReturned()
        {
            Debug.WriteLine("Начало теста Изменение Главного подразделения. Корректные параметры, возврат true.");

            // Act — выполнение 
            Debug.WriteLine("Начал Изменять");
            var ret = subUnit1.ChangeMainUnit(subUnit2);
            Debug.WriteLine("Изменил");

            // Assert — проверка
            Debug.WriteLine($"Должно быть = '{subUnit2.GetName()}', unit.GetMainUnit() = '{subUnit1.GetMainUnit().GetName()}'.");
            Assert.AreEqual(subUnit2, subUnit1.GetMainUnit());
            Debug.WriteLine($"Должно быть = '{true}', unit.ChangeMainUnit().return = '{ret}'.");
            Assert.IsTrue(ret);

            Debug.WriteLine("Удаление Закончено. ");
        }

        [TestMethod()]
        public void ChangeMainUnit_WhenMainUnitNull_NotChangeMainUnitAndFalseReturned()
        {
            Debug.WriteLine("Начало теста Изменение Главного подразделения. Некорректные параметры, главное подразделение = null, возврат false.");

            // Arrange(настройка)
            Unit unit2 = null;

            Debug.WriteLine($"Новое главное подразделение = 'null'.");

            // Act — выполнение 
            Debug.WriteLine("Начал Изменять");
            var ret = unit.ChangeMainUnit(unit2);
            Debug.WriteLine("Изменил");

            // Assert — проверка
            Debug.WriteLine($"Должно быть = '{mainUnit.GetName()}', unit.GetMainUnit() = '{unit.GetMainUnit().GetName()}'.");
            Assert.AreEqual(mainUnit, unit.GetMainUnit());
            Debug.WriteLine($"Должно быть = '{false}', unit.ChangeMainUnit().return = '{ret}'.");
            Assert.IsFalse(ret);

            Debug.WriteLine("Изменение Главного подразделения Закончено. ");
        }

        [TestMethod()]
        public void ChangeMainUnit_WhenMainUnitIsSub_NotChangeMainUnitAndFalseReturned()
        {
            Debug.WriteLine("Начало теста Изменение Главного подразделения. Некорректные параметры, главное подразделение имеется в подчиненном, возврат false.");

            // Arrange(настройка)

            // Act — выполнение 
            Debug.WriteLine("Начал Изменять");
            var ret = unit.ChangeMainUnit(subUnit1);
            Debug.WriteLine("Изменил");

            // Assert — проверка
            Debug.WriteLine($"Должно быть = '{mainUnit.GetName()}', unit.GetMainUnit() = '{unit.GetMainUnit().GetName()}'.");
            Assert.AreEqual(mainUnit, unit.GetMainUnit());
            Debug.WriteLine($"Должно быть = '{false}', unit.ChangeMainUnit().return = '{ret}'.");
            Assert.IsFalse(ret);
            Debug.WriteLine("Изменение Главного подразделения Закончено. ");
        }
        #endregion

        #region Функция IsPossibleAddSubordinateUnit (Проверка на Добавить подчиненное подразделение)
        [TestMethod()]
        public void IsPossibleAddSubordinateUnit_WithValidArguments_TrueReturned()
        {
            Debug.WriteLine("Начало теста Добавление подчиненных подразделений. Корректные параметры, возврат true.");

            // Arrange(настройка)
            var NewSubName = "NewSubUnit1";
            var NewListPositions = new List<string>() { "1" };
            var newUnit = new Unit(NewSubName, NewListPositions);

            var SubUnits = unit.GetSubordinateUnits().ToList();
            //SubUnits.Add(newUnit);
            Debug.WriteLine($"Новое подчиненное подразделение = '{NewSubName}'.");

            // Act — выполнение 
            Debug.WriteLine("Начал Добавлять");
            var ret = unit.IsPossibleAddSubordinateUnit(newUnit);
            Debug.WriteLine("Добавить");

            // Assert — проверка
            Debug.WriteLine($"Должно быть = '{SubUnits.Count}', unit.GetPositions() = '{unit.GetSubordinateUnits().Count}'.");
            CollectionAssert.AreEqual(SubUnits, unit.GetSubordinateUnits().ToList());
            Debug.WriteLine($"Должно быть = '{true}', unit.DeletePosition().return = '{ret}'.");
            Assert.IsTrue(ret);

            Debug.WriteLine("Добавление Закончено. ");
        }

        #endregion


        #region Функция AddSubordinateUnit (Добавить подчиненное подразделение)
        [TestMethod()]
        public void AddSubordinateUnit_WithValidArguments_AddSubordinateUnitAndTrueReturned()
        {
            Debug.WriteLine("Начало теста Добавление подчиненных подразделений. Корректные параметры, возврат true.");

            // Arrange(настройка)
            var NewSubName = "NewSubUnit1";
            var NewListPositions = new List<string>() { "1" };
            var newUnit = new Unit(NewSubName, NewListPositions);

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



        #region Функция IsPossibleDeleteSubordinateUnit (Проверка на Удалить подчиненное подразделение)
        [TestMethod()]
        public void IsPossibleDeleteSubordinateUnit_WithValidArguments_TrueReturned()
        {
            Debug.WriteLine("Начало теста Удаление подчиненных подразделений. Корректные параметры, возврат true.");

            // Arrange(настройка)
            var sub = unit.GetSubordinateUnits().ToList();
            // Act — выполнение 
            Debug.WriteLine("Начал удалять");
            var ret = unit.IsPossibleDeleteSubordinateUnit(subUnit1);
            Debug.WriteLine("Удалил");
            //sub.Remove(subUnit1);

            // Assert — проверка
            Debug.WriteLine($"Должно быть = '{sub.Count}', unit.GetPositions() = '{unit.GetSubordinateUnits().Count}'.");
            CollectionAssert.AreEqual(sub, unit.GetSubordinateUnits().ToList());
            Debug.WriteLine($"Должно быть = '{true}', unit.DeletePosition().return = '{ret}'.");
            Assert.IsTrue(ret);

            Debug.WriteLine("Удаление Закончено. ");
        }
        #endregion


        #region Функция DeleteSubordinateUnit (Удалить подчиненное подразделение)
        [TestMethod()]
        public void DeleteSubordinateUnit_WithValidArguments_DeletePositionAndTrueReturned()
        {
            Debug.WriteLine("Начало теста Удаление подчиненных подразделений. Корректные параметры, возврат true.");

            // Arrange(настройка)
            var sub = unit.GetSubordinateUnits().ToList();
            // Act — выполнение 
            Debug.WriteLine("Начал удалять");
            var ret = unit.DeleteSubordinateUnit(subUnit1);
            Debug.WriteLine("Удалил");
            sub.Remove(subUnit1);

            // Assert — проверка
            Debug.WriteLine($"Должно быть = '{sub.Count}', unit.GetPositions() = '{unit.GetSubordinateUnits().Count}'.");
            CollectionAssert.AreEqual(sub, unit.GetSubordinateUnits().ToList());
            Debug.WriteLine($"Должно быть = '{true}', unit.DeletePosition().return = '{ret}'.");
            Assert.IsTrue(ret);

            Debug.WriteLine("Удаление Закончено. ");
        }
        #endregion


        #region Функция IsPossibleDelete (Проверка на Удалить подразделение)
        [TestMethod()]
        public void IsPossibleDelete_WithValidArguments_TrueReturned()
        {
            Debug.WriteLine("Начало теста Удаление. Корректные параметры, возврат true.");

            // Arrange(настройка)
            var hier = unit.GetHierarchyTier();
            var main = unit.GetMainUnit();
            var sub = unit.GetSubordinateUnits();
            // Act — выполнение 
            Debug.WriteLine("Начал удалять");
            var ret = unit.IsPossibleDelete();
            Debug.WriteLine("Удалил");

            // Assert — проверка
            Debug.WriteLine($"Должно быть = '{true}', unit.GetIsDelete() = '{unit.GetIsDelete()}'.");
            Assert.IsFalse(unit.GetIsDelete());

            Debug.WriteLine($"Должно быть = '{hier}', unit.GetHierarchyTier() = '{unit.GetHierarchyTier()}'.");
            Assert.AreEqual(hier, unit.GetHierarchyTier());
            Debug.WriteLine($"Должно быть = '{main}', unit.GetMainUnit() = '{unit.GetMainUnit()}'.");
            Assert.AreEqual(main,unit.GetMainUnit());

            foreach (var pos in unit.GetPositions())
            {
                Debug.WriteLine($"Должно быть = '{true}', unit.GetPositions().pos.GetIsDelete() = '{pos.GetIsDelete()}'.");
                Assert.IsFalse(pos.GetIsDelete());
            }

            Debug.WriteLine($"Должно быть = '{sub.Count}', unit.GetSubordinateUnits() = '{unit.GetSubordinateUnits().Count}'.");
            CollectionAssert.AreEqual(sub.ToList(), unit.GetSubordinateUnits().ToList());
            Debug.WriteLine($"Должно быть = '{unit.GetName()}', subUnit1.GetMainUnit().GetName() = '{subUnit1.GetMainUnit().GetName()}'.");
            Assert.AreEqual(unit, subUnit1.GetMainUnit());
            Debug.WriteLine($"Должно быть = '{unit.GetName()}', subUnit2.GetMainUnit().GetName() = '{subUnit2.GetMainUnit().GetName()}'.");
            Assert.AreEqual(unit, subUnit2.GetMainUnit());

            Debug.WriteLine($"Должно быть = '{true}', unit.DeletePosition().return = '{ret}'.");
            Assert.IsTrue(ret);

            Debug.WriteLine("Удаление Закончено. ");
        }
        #endregion

        #region Функция Delete (Удалить подразделение)
        [TestMethod()]
        public void Delete_WithValidArguments_DeleteAndTrueReturned()
        {
            Debug.WriteLine("Начало теста Удаление. Корректные параметры, возврат true.");

            // Arrange(настройка)
            // Act — выполнение 
            Debug.WriteLine("Начал удалять");
            var ret = unit.Delete();
            Debug.WriteLine("Удалил");

            // Assert — проверка
            Debug.WriteLine($"Должно быть = '{true}', unit.GetIsDelete() = '{unit.GetIsDelete()}'.");
            Assert.IsTrue(unit.GetIsDelete());
            Debug.WriteLine($"Должно быть = '{0}', unit.GetHierarchyTier() = '{unit.GetHierarchyTier()}'.");
            Assert.AreEqual(0,unit.GetHierarchyTier());
            Debug.WriteLine($"Должно быть = '{null}', unit.GetMainUnit() = '{unit.GetMainUnit()}'.");
            Assert.IsNull(unit.GetMainUnit());

            foreach (var pos in unit.GetPositions())
            {
                Debug.WriteLine($"Должно быть = '{true}', unit.GetPositions().pos.GetIsDelete() = '{pos.GetIsDelete()}'.");
                Assert.IsTrue(pos.GetIsDelete());
            }

            Debug.WriteLine($"Должно быть = '{0}', unit.GetSubordinateUnits() = '{unit.GetSubordinateUnits().Count}'.");
            Assert.AreEqual(0, unit.GetSubordinateUnits().Count);
            Debug.WriteLine($"Должно быть = '{mainUnit.GetName()}', subUnit1.GetMainUnit().GetName() = '{subUnit1.GetMainUnit().GetName()}'.");
            Assert.AreEqual(mainUnit, subUnit1.GetMainUnit());
            Debug.WriteLine($"Должно быть = '{mainUnit.GetName()}', subUnit2.GetMainUnit().GetName() = '{subUnit2.GetMainUnit().GetName()}'.");
            Assert.AreEqual(mainUnit, subUnit2.GetMainUnit());

            Debug.WriteLine($"Должно быть = '{true}', unit.DeletePosition().return = '{ret}'.");
            Assert.IsTrue(ret);

            Debug.WriteLine("Удаление Закончено. ");
        }
        #endregion
    }
}