using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonnelRecord.BL.Enums;
using PersonnelRecord.BL.Interfaces;

namespace PersonnelRecord.BL.Classes
{
    /// <summary>
    /// Класс простая Динамика
    /// </summary>
    public class SimpleChange : IChange
    {
        public IChange PrevpreviousChange { get; }
        public int ID { get; }
        public int NumberOrder { get; }
        public DateTime DateOfChange { get; }
        public IPosition Position { get; }
        public IEmployee Employee { get; }
        public bool CombinationOfPosition { get; }
        public bool IsWork { get; }
        public sbyte Status { get; }

        public void ChangeStatusFalse()
        {
            throw new NotImplementedException();
        }

        public DateTime GetDateChange()
        {
            throw new NotImplementedException();
        }

        public IEmployee GetEmployee()
        {
            throw new NotImplementedException();
        }

        public int GetID()
        {
            throw new NotImplementedException();
        }

        public int GetNumberOrder()
        {
            throw new NotImplementedException();
        }

        public IPosition GetPosition()
        {
            throw new NotImplementedException();
        }

        public IChange GetPreviousChange()
        {
            throw new NotImplementedException();
        }

        public RecordType GetRecordType()
        {
            throw new NotImplementedException();
        }

        public bool GetStatus()
        {
            throw new NotImplementedException();
        }

        public bool IsCombination()
        {
            throw new NotImplementedException();
        }
    }
}
