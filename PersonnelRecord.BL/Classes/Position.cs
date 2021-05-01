using System;

namespace PersonnelRecord.BL.Classes
{
    /// <summary>
    /// Класс Должность
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
        /// <returns>
        /// <para><c>True</c> - Занята</para>
        /// <para><c>False</c> - нет</para>
        /// </returns>
        public bool GetIsPositionBusy()
        {
            return isPositionBusy;
        }

        /// <summary>
        /// Удалена ли должность
        /// </summary>
        private bool isDelete;

        //TODO:не проверял на удаленную должность(
        /// <summary>
        /// Удалена ли должность
        /// </summary>
        /// <returns>
        /// <para><c>True</c> - Удалена</para>
        /// <para><c>False</c> - нет</para>
        /// </returns>
        public bool GetIsDelete()
        {
            return isDelete;
        }
        #endregion

        //TODO:Возможно надо еще конструктор
        /// <summary>
        /// Конструктор Должности можно вызвать только из проекта)
        /// </summary>
        /// <param name="namePosition">Название должности</param>
        /// <param name="unit">Подразделение</param>
        internal Position(string namePosition, Unit unit)
        {
            if (string.IsNullOrWhiteSpace(namePosition))
            {
                throw new ArgumentNullException("Название должности не должно быть пустым!!!");
            }
            //TODO:Как протестить хз
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
        /// <returns>
        /// <para><c>True</c> - возможно удалить должность</para>
        /// <para><c>False</c> - нет</para>
        /// </returns>
        public bool IsPossibleDeletePosition()
        {
            if (GetIsDelete())
            {
                return false;
            }

            if (isPositionBusy)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Удалить должность
        /// </summary>
        /// <returns>
        /// <para><c>True</c> - Удалил должность</para>
        /// <para><c>False</c> - нет</para>
        /// </returns>
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
        /// <returns>
        /// <para><c>True</c> - занял должность</para>
        /// <para><c>False</c> - нет</para>
        /// </returns>
        public bool BusyPosition()
        {
            if (isDelete)
            {
                return false;
            }
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
        /// <returns>
        /// <para><c>True</c> -  Сняли с должности</para>
        /// <para><c>False</c> - нет</para>
        /// </returns>
        public bool RemoveFromPosition()
        {
            if (isDelete)
            {
                return false;
            }
            if (!isPositionBusy)
            {
                return false;
            }
            isPositionBusy = false;
            return true;
        }

        
    }
}
