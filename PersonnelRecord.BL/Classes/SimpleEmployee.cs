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
        public int GetID()
        {
            return id;
        }
       
        /// <summary>
        /// ФИО
        /// </summary>
        private string fullName;
        public string GetFullName()
        {
            return fullName;
        }
       
        /// <summary>
        /// Дата рождения
        /// </summary>
        private DateTime birthday;
        public DateTime GetBirthday()
        {
            return birthday;
        }
        
        /// <summary>
        /// Список динамики сотрудника
        /// </summary>
        private List<IChange> changes;

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

        public void Recruitment(int numberOrder, IPosition position)
        {
            var change = SimpleChange.Recruitment(numberOrder, this, position, false);
            changes.Add(change);
        }

        public void AddPosition(int numberOrder, IPosition position)
        {
            var change = SimpleChange.Recruitment(numberOrder, this, position, true);
            changes.Add(change);
        }

        public void ChangeFullName(string newFullName)
        {
            fullName = newFullName;
        }

        public void ChangePosition(int numberOrder, IPosition oldPosition, IPosition newPosition)
        {
            var oldChange = changes.Where(x => x.GetStatus()).First(x => x.GetPosition() == oldPosition);
            var change = SimpleChange.Transfer(numberOrder, this, oldChange, newPosition);
            changes.Add(change);
        }

        public void Dismissal(int numberOrder, IPosition oldPosition)
        {
            var oldChange = changes.Where(x => x.GetStatus()).First(x => x.GetPosition() == oldPosition);
            var change = SimpleChange.Dismissal(numberOrder, this, oldChange);
            changes.Add(change);
        }

        
        public IReadOnlyList<IPosition> GetListCurrentPositions()
        {
            return changes.Where(x => x.GetStatus()).Select(x => x.GetPosition()).ToList().AsReadOnly();
        }

        
    }
}
