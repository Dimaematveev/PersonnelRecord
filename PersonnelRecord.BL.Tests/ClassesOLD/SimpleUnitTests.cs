using PersonnelRecord.BL.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersonnelRecord.BL.Interfaces;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PersonnelRecord.BL.Classes.Tests
{
   
    public class SimpleUnitTests
    {

        private IUnit subdivision;
        private string nameSubdivision;
        private List<string> positionsName;
        // Запускается перед каждым стартом тестирующего метода
        // Если написать ClassInitialize то выполнится 1 раз, 
        // и мы будем работать с одним классом
        [TestInitialize]
        public void TestInitialize()
        {
            Debug.WriteLine("Test Initialize");
            nameSubdivision = "Subdiv1";
            positionsName = new List<string>() { "Pos1", "Pos2" };
            subdivision = new SimpleUnit(nameSubdivision, positionsName);
        }



        [TestMethod()]
        public void CreateSimpleSubdivision_Subdiv1andPos1Pos2_ReadyClassReturned()
        {
            //Arrange (настройка) — в этом блоке кода мы настраиваем 
            //тестовое окружение тестируемого юнита;
            nameSubdivision = "Subdiv1";
            positionsName = new List<string>() { "Pos1", "Pos2" };
            IUnit mainSubdivision = null;
            List<IUnit> subordinateSubdivisions = new List<IUnit>();
            int hierarchyTier = 0;

            // Act — выполнение или вызов тестируемого сценария;
            IUnit subdiv1 = new SimpleUnit(nameSubdivision, positionsName);

            // Assert — проверка того, что тестируемый вызов ведет себя 
            // определенным образом.
            Assert.AreEqual(nameSubdivision, subdiv1.Name, "Название подразделения должно быть {0}, а получилось {1}.", nameSubdivision, subdiv1.Name);
            CollectionAssert.AreEqual(positionsName, subdiv1.Positions.Select(x => x.Name).ToList(), "Должности должны быть {0}, а получились {1}.", subdivision, subdiv1.Positions.Select(x => x.Name).ToList());
            Assert.AreEqual(mainSubdivision, subdiv1.MainSubdivision, "Главное подразделение должно быть {0}, а получилось {1}.", mainSubdivision, subdiv1.MainSubdivision);
            CollectionAssert.AreEqual(subordinateSubdivisions, subdiv1.SubordinateSubdivisions, "Подчиненные подразделения должны быть {0}, а получилось {1}.", subordinateSubdivisions, subdiv1.SubordinateSubdivisions);
            Assert.AreEqual(hierarchyTier, subdiv1.HierarchyTier, "Ярус иерархии должен быть {0}, а получилось {1}.", hierarchyTier, subdiv1.HierarchyTier);
        }

        [TestMethod()]
        public void Rename_NameSubdiv1ToSubdiv2()
        {
            //Arrange
            string newName = "Subdiv2";
            // Act
            subdivision.Rename(newName);
            // Assert
            Assert.AreEqual(newName, subdivision.Name, "Новое название подразделения должно быть {0}, а у нас {1}", newName, subdivision.Name);

        }

        [TestMethod()]
        public void DeletePosition_Pos1()
        {
            //Arrange
            IPosition DelPos = subdivision.Positions.FirstOrDefault(x => x.Name == "Pos2");
            // Act
            subdivision.DeletePosition(DelPos);
            // Assert
            Assert.AreEqual(1, subdivision.Positions.Count);

        }


        [TestMethod()]
        public void AddPosition_Pos3()
        {
            //Arrange
            string newPosition = "Pos3";
            // Act
            subdivision.AddPosition(newPosition);
            // Assert
            Assert.AreEqual(3, subdivision.Positions.Count);
        }

        [TestMethod()]
        public void ChangeMainSubdivision_ToNewMain()
        {
            //Arrange
            IUnit subdiv1 = new SimpleUnit("1", new List<string>() { "11" });
            // Act
            subdivision.ChangeMainSubdivision(subdiv1);
            // Assert
            Assert.AreEqual(subdiv1, subdivision.MainSubdivision);
        }

        [TestMethod()]
        public void AddSubordinateSubdivision_ToNewMain()
        {
            //Arrange
            IUnit subdiv1 = new SimpleUnit("1", new List<string>() { "11" });
            // Act
            subdivision.AddSubordinateSubdivision(subdiv1);
            // Assert
            Assert.AreEqual(subdiv1, subdivision.SubordinateSubdivisions[0]);
        }

        [TestMethod()]
        public void DeleteSubordinateSubdivision_ToNewMain()
        {
            //Arrange
            IUnit subdiv1 = new SimpleUnit("1", new List<string>() { "11" });
            IUnit subdiv2 = new SimpleUnit("2", new List<string>() { "22" });
            // Act
            subdivision.AddSubordinateSubdivision(subdiv1);
            subdivision.AddSubordinateSubdivision(subdiv2);

            subdivision.DeleteSubordinateSubdivision(subdiv1);
            // Assert
            Assert.AreEqual(subdiv2, subdivision.SubordinateSubdivisions[0]);
        }

        [TestMethod()]
        public void Delete_ToNewMain()
        {
            //Arrange

            // Act
            subdivision.Delete();
            // Assert
            Assert.AreEqual(null, subdivision.MainSubdivision);
            Assert.AreEqual(0, subdivision.SubordinateSubdivisions.Count);
            Assert.AreEqual(0, subdivision.HierarchyTier);
        }

        [TestMethod()]
        public void GetNameTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetMainUnitTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetSubordinateUnitsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetPositionsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetHierarchyTierTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetIsDeleteTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void RenameTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AddPositionTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeletePositionTest()
        {
            Assert.Fail();
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