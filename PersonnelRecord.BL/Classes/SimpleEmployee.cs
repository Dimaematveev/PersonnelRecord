using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonnelRecord.BL.Interfaces;

namespace PersonnelRecord.BL.Classes
{
    /// <summary>
    /// Класс  Простой Сотрудник
    /// </summary>
    public class SimpleEmployee : IEmployee
    {
        public int ID { get; }
        public int FIO { get; }
        public List<IChange> Changes { get; }
        public bool IsWork { get; }

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

        public DateTime GetBirthday()
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<IChange> GetChanges()
        {
            throw new NotImplementedException();
        }

        public string GetFullName()
        {
            throw new NotImplementedException();
        }

        public int GetID()
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
