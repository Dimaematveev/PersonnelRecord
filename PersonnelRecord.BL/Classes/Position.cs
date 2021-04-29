using System;

namespace PersonnelRecord.BL.Classes
{
    /// <summary>
    /// Класс простая Должность
    /// </summary>
    public class Position
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
        private Unit unit;
        /// <summary>
        /// Получить подразделение к которому относится должность
        /// </summary>
        /// <returns>Подразделение</returns>
        public Unit GetUnit()
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

        internal Position(string namePosition, Unit unit)
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

        /// <summary>
        /// Удалить должность
        /// </summary>
        /// <returns>True - Удалил должность, False - нет</returns>
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


        /// <summary>
        /// Снять с должности
        /// </summary>
        /// <returns>True - Сняли с должности, False - нет</returns>
        public bool RemoveFromPosition()
        {
            if (!isPositionBusy)
            {
                return false;
            }
            isPositionBusy = false;
            return true;
        }

        
    }
}
