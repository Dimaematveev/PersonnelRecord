using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelRecord.BL.Interfaces
{
    /// <summary>
    /// Интерфейс Подразделение
    /// </summary>
    public interface IUnit
    {
        /// <summary>
        /// Получить Название подразделения
        /// </summary>
        /// <returns>Название подразделения</returns>
        string GetName();

        /// <summary>
        /// Получить главное подразделение
        /// </summary>
        /// <returns>Главное подразделение</returns>
        IUnit GetMainUnit();

        /// <summary>
        /// Получить список подчиненных(дочерних) подразделений 
        /// </summary>
        /// <returns>Список подчиненных подразделений</returns>
        IReadOnlyList<IUnit> GetSubordinateUnits();

        /// <summary>
        /// Получить список должностей этого подразделения
        /// </summary>
        /// <returns>Список должностей</returns>
        IReadOnlyList<IPosition> GetPositions();

        /// <summary>
        /// Получить ярус иерархии
        /// </summary>
        /// <returns>Ярус иерархии</returns>
        int GetHierarchyTier();


        /// <summary>
        /// Узнать удалено ли подразделение
        /// </summary>
        /// <returns>True - Удалено, False - нет</returns>
        bool GetIsDelete();

        /// <summary>
        /// Переименовать подразделение
        /// </summary>
        /// <param name="newName">Новое название подразделения</param>
        void Rename(string newName);

        /// <summary>
        /// Добавить должность подразделению по названию
        /// </summary>
        /// <param name="newPosition">Название новой должности</param>
        void AddPosition(string newPosition);

        /// <summary>
        /// Удалить должность у подразделение
        /// </summary>
        /// <param name="deletedPosition"> Удаляемая должность</param>
        void DeletePosition(IPosition deletedPosition);

        /// <summary>
        /// Изменить Главное подразделение
        /// </summary>
        /// <param name="newMainUnit">Новое главное подразделение</param>
        void ChangeMainSubdivision(IUnit newMainUnit);

        /// <summary>
        /// Добавить подчиненное подразделение
        /// </summary>
        /// <param name="addedSubordinateUnit">Добавляемое подчиненное подразделение</param>
        void AddSubordinateSubdivision(IUnit addedSubordinateUnit);

        /// <summary>
        /// Удалить подчиненное подразделение
        /// </summary>
        /// <param name="deletedSubordinateUnit">Удаляемое подчиненное подразделение</param>
        void DeleteSubordinateSubdivision(IUnit deletedSubordinateUnit);

        /// <summary>
        /// Удалить подразделение
        /// </summary>
        void Delete();
    }
}
