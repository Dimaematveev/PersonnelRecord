using PersonnelRecord.BL.Enums;
using System;

namespace PersonnelRecord.BL.Classes
{
    /// <summary>
    /// Класс простая Динамика
    /// </summary>
    public class Change
    {
        private static int maxID = 0;

        #region Поля
        /// <summary>
        /// предыдущую динамику
        /// </summary>
        private Change prevpreviousChange;
        /// <summary>
        /// Получить предыдущее изменение должности (предыдущую динамику)
        /// </summary>
        /// <returns>Предыдущая динамика</returns>
        public Change GetPreviousChange()
        {
            return prevpreviousChange;
        }

        //TODO:Что делать с ID
        /// <summary>
        /// ID Динамики
        /// </summary>
        private int id;
        /// <summary>
        /// Получить ID Динамики
        /// </summary>
        /// <returns>ID Динамики</returns>
        public int GetID()
        {
            return id;
        }

        /// <summary>
        /// Номер приказа
        /// </summary>
        private int numberOrder;
        /// <summary>
        /// Получить Номер приказа
        /// </summary>
        /// <returns>Номер приказа</returns>
        public int GetNumberOrder()
        {
            return numberOrder;
        }

        /// <summary>
        /// Дату внесения данных
        /// </summary>
        private DateTime dateOfChange;
        /// <summary>
        /// Получить дату внесения данных
        /// </summary>
        /// <returns>Дата изменения</returns>
        public DateTime GetDateChange()
        {
            return dateOfChange;
        }

        /// <summary>
        /// Занимаемая должность
        /// </summary>
        private Position position;
        /// <summary>
        /// Получить занимаемую должность
        /// </summary>
        /// <returns>Должность</returns>
        public Position GetPosition()
        {
            return position;
        }

        /// <summary>
        /// Сотрудник
        /// </summary>
        private Employee employee;
        /// <summary>
        /// Получить сотрудника
        /// </summary>
        /// <returns>Сотрудник</returns>
        public Employee GetEmployee()
        {
            return employee;
        }

        // <summary>
        /// Совмещает должность?
        /// </summary>
        private bool combinationOfPosition;
        /// <summary>
        /// Совмещает должность?
        /// </summary>
        /// <returns>True - Совмещает, false - нет</returns>
        public bool GetIsCombination()
        {
            return combinationOfPosition;
        }

        /// <summary>
        /// Статус сотрудника. 
        /// Занимает эту должность или нет
        /// </summary>
        private bool isWork;
        /// <summary>
        /// Получить статус сотрудника. 
        /// Занимает эту должность или нет
        /// </summary>
        /// <returns>True - Занимает должность, false - нет</returns>
        public bool GetStatus()
        {
            return isWork;
        }

        /// <summary>
        /// Тип изменения
        /// </summary>
        private RecordType status;
        /// <summary>
        /// Получить тип изменения
        /// </summary>
        /// <returns>Тип записи</returns>
        public RecordType GetRecordType()
        {
            return status;
        }
        #endregion




        private Change(Change prevpreviousChange,
                             int numberOrder,
                             Employee employee,
                             Position position,
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
            this.dateOfChange = DateTime.Now;

            if (prevpreviousChange != null)
            {
                prevpreviousChange.ChangeStatusFalse();
            }
            maxID++;
            this.id = maxID;

        }

        /// <summary>
        /// Проверить возможно ли изменить статус на false
        /// </summary>
        /// <returns>true - Возможно изменить, false- нет</returns>
        public bool IsPossibleChangeStatusToFalse()
        {
            if (isWork == false)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Изменить статус сотрудника на False
        /// </summary>
        /// <returns>true - удалили флаг работающего человека, false- нет получилось</returns>
        public bool ChangeStatusFalse()
        {
            if (!IsPossibleChangeStatusToFalse())
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
        public static Change Recruitment(int numberOrder,
                                          Employee employee,
                                          Position position,
                                          bool combinationOfPosition)
        {
            var change = new Change(null,
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
        public static Change Transfer(int numberOrder,
                                          Employee employee,
                                          Change prevpreviousChange,
                                          Position position)
        {
            var change = new Change(prevpreviousChange,
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
        public static Change Dismissal(int numberOrder,
                                          Employee employee,
                                          Change prevpreviousChange)
        {
            var change = new Change(prevpreviousChange,
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
