using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersonnelRecord.BL.Interfaces;
using System;
using System.Diagnostics;

namespace PersonnelRecord.BL.Classes.Tests
{
   

 
    public class SimplePositionTests
    {
        private IPosition position;
        private IUnit subdivision;
        private string namePosition;
        // Запускается перед каждым стартом тестирующего метода
        // Если написать ClassInitialize то выполнится 1 раз, 
        // и мы будем работать с одним классом
        [TestInitialize]
        public void TestInitialize()
        {
            Debug.WriteLine("Test Initialize");
            namePosition = "Pos1";
         
            subdivision = new SimpleUnit("1",new System.Collections.Generic.List<string>() { "1" });
            position = new SimplePosition(namePosition, subdivision);

        }

        [TestMethod()]
        public void CreateSimplePosition_Pos1andSub1_ReadyClassReturned()
        {
            //Arrange (настройка) — в этом блоке кода мы настраиваем 
            //тестовое окружение тестируемого юнита;
            namePosition = "Pos1";
            subdivision = new SimpleUnit("1", new System.Collections.Generic.List<string>() { "1" });
            bool isWork = true;
            bool isDelete = false;

            // Act — выполнение или вызов тестируемого сценария;
            IPosition pos1 = new SimplePosition(namePosition, subdivision);

            // Assert — проверка того, что тестируемый вызов ведет себя 
            // определенным образом.
            Assert.AreEqual(namePosition, pos1.Name, "Название должности должно быть {0}, а получилось {1}.", namePosition, pos1.Name);
            Assert.AreSame(subdivision, pos1.Subdivision, "Подразделение должно быть {0}, а получилось {1}.", subdivision, pos1.Subdivision);
            Assert.AreEqual(isWork, pos1.IsWork, "IsWork должно быть {0}, а получилось {1}.", isWork, pos1.IsWork);
            Assert.AreEqual(isDelete, pos1.IsDelete, "IsDelete должно быть {0}, а получилось {1}.", isDelete, pos1.IsDelete);
        }

       
        
        [ExpectedException(typeof(ArgumentNullException), "Исключение на передачу в название должности null или пустой строки, не было вызвано.")]
        [DataTestMethod()]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow("  ")]
        [DataRow("\n")]
        [DataRow("\t")]
        public void CreateSimplePosition_NamePositionEqualNull_Exception(string namePosition)
        {
            
            //Arrange (настройка) — в этом блоке кода мы настраиваем 

            //тестовое окружение тестируемого юнита;
            Debug.WriteLine($"namePosition='{namePosition}'");
            // Act — выполнение или вызов тестируемого сценария;
            IPosition pos1 = new SimplePosition(namePosition, subdivision);

            // Assert — проверка того, что тестируемый вызов ведет себя 
            // определенным образом.
            
        }
        
        
        [ExpectedException(typeof(ArgumentNullException), "Исключение на передачу в название должности null или пустой строки, не было вызвано.")]
        [TestMethod()]
        public void CreateSimplePosition_SubdivisionEqualNull_Exception()
        {

            //Arrange (настройка) — в этом блоке кода мы настраиваем 

            //тестовое окружение тестируемого юнита;

            // Act — выполнение или вызов тестируемого сценария;
            IPosition pos1 = new SimplePosition(namePosition, null);

            // Assert — проверка того, что тестируемый вызов ведет себя 
            // определенным образом.

        }


        [TestMethod()]
        public void Delete_IsDelete_trueReturned()
        {
            //Arrange (настройка) — в этом блоке кода мы настраиваем 
            //тестовое окружение тестируемого юнита;
            bool isDelete = true;

            // Act — выполнение или вызов тестируемого сценария;
            position.Delete();

            // Assert — проверка того, что тестируемый вызов ведет себя 
            // определенным образом.
            Assert.AreEqual(isDelete, position.IsDelete, "IsDelete должно быть {0}, а получилось {1}.", isDelete, position.IsDelete);

        }



    }
}