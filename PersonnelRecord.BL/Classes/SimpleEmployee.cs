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


        public void AddPosition(int numberOrder, IPosition position)
        {
            throw new NotImplementedException();
        }

        public void ChangeFullName(string newFullName)
        {
            throw new NotImplementedException();
        }

        public void ChangePosition(int numberOrder, IPosition oldPosition, IPosition newPosition)
        {
            throw new NotImplementedException();
        }

        public void Dismissal(int numberOrder, IPosition oldPosition)
        {
            throw new NotImplementedException();
        }

        
        public IReadOnlyList<IPosition> GetListCurrentPositions()
        {
            throw new NotImplementedException();
        }

        public void Recruitment(int numberOrder, IPosition position)
        {
            throw new NotImplementedException();
        }
    }
}
