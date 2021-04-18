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
            hierarchyTier = 0;
            mainUnit = null;
            subordinateUnits = new List<IUnit>();
            positions = new List<IPosition>();
            foreach (var positionName in positionsName)
            {
                positions.Add(new SimplePosition(positionName, this));
            }
            
        }

        public string GetName()
        {
            return name;
        }

        public IUnit GetMainUnit()
        {
            return mainUnit;
        }

        public IReadOnlyList<IUnit> GetSubordinateUnits()
        {
            return subordinateUnits.AsReadOnly();
        }

        public IReadOnlyList<IPosition> GetPositions()
        {
            return positions.AsReadOnly();
        }

        public int GetHierarchyTier()
        {
            return hierarchyTier;
        }

        public bool GetIsDelete()
        {
            return isDelete;
        }

        public bool Rename(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
            {
                return false;
            }
            name = newName;
            return true;
        }

        public bool AddPosition(string newPosition)
        {
            if (string.IsNullOrWhiteSpace(newPosition))
            {
                return false;
            }
            positions.Add(new SimplePosition(newPosition, this));
            return true;
        }

        public bool DeletePosition(IPosition deletedPosition)
        {
            if (deletedPosition == null)
            {
                return false;
            }
            if (!positions.Contains(deletedPosition))
            {
                return false;
            }
            positions.Remove(deletedPosition);
            return true;
        }

        public IReadOnlyList<IUnit> GetMainUnits()
        {
            throw new NotImplementedException();
        }

        public bool ChangeMainUnit(IUnit newMainUnit)
        {
            throw new NotImplementedException();
        }

        public bool AddSubordinateUnit(IUnit addedSubordinateUnit)
        {
            throw new NotImplementedException();
        }

        public bool DeleteSubordinateUnit(IUnit deletedSubordinateUnit)
        {
            throw new NotImplementedException();
        }

        public bool Delete()
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
