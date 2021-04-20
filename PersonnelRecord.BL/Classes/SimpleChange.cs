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
        /// <summary>
        /// предыдущую динамику
        /// </summary>
        private IChange prevpreviousChange;
        public IChange GetPreviousChange()
        {
            return prevpreviousChange;
        }

        /// <summary>
        /// ID Динамики
        /// </summary>
        private int id;
        public int GetID()
        {
            return id;
        }

        /// <summary>
        /// Номер приказа
        /// </summary>
        private int numberOrder;
        public int GetNumberOrder()
        {
            return numberOrder;
        }

        /// <summary>
        /// Дату внесения данных
        /// </summary>
        private DateTime dateOfChange;
        public DateTime GetDateChange()
        {
            return dateOfChange;
        }

        /// <summary>
        /// Занимаемая должность
        /// </summary>
        private IPosition position;
        public IPosition GetPosition()
        {
            return position;
        }

        /// <summary>
        /// Сотрудник
        /// </summary>
        private IEmployee employee;
        public IEmployee GetEmployee()
        {
            return employee;
        }

        // <summary>
        /// Совмещает должность?
        /// </summary>
        private bool combinationOfPosition;
        public bool GetIsCombination()
        {
            return combinationOfPosition;
        }

        /// <summary>
        /// Статус сотрудника. 
        /// Занимает эту должность или нет
        /// </summary>
        private bool isWork;
        public bool GetStatus()
        {
            return isWork;
        }

        /// <summary>
        /// Тип изменения
        /// </summary>
        private RecordType status;
        public RecordType GetRecordType()
        {
            return status;
        }

        public bool ChangeStatusFalse()
        {
            if (isWork == false)
            {
                return false;
            }
            isWork = false;
            return true;
        }
    }
}
