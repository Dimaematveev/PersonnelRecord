using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonnelRecord.BL.Classes
{
    /// <summary>
    /// Класс Подразделение
    /// </summary>
    public class Unit
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
        private Unit mainUnit;

        /// <summary>
        /// Получить главное подразделение
        /// </summary>
        /// <returns>Главное подразделение</returns>
        public Unit GetMainUnit()
        {
            return mainUnit;
        }

        /// <summary>
        /// Список Подчиненных подразделений
        /// </summary>
        private List<Unit> subordinateUnits;
        /// <summary>
        /// Получить не редактируемый список подчиненных(дочерних) подразделений 
        /// </summary>
        /// <returns>Список подчиненных подразделений</returns>
        public IReadOnlyList<Unit> GetSubordinateUnits()
        {
            return subordinateUnits.AsReadOnly();
        }

        /// <summary>
        /// Список Должностей
        /// </summary>
        private List<Position> positions;
        /// <summary>
        /// Получить не редактируемый список должностей этого подразделения
        /// </summary>
        /// <returns>Список должностей</returns>
        public IReadOnlyList<Position> GetPositions()
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
        /// <returns>
        /// <para><c>True</c> - Удалено</para>
        /// <para><c>False</c> - нет</para>
        /// </returns>
        public bool GetIsDelete()
        {
            return isDelete;
        }
        #endregion



        /// <summary>
        /// Получить список всех главных подразделений
        /// </summary>
        /// <returns>Список подразделений</returns>
        public IReadOnlyList<Unit> GetMainUnits()
        {
            List<Unit> units = new List<Unit>() { };

            Unit lastMainUnit = mainUnit;
            while (lastMainUnit != null)
            {
                units.Add(lastMainUnit);
                lastMainUnit = lastMainUnit.GetMainUnit();
            }
            return units;
        }

        /// <summary>
        /// Конструктор Подразделения
        /// </summary>
        /// <param name="nameUnit">Название подразделения</param>
        /// <param name="positionsName">Список должностей</param>
        /// <param name="IsMain">
        /// <para><c>True</c> - Самое главное подразделение(только одно)</para>
        /// <para><c>False</c> - Обычное подразделение</para>
        /// </param>
        public Unit(string nameUnit, List<string> positionsName, bool IsMain = false)
        {
            if (string.IsNullOrWhiteSpace(nameUnit))
            {
                throw new ArgumentNullException("Название подразделения не должно быть пустым!!!");
            }
            if (positionsName == null)
            {
                throw new ArgumentNullException("Список должностей не должен быть null !!!");
            }
            if (positionsName.Count == 0)
            {
                throw new ArgumentNullException("Список должностей не должен быть пустым !!!");
            }
            foreach (var item in positionsName)
            {
                if (string.IsNullOrWhiteSpace(item))
                {
                    throw new ArgumentNullException("Название должности не должно быть пустым!!!");
                }
            }

            name = nameUnit;
            if (IsMain)
            {
                hierarchyTier = 1;
            }
            else
            {
                hierarchyTier = 0;
            }
           
            mainUnit = null;
            isDelete = false;
            subordinateUnits = new List<Unit>();
            positions = new List<Position>();
            foreach (var positionName in positionsName)
            {
                positions.Add(new Position(positionName, this));
            }

        }



        /// <summary>
        /// Переименовать подразделение
        /// </summary>
        /// <param name="newName">Новое название подразделения</param>
        /// <returns>
        /// <para><c>True</c> - Удалось переименовать</para>
        /// <para><c>False</c> - нет</para>
        /// </returns>
        public bool Rename(string newName)
        {
            
            if (string.IsNullOrWhiteSpace(newName))
            {
                return false;
            }
            if (name == newName)
            {
                return false;
            }
            name = newName;
            return true;
        }

        /// <summary>
        /// Возможно-ли Добавить должность подразделению по названию
        /// </summary>
        /// <param name="newPosition">Название новой должности</param>
        /// <returns>
        /// Любая строка это ошибка, почему нельзя это сделать, 
        /// Null - возможно
        /// </returns>
        public string IsPossibleAddPosition(string newPosition)
        {
            if (string.IsNullOrWhiteSpace(newPosition))
            {
                return "Новая должность пуста!!";
            }
            return null;
        }

        /// <summary>
        /// Добавить должность подразделению по названию
        /// </summary>
        /// <param name="newPosition">Название новой должности</param>
        /// <returns>
        /// <para><c>True</c> - Добавили должность</para>
        /// <para><c>False</c> - нет</para>
        /// </returns>
        public bool AddPosition(string newPosition)
        {
            string Error = IsPossibleAddPosition(newPosition);
            if (Error != null)
            {
                return false;
            }

            positions.Add(new Position(newPosition, this));
            return true;
        }

        /// <summary>
        /// Возможно-ли Удалить должность у подразделение
        /// </summary>
        /// <param name="deletedPosition"> Удаляемая должность</param>
        /// <returns>
        /// Любая строка это ошибка, почему нельзя это сделать, 
        /// Null - возможно
        /// </returns>
        public string IsPossibleDeletePosition(Position deletedPosition)
        {
            if (deletedPosition == null)
            {
                return "Должность есть 'NULL'";
            }

            if (!positions.Contains(deletedPosition))
            {
                return "Должности нет в списке 'deletedPosition'";
            }

            if (!deletedPosition.IsPossibleDeletePosition())
            {
                return "Нельзя удалить должность";
            }

            return null;
        }

        /// <summary>
        /// Удалить должность у подразделение
        /// </summary>
        /// <param name="deletedPosition"> Удаляемая должность</param>
        /// <returns>
        /// <para><c>True</c> - Удалили должност</para>
        /// <para><c>False</c> - нет</para>
        /// </returns>
        public bool DeletePosition(Position deletedPosition)
        {
            string Error = IsPossibleDeletePosition(deletedPosition);
            if (Error != null)
            {
                return false;
            }
            deletedPosition.Delete();
            positions.Remove(deletedPosition);

            return true;
        }



        /// <summary>
        /// Возможно-ли Изменить Главное подразделение
        /// </summary>
        /// <param name="newMainUnit">Новое главное подразделение</param>
        /// <returns>
        /// Любая строка это ошибка, почему нельзя это сделать, 
        /// Null - возможно
        /// </returns>
        public string IsPossibleChangeMainUnit(Unit newMainUnit)
        {
            if (newMainUnit == null)
            {
                return "Новое главное подразделение есть 'NULL'";
            }
            if (newMainUnit.GetHierarchyTier() == 0)
            {
                return "Новое главное подразделение с ярусом '0'";
            }
            if (newMainUnit.GetIsDelete())
            {
                return "Новое главное подразделение 'Удалено'";
            }
            if (newMainUnit == this)
            {
                return "Новое главное подразделение есть наше подразделение";
            }
            if (newMainUnit.GetMainUnits().Contains(this))
            {
                return "Новое главное подразделение есть одно из дочерних нашего подразделения";
            }
            return null;
           
        }

        /// <summary>
        /// Изменить Главное подразделение
        /// </summary>
        /// <param name="newMainUnit">Новое главное подразделение</param>
        /// <returns>
        /// <para><c>True</c> -  Изменили главное подразделение</para>
        /// <para><c>False</c> - нет</para>
        /// </returns>
        public bool ChangeMainUnit(Unit newMainUnit)
        {
            if (IsPossibleChangeMainUnit(newMainUnit) != null)
            {
                return false;
            }
           
            mainUnit = newMainUnit;
            hierarchyTier = newMainUnit.GetHierarchyTier() + 1;
            return true;
        }

        /// <summary>
        /// Возможно ли Добавить подчиненное подразделение
        /// </summary>
        /// <param name="addedSubordinateUnit">Добавляемое подчиненное подразделение</param>
        /// <returns>
        /// <para><c>True</c> - возможно</para>
        /// <para><c>False</c> - нет</para>
        /// </returns>
        public bool IsPossibleAddSubordinateUnit(Unit addedSubordinateUnit)
        {
            if (addedSubordinateUnit == null)
            {
                return false;
            }
            if (addedSubordinateUnit == this)
            {
                return false;
            }
            if (this.GetSubordinateUnits().Contains(addedSubordinateUnit))
            {
                return false;
            }

            
            if (this.GetMainUnits().Contains(addedSubordinateUnit))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Добавить подчиненное подразделение
        /// </summary>
        /// <param name="addedSubordinateUnit">Добавляемое подчиненное подразделение</param>
        /// <returns>
        /// <para><c>True</c> - Добавили подчиненное подразделение </para>
        /// <para><c>False</c> - нет</para>
        /// </returns>
        public bool AddSubordinateUnit(Unit addedSubordinateUnit)
        {
            if (!IsPossibleAddSubordinateUnit(addedSubordinateUnit))
            {
                return false;
            }
          
            subordinateUnits.Add(addedSubordinateUnit);
            return true;
        }



        /// <summary>
        /// Возможно ли Удалить подчиненное подразделение
        /// </summary>
        /// <param name="deletedSubordinateUnit">Удаляемое подчиненное подразделение</param>
        /// <returns>
        /// <para><c>True</c> - Возможно</para>
        /// <para><c>False</c> - нет</para>
        /// </returns>
        public bool IsPossibleDeleteSubordinateUnit(Unit deletedSubordinateUnit)
        {
            if (subordinateUnits == null)
            {
                return false;
            }
            if (!subordinateUnits.Contains(deletedSubordinateUnit))
            {
                return false;
            }
           
            return true;
        }

        /// <summary>
        /// Удалить подчиненное подразделение
        /// </summary>
        /// <param name="deletedSubordinateUnit">Удаляемое подчиненное подразделение</param>
        /// <returns>
        /// <para><c>True</c> - Удалили подчиненное подразделение</para>
        /// <para><c>False</c> - нет</para>
        /// </returns>
        public bool DeleteSubordinateUnit(Unit deletedSubordinateUnit)
        {
            if (!IsPossibleDeleteSubordinateUnit(deletedSubordinateUnit))
            {
                return false;
            }

            subordinateUnits.Remove(deletedSubordinateUnit);
            return true;
        }

        /// <summary>
        ///  Возможно ли Переподчинение подразделения
        /// </summary>
        /// <param name="newMainUnit">Новое главное подразделение</param>
        /// <returns>
        /// <para><c>True</c> - возможно</para>
        /// <para><c>False</c> - нет</para>
        /// </returns>
        public bool IsPossibleReassignment(Unit newMainUnit)
        {
            if (newMainUnit == null)
            {
                return false;
            }

            //Возможно ли Удалить из главного
            if (mainUnit != null && !this.mainUnit.IsPossibleDeleteSubordinateUnit(this))
            {
                return false;
            }


            //Возможно ли Добавить в главное
            if (!newMainUnit.IsPossibleAddSubordinateUnit(this))
            {
                return false;
            }


            //Возможно ли Изменить главное
            if (IsPossibleChangeMainUnit(newMainUnit) != null)
            {
                return false;
            }

            return true;

        }

        /// <summary>
        ///  Переподчинение подразделения
        /// </summary>
        /// <param name="newMainUnit">Новое главное подразделение</param>
        /// <returns>
        /// <para><c>True</c> - переподчинили  подразделение</para>
        /// <para><c>False</c> - нет</para>
        /// </returns>
        public bool Reassignment(Unit newMainUnit)
        {
            if (!IsPossibleReassignment(newMainUnit))
            {
                return false;
            }
            //Удалить из главного
            if (mainUnit != null)
            {
                mainUnit.DeleteSubordinateUnit(this);
            }
           
            //Добавить в главное
            newMainUnit.AddSubordinateUnit(this);

            // Изменить главное
            ChangeMainUnit(newMainUnit);

            return true;

        }

        /// <summary>
        /// Возможно ли Удалить подразделение
        /// </summary>
        /// <returns>
        /// <para><c>True</c> - Возможно</para>
        /// <para><c>False</c> - нет</para>
        /// </returns>
        public bool IsPossibleDelete()
        {
            foreach (var pos in positions)
            {
                if (IsPossibleDeletePosition(pos) != null)
                {
                    return false;
                }
            }
            foreach (var subUnit in subordinateUnits)
            {
                if (!subUnit.IsPossibleReassignment(this.GetMainUnit()))
                {
                    return false;
                }
                
            }
           
            return true;
        }

        /// <summary>
        /// Удалить подразделение
        /// </summary>
        /// <returns>
        /// <para><c>True</c> - Удалили подразделение</para>
        /// <para><c>False</c> - нет</para>
        /// </returns>
        public bool Delete()
        {
            if(!IsPossibleDelete())
            {
                return false;
            }

            for (int i = positions.Count - 1; i >= 0; i--)
            {
                var pos = positions[i];
                DeletePosition(pos);
            }

            for (int i = subordinateUnits.Count - 1; i >= 0; i--)
            {
                var subUnit = subordinateUnits[i];
                subUnit.Reassignment(this.GetMainUnit());
            }

            isDelete = true;
            mainUnit = null;
            hierarchyTier = 0;
            return true;
        }


    }
}
