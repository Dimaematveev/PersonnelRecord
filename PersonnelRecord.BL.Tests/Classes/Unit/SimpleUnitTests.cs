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

        [TestInitialize]
        public void TestInitialize()
        {
            Debug.WriteLine("Test Initialize");
            nameUnit = "Unit1";
            positionsName = new List<string>() { "Pos1", "Pos2" };
            unit = new SimpleUnit(nameUnit, positionsName);
        }

        [TestMethod()]
        public void RenameTest_NewNameToUnit2_trueReturned()
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
            Debug.WriteLine($"Должно быть = '{true}', unit.GetName().return = '{ret}'.");
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
        public void RenameTest_NewNameToWrongArgument_falseReturned(string newName)
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
            Debug.WriteLine($"Должно быть = '{false}', unit.GetName().return = '{ret}'.");
            Assert.IsFalse(ret);

            Debug.WriteLine("Переименование Закончено. ");
        }

        [TestMethod()]
        public void AddPositionTest_NewPositionToPos3_TrueReturnes()
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
            Debug.WriteLine($"Должно быть = '{true}', unit.GetName() = '{ret}'.");
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
        public void AddPositionTest_NewPositionToWrongArgument_FalseReturnes(string NewPosition)
        {
            Debug.WriteLine("Начало теста Добавление должности. Некорректные параметры, возврат False.");

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
            Debug.WriteLine($"Должно быть = '{false}', unit.GetName() = '{ret}'.");
            Assert.IsFalse(ret);

            Debug.WriteLine("Добавление должности Закончено. ");
        }

        [TestMethod()]
        public void DeletePositionTest_DeletePositionToPos1_trueReturned()
        {
            Debug.WriteLine("Начало теста Удаление должности. Корректные параметры, возврат true.");

            // Arrange(настройка)
            var DeletePosition = "Pos1";
            var Positions = unit.GetPositions().Select(x => x.GetName()).ToList();
            Positions.Remove(DeletePosition);
            Debug.WriteLine($"Удаляемая должность = '{DeletePosition}'.");

            // Act — выполнение 
            Debug.WriteLine("Начал удалять");
            var ret = unit.DeletePosition(unit.GetPositions().FirstOrDefault(x => x.GetName()== DeletePosition));
            Debug.WriteLine("Удалил");

            // Assert — проверка
            Debug.WriteLine($"Должно быть = '{Positions.Count}', unit.GetPositions() = '{unit.GetPositions().Count}'.");
            CollectionAssert.AreEqual(Positions, unit.GetPositions().Select(x => x.GetName()).ToList());
            Debug.WriteLine($"Должно быть = '{true}', unit.GetName() = '{ret}'.");
            Assert.IsTrue(ret);

            Debug.WriteLine("Удаление Закончено. ");
        }

        [DataTestMethod()]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow("  ")]
        [DataRow("\n")]
        [DataRow("\t")]
        public void DeletePositionTest_DeletePositionToWrongArgument_FalseReturnes(string DeletePosition)
        {
            Debug.WriteLine("Начало теста Удаление должности. Корректные параметры, возврат true.");

            // Arrange(настройка)
            var Positions = unit.GetPositions().Select(x => x.GetName()).ToList();
            Positions.Remove(DeletePosition);
            Debug.WriteLine($"Удаляемая должность = '{DeletePosition}'.");

            // Act — выполнение 
            Debug.WriteLine("Начал удалять");
            var ret = unit.DeletePosition(unit.GetPositions().FirstOrDefault(x => x.GetName() == DeletePosition));
            Debug.WriteLine("Удалил");

            // Assert — проверка
            Debug.WriteLine($"Должно быть = '{Positions.Count}', unit.GetPositions() = '{unit.GetPositions().Count}'.");
            CollectionAssert.AreEqual(Positions, unit.GetPositions().Select(x => x.GetName()).ToList());
            Debug.WriteLine($"Должно быть = '{true}', unit.GetName() = '{ret}'.");
            Assert.IsTrue(ret);

            Debug.WriteLine("Удаление Закончено. ");
        }


        [TestMethod()]
        public void ChangeMainSubdivisionTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AddSubordinateSubdivisionTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteSubordinateSubdivisionTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Assert.Fail();
        }
    }
}