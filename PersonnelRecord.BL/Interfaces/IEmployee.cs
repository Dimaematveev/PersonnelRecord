using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelRecord.BL.Interfaces
{
    /// <summary>
    /// Интерфейс Сотрудник
    /// </summary>
    public interface IEmployee
    {
        /// <summary>
        /// Получить ID
        /// </summary>
        /// <returns>ID сотрудника(Таб.номер)</returns>
        int GetID();

        /// <summary>
        /// Получить ФИО
        /// </summary>
        /// <returns>Строка с ФИО сотрудника</returns>
        string GetFullName();

        /// <summary>
        /// Получить Дату рождения
        /// </summary>
        /// <returns>Дата рождения</returns>
        DateTime GetBirthday();

        /// <summary>
        /// Получить неизменяемый список динамики сотрудника
        /// </summary>
        /// <returns>Список динамики</returns>
        IReadOnlyList<IChange> GetChanges();



        /// <summary>
        /// Принятие на работу. 
        /// Основная должность.
        /// </summary>
        /// <param name="numberOrder">Номер приказа</param>
        /// <param name="position">Должность</param>
        void Recruitment(int numberOrder, IPosition position);
        
        /// <summary>
        /// Добавить должность на совмещение. 
        /// Должность по совмещению.
        /// </summary>
        /// <param name="numberOrder">Номер приказа</param>
        /// <param name="position">Должность</param>
        void AddPosition(int numberOrder, IPosition position);
        
        /// <summary>
        /// Изменение должности (перевод)
        /// </summary>
        /// <param name="numberOrder">Номер приказа</param>
        /// <param name="oldPosition">Старая должность</param>
        /// <param name="newPosition">Новая должность</param>
        void ChangePosition(int numberOrder, IPosition oldPosition, IPosition newPosition);
        
        /// <summary>
        /// Увольнение с должности
        /// </summary>
        /// <param name="numberOrder">Номер приказа</param>
        /// <param name="oldPosition">Старая должность</param>
        void Dismissal(int numberOrder, IPosition oldPosition);

        /// <summary>
        /// Изменение ФИО
        /// </summary>
        /// <param name="newFullName">Новое ФИО</param>
        void ChangeFullName(string newFullName);

        /// <summary>
        /// Получить список должностей сотрудника на которых он находится
        /// </summary>
        /// <returns>Список должностей</returns>
        IReadOnlyList<IPosition> GetListCurrentPositions();
     
    }
}
