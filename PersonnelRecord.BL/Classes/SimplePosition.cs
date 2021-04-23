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
        #region Поля
        /// <summary>
        /// Название должности
        /// </summary>
        private string name;
        public string GetName()
        {
            return name;
        }
        
        /// <summary>
        /// Подразделение к которому относится должность
        /// </summary>
        private IUnit unit;
        public IUnit GetUnit()
        {
            return unit;
        }

        /// <summary>
        /// Занята ли должность
        /// </summary>
        private bool isPositionBusy;
        public bool GetIsPositionBusy()
        {
            return isPositionBusy;
        }
        
        /// <summary>
        /// Удалена ли должность
        /// </summary>
        private bool isDelete;
        public bool GetIsDelete()
        {
            return isDelete;
        }
        #endregion

        //internal SimplePosition(string namePosition)
        //{
        //   name = namePosition;
        //}
        ////Setsubbdiv;

        internal SimplePosition(string namePosition, IUnit unit)
        {
            if (string.IsNullOrWhiteSpace(namePosition))
            {
                throw new ArgumentNullException("Название должности не должно быть пустым!!!");
            }
            if (unit == null)
            {
                throw new ArgumentNullException("Подразделение не должно быть null!!!");
            }
            name = namePosition;
            this.unit = unit;


            isPositionBusy = false;
            isDelete = false;

        }

       

        
        public bool Delete()
        {
            if (!IsPossibleDeletePosition())
            {
                return false;
            }

            isDelete = true;
            return true;
        }

        public bool BusyPosition()
        {
            if (isPositionBusy)
            {
                return false;
            }
            isPositionBusy = true;
            return true;
        }

        public bool NotBusyPosition()
        {
            if (!isPositionBusy)
            {
                return false;
            }
            isPositionBusy = false;
            return true;
        }

        public bool IsPossibleDeletePosition()
        {
            if (isPositionBusy)
            {
                return false;
            }

            return true;
        }
    }
}
