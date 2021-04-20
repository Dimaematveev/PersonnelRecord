using PersonnelRecord.BL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelRecord.BL.Interfaces
{
    /// <summary>
    /// Интерфейс Динамика
    /// </summary>
    public interface IChange
    {
        /// <summary>
        /// Получить предыдущее изменение должности (предыдущую динамику)
        /// </summary>
        /// <returns>Предыдущая динамика</returns>
        IChange GetPreviousChange();

        /// <summary>
        /// Получить ID Динамики
        /// </summary>
        /// <returns>ID Динамики</returns>
        int GetID();

        /// <summary>
        /// Получить Номер приказа
        /// </summary>
        /// <returns>Номер приказа</returns>
        int GetNumberOrder();

        /// <summary>
        /// Получить дату внесения данных
        /// </summary>
        /// <returns>Дата изменения</returns>
        DateTime GetDateChange();

        /// <summary>
        /// Получить занимаемую должность
        /// </summary>
        /// <returns>Должность</returns>
        IPosition GetPosition();

        /// <summary>
        /// Получить сотрудника
        /// </summary>
        /// <returns>Сотрудник</returns>
        IEmployee GetEmployee();

        /// <summary>
        /// Совмещает должность?
        /// </summary>
        /// <returns>True - Совмещает, false - нет</returns>
        bool GetIsCombination();

        /// <summary>
        /// Получить статус сотрудника. 
        /// Занимает эту должность или нет
        /// </summary>
        /// <returns>True - Занимает должность, false - нет</returns>
        bool GetStatus();

        /// <summary>
        /// Получить тип изменения
        /// </summary>
        /// <returns>Тип записи</returns>
        RecordType GetRecordType();

        /// <summary>
        /// Изменить статус сотрудника на False
        /// </summary>
        /// <returns>true - удалили флаг работающего человека, false- нет получилось</returns>
        bool ChangeStatusFalse();

    }
}
