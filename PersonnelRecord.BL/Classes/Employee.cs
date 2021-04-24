using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonnelRecord.BL.Classes
{
    /// <summary>
    /// Класс Простой Сотрудник
    /// </summary>
    public class Employee
    {
        #region Поля
        /// <summary>
        ///  ID Сотрудника или таб. номер
        /// </summary>
        private int id;
        /// <summary>
        /// Получить ID сотрудника или таб.номер
        /// </summary>
        /// <returns>ID сотрудника(Таб.номер)</returns>
        public int GetID()
        {
            return id;
        }

        /// <summary>
        /// ФИО
        /// </summary>
        private string fullName;
        /// <summary>
        /// Получить ФИО
        /// </summary>
        /// <returns>Строка с ФИО сотрудника</returns>
        public string GetFullName()
        {
            return fullName;
        }

        /// <summary>
        /// Дата рождения
        /// </summary>
        private DateTime birthday;
        /// <summary>
        /// Получить Дату рождения
        /// </summary>
        /// <returns>Дата рождения</returns>
        public DateTime GetBirthday()
        {
            return birthday;
        }

        /// <summary>
        /// Список динамики сотрудника
        /// </summary>
        private List<Change> changes;
        /// <summary>
        /// Получить неизменяемый список динамики сотрудника
        /// </summary>
        /// <returns>Список динамики</returns>
        public IReadOnlyList<Change> GetChanges()
        {
            return changes.AsReadOnly();
        }

        #endregion

        public Employee(int id, string fullName, DateTime birthday)
        {
            this.id = id;
            this.fullName = fullName;
            this.birthday = birthday;
            changes = new List<Change>();
        }

        /// <summary>
        /// Принятие на работу. 
        /// Основная должность.
        /// </summary>
        /// <param name="numberOrder">Номер приказа</param>
        /// <param name="position">Должность</param>
        public void Recruitment(int numberOrder, Position position)
        {
            var change = Change.Recruitment(numberOrder, this, position, false);
            changes.Add(change);
        }

        /// <summary>
        /// Добавить должность на совмещение. 
        /// Должность по совмещению.
        /// </summary>
        /// <param name="numberOrder">Номер приказа</param>
        /// <param name="position">Должность</param>
        public void AddPosition(int numberOrder, Position position)
        {
            var change = Change.Recruitment(numberOrder, this, position, true);
            changes.Add(change);
        }

        /// <summary>
        /// Изменение ФИО
        /// </summary>
        /// <param name="newFullName">Новое ФИО</param>
        public void ChangeFullName(string newFullName)
        {
            fullName = newFullName;
        }

        /// <summary>
        /// Изменение должности (перевод)
        /// </summary>
        /// <param name="numberOrder">Номер приказа</param>
        /// <param name="oldPosition">Старая должность</param>
        /// <param name="newPosition">Новая должность</param>
        public void ChangePosition(int numberOrder, Position oldPosition, Position newPosition)
        {
            var oldChange = changes.Where(x => x.GetStatus()).First(x => x.GetPosition() == oldPosition);
            var change = Change.Transfer(numberOrder, this, oldChange, newPosition);
            changes.Add(change);
        }

        /// <summary>
        /// Увольнение с должности
        /// </summary>
        /// <param name="numberOrder">Номер приказа</param>
        /// <param name="oldPosition">Старая должность</param>
        public void Dismissal(int numberOrder, Position oldPosition)
        {
            var oldChange = changes.Where(x => x.GetStatus()).First(x => x.GetPosition() == oldPosition);
            var change = Change.Dismissal(numberOrder, this, oldChange);
            changes.Add(change);
        }


        /// <summary>
        /// Получить список должностей сотрудника на которых он находится
        /// </summary>
        /// <returns>Список должностей</returns>
        public IReadOnlyList<Position> GetListCurrentPositions()
        {
            return changes.Where(x => x.GetStatus()).Select(x => x.GetPosition()).ToList().AsReadOnly();
        }


    }
}
