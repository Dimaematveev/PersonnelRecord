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
        #region Поля
        /// <summary>
        /// Название подразделения
        /// </summary>
        private string name;
        public string GetName()
        {
            return name;
        }

        /// <summary>
        /// Главное подразделение
        /// </summary>
        private IUnit mainUnit;
        public IUnit GetMainUnit()
        {
            return mainUnit;
        }

        /// <summary>
        /// Список Подчиненных подразделений
        /// </summary>
        private List<IUnit> subordinateUnits;
        public IReadOnlyList<IUnit> GetSubordinateUnits()
        {
            return subordinateUnits.AsReadOnly();
        }

        /// <summary>
        /// Список Должностей
        /// </summary>
        private List<IPosition> positions;
        public IReadOnlyList<IPosition> GetPositions()
        {
            return positions.AsReadOnly();
        }
       
        /// <summary>
        /// Ярус иерархии
        /// </summary>
        private int hierarchyTier;
        public int GetHierarchyTier()
        {
            return hierarchyTier;
        }

        /// <summary>
        /// Удалено подразделение?
        /// </summary>
        private bool isDelete;
        public bool GetIsDelete()
        {
            return isDelete;
        }
        #endregion

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
            if (!deletedPosition.Delete())
            {
                return false;
            }

            positions.Remove(deletedPosition);
            return true;
        }

        public IReadOnlyList<IUnit> GetMainUnits()
        {
            List<IUnit> units = new List<IUnit>() { };
            
            IUnit lastMainUnit = mainUnit;
            while (lastMainUnit != null)
            {
                units.Add(lastMainUnit);
                lastMainUnit = lastMainUnit.GetMainUnit();
            }
            return units;
        }

        public bool ChangeMainUnit(IUnit newMainUnit)
        {
            if (newMainUnit == null)
            {
                return false;
            }
            if (newMainUnit.GetHierarchyTier() == 0)
            {
                return false;
            }
            if (!newMainUnit.GetIsDelete())
            {
                return false;
            }
            if (newMainUnit == this)
            {
                return false;
            }
            if (newMainUnit.GetMainUnits().Contains(this))
            {
                return false;
            }
            mainUnit = newMainUnit;
            hierarchyTier = newMainUnit.GetHierarchyTier() + 1;
            return true;
        }

        public bool AddSubordinateUnit(IUnit addedSubordinateUnit)
        {
            if (addedSubordinateUnit == null)
            {
                return false;
            }
            if (addedSubordinateUnit == this)
            {
                return false;
            }
            if (addedSubordinateUnit.GetMainUnit() != this)
            {
                return false;
            }

            subordinateUnits.Add(addedSubordinateUnit);
            return true;
        }

        public bool DeleteSubordinateUnit(IUnit deletedSubordinateUnit)
        {
            if (!subordinateUnits.Contains(deletedSubordinateUnit))
            {
                return false;
            }
            subordinateUnits.Remove(deletedSubordinateUnit);
            return true;
        }

        public bool Reassignment(IUnit newMainUnit)
        {
            //Удалить из главного
            if (!this.mainUnit.DeleteSubordinateUnit(this))
            {
                return false;
            }


            //Добавть в главное
            if (!newMainUnit.AddSubordinateUnit(this))
            {
                this.mainUnit.AddSubordinateUnit(this);
                return false;
            }

            // Изменить главное
            if (!ChangeMainUnit(this))
            {
                this.mainUnit.AddSubordinateUnit(this);
                newMainUnit.DeleteSubordinateUnit(this);
                return false;
            }
            
            return true;

        }

        public bool Delete()
        {
            foreach (var pos in positions)
            {
                if (!pos.IsPossibleDeletePosition())
                {
                    return false;
                }
            }
            foreach (var subUnit in subordinateUnits)
            {
                subUnit.Reassignment(this.GetMainUnit());
            }
            isDelete = true;
            mainUnit = null;
            hierarchyTier = 0;
            return true;
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
