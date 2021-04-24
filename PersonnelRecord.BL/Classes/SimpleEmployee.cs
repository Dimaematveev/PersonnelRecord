using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonnelRecord.BL.Interfaces;

namespace PersonnelRecord.BL.Classes
{
    /// <summary>
    /// Класс Простой Сотрудник
    /// </summary>
    public class SimpleEmployee : IEmployee
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
        private List<IChange> changes;
        /// <summary>
        /// Получить неизменяемый список динамики сотрудника
        /// </summary>
        /// <returns>Список динамики</returns>
        public IReadOnlyList<IChange> GetChanges()
        {
            return changes.AsReadOnly();
        }

        #endregion

        public SimpleEmployee(int id, string fullName, DateTime birthday)
        {
            this.id = id;
            this.fullName = fullName;
            this.birthday = birthday;
            changes = new List<IChange>();
        }

        /// <summary>
        /// Принятие на работу. 
        /// Основная должность.
        /// </summary>
        /// <param name="numberOrder">Номер приказа</param>
        /// <param name="position">Должность</param>
        public void Recruitment(int numberOrder, IPosition position)
        {
            var change = SimpleChange.Recruitment(numberOrder, this, position, false);
            changes.Add(change);
        }

        /// <summary>
        /// Добавить должность на совмещение. 
        /// Должность по совмещению.
        /// </summary>
        /// <param name="numberOrder">Номер приказа</param>
        /// <param name="position">Должность</param>
        public void AddPosition(int numberOrder, IPosition position)
        {
            var change = SimpleChange.Recruitment(numberOrder, this, position, true);
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
        public void ChangePosition(int numberOrder, IPosition oldPosition, IPosition newPosition)
        {
            var oldChange = changes.Where(x => x.GetStatus()).First(x => x.GetPosition() == oldPosition);
            var change = SimpleChange.Transfer(numberOrder, this, oldChange, newPosition);
            changes.Add(change);
        }

        /// <summary>
        /// Увольнение с должности
        /// </summary>
        /// <param name="numberOrder">Номер приказа</param>
        /// <param name="oldPosition">Старая должность</param>
        public void Dismissal(int numberOrder, IPosition oldPosition)
        {
            var oldChange = changes.Where(x => x.GetStatus()).First(x => x.GetPosition() == oldPosition);
            var change = SimpleChange.Dismissal(numberOrder, this, oldChange);
            changes.Add(change);
        }


        /// <summary>
        /// Получить список должностей сотрудника на которых он находится
        /// </summary>
        /// <returns>Список должностей</returns>
        public IReadOnlyList<IPosition> GetListCurrentPositions()
        {
            return changes.Where(x => x.GetStatus()).Select(x => x.GetPosition()).ToList().AsReadOnly();
        }

        
    }
}
