using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelRecord.BL.Interfaces
{
    /// <summary>
    /// Интерфейс Должность
    /// </summary>
    public interface IPosition
    {
        /// <summary>
        /// Получить название должности
        /// </summary>
        /// <returns>Название должности</returns>
        string GetName();

        /// <summary>
        /// Получить подразделение к которому относится должность
        /// </summary>
        /// <returns>Подразделение</returns>
        IUnit GetUnit();

        /// <summary>
        /// Занята ли должность
        /// </summary>
        /// <returns> True - Занята, False - нет</returns>
        bool GetIsPositionBusy();

        /// <summary>
        /// Удалена ли должность
        /// </summary>
        /// <returns> True - Удалена, False - нет</returns>
        bool GetIsDelete();

        /// <summary>
        /// Удалить должность
        /// </summary>
        void Delete();
    }
}
