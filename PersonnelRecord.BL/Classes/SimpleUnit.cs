using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonnelRecord.BL.Interfaces;

namespace PersonnelRecord.BL.Classes
{
    /// <summary>
    /// Класс простое Подразделение
    /// </summary>
    public class SimpleUnit : IUnit
    {
        /// <summary>
        /// Название подразделения
        /// </summary>
        private string name;

        /// <summary>
        /// Главное подразделение
        /// </summary>
        private IUnit mainUnit;

        /// <summary>
        /// Список Подчиненных подразделений
        /// </summary>
        private List<IUnit> subordinateUnits;

        /// <summary>
        /// Список Должностей
        /// </summary>
        private List<IPosition> positions;
        
        /// <summary>
        /// Ярус иерархии
        /// </summary>
        private int hierarchyTier;

        /// <summary>
        /// Удалено подразделение?
        /// </summary>
        private bool isDelete;

        public SimpleUnit(string nameUnit, List<string> positionsName)
        {
            name = nameUnit;
            mainUnit = null;
            subordinateUnits = new List<IUnit>();
            positions = new List<IPosition>();
            foreach (var positionName in positionsName)
            {
                positions.Add(new SimplePosition(positionName, this));
            }
            hierarchyTier = 0;
        }

        public string GetName()
        {
            throw new NotImplementedException();
        }

        public IUnit GetMainUnit()
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<IUnit> GetSubordinateUnits()
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<IPosition> GetPositions()
        {
            throw new NotImplementedException();
        }

        public int GetHierarchyTier()
        {
            throw new NotImplementedException();
        }

        public bool GetIsDelete()
        {
            throw new NotImplementedException();
        }

        public void Rename(string newName)
        {
            throw new NotImplementedException();
        }

        public void AddPosition(string newPosition)
        {
            throw new NotImplementedException();
        }

        public void DeletePosition(IPosition deletedPosition)
        {
            throw new NotImplementedException();
        }

        public void ChangeMainSubdivision(IUnit newMainUnit)
        {
            throw new NotImplementedException();
        }

        public void AddSubordinateSubdivision(IUnit addedSubordinateUnit)
        {
            throw new NotImplementedException();
        }

        public void DeleteSubordinateSubdivision(IUnit deletedSubordinateUnit)
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

      

        //public void Rename(string newName)
        //{
        //    Name = newName;
        //}

        //public void DeletePosition(IPosition deletePosition)
        //{
        //    Positions.Remove(deletePosition);
        //}

        //public void AddPosition(string newPosition)
        //{
        //    Positions.Add(new SimplePosition(newPosition, this));
        //}

        //public void ChangeMainSubdivision(IUnit newMainSubdivision)
        //{
        //    MainSubdivision = newMainSubdivision;
        //    HierarchyTier = MainSubdivision.HierarchyTier + 1;
        //}

        //public void AddSubordinateSubdivision(IUnit newSubordinateSubdivision)
        //{
        //    SubordinateSubdivisions.Add(newSubordinateSubdivision);
        //}

        //public void DeleteSubordinateSubdivision(IUnit deleteSubordinateSubdivision)
        //{
        //    SubordinateSubdivisions.Remove(deleteSubordinateSubdivision);
        //}

        //public void Delete()
        //{
        //    MainSubdivision.DeleteSubordinateSubdivision(this);
        //    SubordinateSubdivisions.ForEach(x => x.ChangeMainSubdivision(MainSubdivision));
        //    MainSubdivision = null;
        //    SubordinateSubdivisions = new List<IUnit>();

        //    HierarchyTier = 0;
        //}

       
    }
}
