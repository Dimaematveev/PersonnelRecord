using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonnelRecord.BL.Interfaces;

namespace PersonnelRecord.BL.Classes
{
    /// <summary>
    /// Класс простая Должность
    /// </summary>
    public class SimplePosition : IPosition
    {
        internal SimplePosition(string name)
        {
            Name = name;
        }
        //Setsubbdiv;

        internal SimplePosition(string name, IUnit subdivision)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Название должности не должно быть пустым!!!");
            }
            if (subdivision == null)
            {
                throw new ArgumentNullException("Подразделение не должно быть null!!!");
            }
            Name = name;
            Subdivision = subdivision;


            IsWork = true;
            IsDelete = false;

        }

        public string Name { get; private set; }
        public IUnit Subdivision { get; private set; }
        public bool IsWork { get; private set; }
        public bool IsDelete { get; private set; }


        public void Delete()
        {
            IsDelete = true;
        }

        public bool GetIsDelete()
        {
            throw new NotImplementedException();
        }

        public bool GetIsPositionBusy()
        {
            throw new NotImplementedException();
        }

        public string GetName()
        {
            throw new NotImplementedException();
        }

        public IUnit GetUnit()
        {
            throw new NotImplementedException();
        }
    }
}
