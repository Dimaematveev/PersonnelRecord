using PersonnelRecord.BL.Enums;
using System;
using System.Linq;

namespace PersonnelRecord.BL.Classes
{
    /// <summary>
    /// Класс Динамика
    /// </summary>
    public class Change
    {

        #region Поля
        /// <summary>
        /// предыдущую динамику
        /// </summary>
        private readonly Change prevpreviousChange;
        /// <summary>
        /// Получить предыдущее изменение должности (предыдущую динамику)
        /// </summary>
        /// <returns>Предыдущая динамика</returns>
        public Change GetPreviousChange()
        {
            return prevpreviousChange;
        }

        /// <summary>
        /// Номер приказа
        /// </summary>
        private readonly int numberOrder;
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
        private readonly DateTime dateOfChange;
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
        private readonly Position position;
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
        private readonly Employee employee;
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
        private readonly bool combinationOfPosition;
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
        private bool status;
        /// <summary>
        /// Получить статус сотрудника. 
        /// Занимает эту должность или нет
        /// </summary>
        /// <returns>True - Занимает должность, false - нет</returns>
        public bool GetStatus()
        {
            return status;
        }

        /// <summary>
        /// Тип изменения
        /// </summary>
        private readonly RecordType recordType;
        /// <summary>
        /// Получить тип изменения
        /// </summary>
        /// <returns>Тип записи</returns>
        public RecordType GetRecordType()
        {
            return recordType;
        }
        #endregion




        private Change(Change prevpreviousChange,
                             int numberOrder,
                             Employee employee,
                             Position position,
                             bool combinationOfPosition,
                             bool status,
                             RecordType recordType)
        {

            this.prevpreviousChange = prevpreviousChange;
            this.numberOrder = numberOrder;
            this.employee = employee;
            this.position = position;
            this.combinationOfPosition = combinationOfPosition;
            this.status = status;
            this.recordType = recordType;
            this.dateOfChange = DateTime.Now;

            if (prevpreviousChange != null)
            {
                prevpreviousChange.ChangeStatusFalse();
            }
        }

        /// <summary>
        /// Проверить возможно ли изменить статус на false
        /// </summary>
        /// <returns>true - Возможно изменить, false- нет</returns>
        public bool IsPossibleChangeStatusToFalse()
        {
            if (status == false)
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
            status = false;
            return true;
        }


        #region Статические методы создания класса
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
            if (numberOrder <= 0)
            {
                throw new ArgumentException("Номер приказа должен быть положительным числом!", nameof(numberOrder));
            }
            if (position == null)
            {
                throw new ArgumentNullException(nameof(position), "Должность не может быть пустой");
            }
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee), "Сотрудник не может быть пустым");
            }

            if (position.GetIsPositionBusy())
            {
                throw new ArgumentNullException(nameof(position), "Должность не может быть занята!");
            }

           
            var change = new Change(null,
                                    numberOrder,
                                    employee,
                                    position,
                                    combinationOfPosition,
                                    true,
                                    RecordType.Найм);
            return change;
        }

        //TODO:Надо наверное проверить что в старой динамике не null должность
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
            if (numberOrder <= 0)
            {
                throw new ArgumentException("Номер приказа должен быть положительным числом!", nameof(numberOrder));
            }

            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee), "Сотрудник не может быть пустой");
            }
            if (prevpreviousChange == null)
            {
                throw new ArgumentNullException(nameof(prevpreviousChange), "Старая динамика не может быть пустой");
            }
            if (position == null)
            {
                throw new ArgumentNullException(nameof(position), "Новая должность не может быть пустой");
            }

            if (prevpreviousChange.GetEmployee() != employee)
            {
                throw new ArgumentException("Предыдущая динамика не от этого сотрудника!", nameof(prevpreviousChange));
            }
            if (prevpreviousChange.GetPosition() == position)
            {
                throw new ArgumentException("Ставим сотрудника на туже должность!", nameof(prevpreviousChange));
            }

            if (position.GetIsPositionBusy())
            {
                throw new ArgumentException(nameof(position), "Новая Должность не может быть занята!");
            }

            var change = new Change(prevpreviousChange,
                                    numberOrder,
                                    employee,
                                    position,
                                    prevpreviousChange.GetIsCombination(),
                                    true,
                                    RecordType.Изменение);

            return change;
        }

        //TODO:Надо наверное проверить что в старой динамике не null должность
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
            if (numberOrder <= 0)
            {
                throw new ArgumentException("Номер приказа должен быть положительным числом!", nameof(numberOrder));
            }

            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee), "Сотрудник не может быть пустой");
            }
            if (prevpreviousChange == null)
            {
                throw new ArgumentNullException(nameof(prevpreviousChange), "Старая динамика не может быть пустой");
            }

            if (prevpreviousChange.GetEmployee() != employee)
            {
                throw new ArgumentException("Предыдущая динамика не от этого сотрудника!", nameof(prevpreviousChange));
            }
          

            var change = new Change(prevpreviousChange,
                                    numberOrder,
                                    employee,
                                    null,
                                    prevpreviousChange.GetIsCombination(),
                                    false,
                                    RecordType.Увольнение);

            return change;
        }
        #endregion
    }
}
