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
        /// <summary>
        /// Получить Название подразделения
        /// </summary>
        /// <returns>Название подразделения</returns>
        public string GetName()
        {
            return name;
        }

        /// <summary>
        /// Главное подразделение
        /// </summary>
        private IUnit mainUnit;

        /// <summary>
        /// Получить главное подразделение
        /// </summary>
        /// <returns>Главное подразделение</returns>
        public IUnit GetMainUnit()
        {
            return mainUnit;
        }

        /// <summary>
        /// Список Подчиненных подразделений
        /// </summary>
        private List<IUnit> subordinateUnits;
        /// <summary>
        /// Получить список подчиненных(дочерних) подразделений 
        /// </summary>
        /// <returns>Список подчиненных подразделений</returns>
        public IReadOnlyList<IUnit> GetSubordinateUnits()
        {
            return subordinateUnits.AsReadOnly();
        }

        /// <summary>
        /// Список Должностей
        /// </summary>
        private List<IPosition> positions;
        /// <summary>
        /// Получить список должностей этого подразделения
        /// </summary>
        /// <returns>Список должностей</returns>
        public IReadOnlyList<IPosition> GetPositions()
        {
            return positions.AsReadOnly();
        }
       
        /// <summary>
        /// Ярус иерархии
        /// </summary>
        private int hierarchyTier;
        /// <summary>
        /// Получить ярус иерархии
        /// </summary>
        /// <returns>Ярус иерархии</returns>
        public int GetHierarchyTier()
        {
            return hierarchyTier;
        }

        /// <summary>
        /// Удалено подразделение?
        /// </summary>
        private bool isDelete;

        /// <summary>
        /// Узнать удалено ли подразделение
        /// </summary>
        /// <returns>True - Удалено, False - нет</returns>
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

        /// <summary>
        /// Переименовать подразделение
        /// </summary>
        /// <param name="newName">Новое название подразделения</param>
        /// <returns>True - Удалось переименовать, False - нет</returns>
        public bool Rename(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
            {
                return false;
            }
            name = newName;
            return true;
        }

        /// <summary>
        /// Добавить должность подразделению по названию
        /// </summary>
        /// <param name="newPosition">Название новой должности</param>
        /// <returns>True - Добавили должность, False - нет</returns>
        public bool AddPosition(string newPosition)
        {
            if (string.IsNullOrWhiteSpace(newPosition))
            {
                return false;
            }
            positions.Add(new SimplePosition(newPosition, this));
            return true;
        }

        /// <summary>
        /// Удалить должность у подразделение
        /// </summary>
        /// <param name="deletedPosition"> Удаляемая должность</param>
        /// <returns>True - Удалили должность, False - нет</returns>
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

        /// <summary>
        /// Получить список всех главных подразделений
        /// </summary>
        /// <returns>Список подразделений</returns>
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

        /// <summary>
        /// Изменить Главное подразделение
        /// </summary>
        /// <param name="newMainUnit">Новое главное подразделение</param>
        /// <returns>True - Изменили главное подразделение, False - нет</returns>
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

        /// <summary>
        /// Добавить подчиненное подразделение
        /// </summary>
        /// <param name="addedSubordinateUnit">Добавляемое подчиненное подразделение</param>
        /// <returns>True - Добавили подчиненное подразделение, False - нет</returns>
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
        /// <summary>
        /// Удалить подчиненное подразделение
        /// </summary>
        /// <param name="deletedSubordinateUnit">Удаляемое подчиненное подразделение</param>
        /// <returns>True - Удалили подчиненное подразделение, False - нет</returns>
        public bool DeleteSubordinateUnit(IUnit deletedSubordinateUnit)
        {
            if (!subordinateUnits.Contains(deletedSubordinateUnit))
            {
                return false;
            }
            subordinateUnits.Remove(deletedSubordinateUnit);
            return true;
        }

        /// <summary>
        ///  Переподчинение подразделения
        /// </summary>
        /// <param name="newMainUnit">Новое главное подразделение</param>
        /// <returns>True - переподчинили  подразделение, False - нет</returns>
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

        /// <summary>
        /// Удалить подразделение
        /// </summary>
        /// <returns>True - Удалили подразделение, False - нет</returns>
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
