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
        /// <summary>
        /// Получить название должности
        /// </summary>
        /// <returns>Название должности</returns>
        public string GetName()
        {
            return name;
        }
        
        /// <summary>
        /// Подразделение к которому относится должность
        /// </summary>
        private IUnit unit;
        /// <summary>
        /// Получить подразделение к которому относится должность
        /// </summary>
        /// <returns>Подразделение</returns>
        public IUnit GetUnit()
        {
            return unit;
        }

        /// <summary>
        /// Занята ли должность
        /// </summary>
        private bool isPositionBusy;
        /// <summary>
        /// Занята ли должность
        /// </summary>
        /// <returns> True - Занята, False - нет</returns>
        public bool GetIsPositionBusy()
        {
            return isPositionBusy;
        }
        
        /// <summary>
        /// Удалена ли должность
        /// </summary>
        private bool isDelete;
        
        /// <summary>
        /// Удалена ли должность
        /// </summary>
        /// <returns> True - Удалена, False - нет</returns>
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



        /// <summary>
        /// Удалить должность
        /// </summary>
        /// <returns>True -Удалил должность, False - нет</returns>
        public bool Delete()
        {
            if (!IsPossibleDeletePosition())
            {
                return false;
            }

            isDelete = true;
            return true;
        }

        /// <summary>
        /// Занять должность
        /// </summary>
        /// <returns>True -занял должность, False - нет</returns>
        public bool BusyPosition()
        {
            if (isPositionBusy)
            {
                return false;
            }
            isPositionBusy = true;
            return true;
        }

        //TODO: Переименовать
        /// <summary>
        /// Снять с должности
        /// </summary>
        /// <returns>True - Сняли с должности, False - нет</returns>
        public bool NotBusyPosition()
        {
            if (!isPositionBusy)
            {
                return false;
            }
            isPositionBusy = false;
            return true;
        }

        /// <summary>
        /// Проверка, можно ли удалить должность
        /// </summary>
        /// <returns>True -возможно удалить должность, False - нет</returns>
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
