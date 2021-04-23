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
        private static int maxID;

        #region Поля
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
        #endregion

        private SimpleChange(IChange prevpreviousChange,
                             int numberOrder,
                             IEmployee employee,
                             IPosition position,
                             bool combinationOfPosition,
                             bool isWork,
                             RecordType status)
        {
            
            this.prevpreviousChange = prevpreviousChange;
            this.numberOrder = numberOrder;
            this.employee = employee;
            this.position = position;
            this.combinationOfPosition = combinationOfPosition;
            this.isWork = isWork;
            this.status = status;

            if (prevpreviousChange != null)
            {
                prevpreviousChange.ChangeStatusFalse();
            }
            maxID++;
            this.id = maxID;

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

        /// <summary>
        /// Нанять на должность
        /// </summary>
        /// <param name="numberOrder">Номер приказа</param>
        /// <param name="employee">Сотрудник</param>
        /// <param name="position">Должность</param>
        /// <param name="combinationOfPosition">Совмещенная должность?</param>
        /// <returns>Новая динамика</returns>
        public static IChange Recruitment(int numberOrder,
                                          IEmployee employee,
                                          IPosition position,
                                          bool combinationOfPosition)
        {
            var change = new SimpleChange(null,
                                          numberOrder,
                                          employee,
                                          position,
                                          combinationOfPosition,
                                          true,
                                          RecordType.Найм);
            return change;
        }

        /// <summary>
        /// Изменить должность
        /// </summary>
        /// <param name="numberOrder">Номер приказа</param>
        /// <param name="employee">Сотрудник</param>
        /// <param name="prevpreviousChange">Предыдущая динамика</param>
        /// <param name="position">Должность</param>
        /// <returns>Новая динамика</returns>
        public static IChange Transfer(int numberOrder,
                                          IEmployee employee,
                                          IChange prevpreviousChange,
                                          IPosition position)
        {
            var change = new SimpleChange(prevpreviousChange,
                                          numberOrder,
                                          employee,
                                          position,
                                          prevpreviousChange.GetIsCombination(),
                                          true,
                                          RecordType.Изменение);
            
            return change;
        }
        /// <summary>
        /// Уволить с должности
        /// </summary>
        /// <param name="numberOrder">Номер приказа</param>
        /// <param name="employee">Сотрудник</param>
        /// <param name="prevpreviousChange">Предыдущая динамика</param>
        /// <returns>Новая динамика</returns>
        public static IChange Dismissal(int numberOrder,
                                          IEmployee employee,
                                          IChange prevpreviousChange)
        {
            var change = new SimpleChange(prevpreviousChange,
                                          numberOrder,
                                          employee,
                                          null,
                                          prevpreviousChange.GetIsCombination(),
                                          false,
                                          RecordType.Увольнение);

            return change;
        }
    }
}
