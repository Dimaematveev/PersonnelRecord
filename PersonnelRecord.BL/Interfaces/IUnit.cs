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
        /// <returns>True - Удалось переименовать, False - нет</returns>
        bool Rename(string newName);

        /// <summary>
        /// Добавить должность подразделению по названию
        /// </summary>
        /// <param name="newPosition">Название новой должности</param>
        /// <returns>True - Добавили должность, False - нет</returns>
        bool AddPosition(string newPosition);

        /// <summary>
        /// Удалить должность у подразделение
        /// </summary>
        /// <param name="deletedPosition"> Удаляемая должность</param>
        /// <returns>True - Удалили должность, False - нет</returns>
        bool DeletePosition(IPosition deletedPosition);

        /// <summary>
        /// Получить список всех главных подразделений
        /// </summary>
        /// <returns>Список подразделений</returns>
        IReadOnlyList<IUnit> GetMainUnits();

        /// <summary>
        /// Изменить Главное подразделение
        /// </summary>
        /// <param name="newMainUnit">Новое главное подразделение</param>
        /// <returns>True - Изменили главное подразделение, False - нет</returns>
        bool ChangeMainUnit(IUnit newMainUnit);

        /// <summary>
        ///  Переподчинение подразделения
        /// </summary>
        /// <param name="newMainUnit">Новое главное подразделение</param>
        /// <returns>True - переподчинили  подразделение, False - нет</returns>
        bool Reassignment(IUnit newMainUnit);

        /// <summary>
        /// Добавить подчиненное подразделение
        /// </summary>
        /// <param name="addedSubordinateUnit">Добавляемое подчиненное подразделение</param>
        /// <returns>True - Добавили подчиненное подразделение, False - нет</returns>
        bool AddSubordinateUnit(IUnit addedSubordinateUnit);

        /// <summary>
        /// Удалить подчиненное подразделение
        /// </summary>
        /// <param name="deletedSubordinateUnit">Удаляемое подчиненное подразделение</param>
        /// <returns>True - Удалили подчиненное подразделение, False - нет</returns>
        bool DeleteSubordinateUnit(IUnit deletedSubordinateUnit);

        /// <summary>
        /// Удалить подразделение
        /// </summary>
        /// <returns>True - Удалили подразделение, False - нет</returns>
        bool Delete();
    }
}
