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
            nameUnit = "Subdiv1";
            positionsName = new List<string>() { "Pos1", "Pos2" };
            unit = new SimpleUnit(nameUnit, positionsName);
        }

        [TestMethod()]
        public void SimpleUnit_()
        {
            Assert.Fail();
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