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
        /// Предыдущая динамика
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
        /// Получить номер приказа
        /// </summary>
        /// <returns>Номер приказа</returns>
        public int GetNumberOrder()
        {
            return numberOrder;
        }

        /// <summary>
        /// Дата внесения данных
        /// </summary>
        private readonly DateTime dateOfChange;
        /// <summary>
        /// Получить дату внесения данных
        /// </summary>
        /// <returns>Дата внесения изменения</returns>
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

        /// <summary>
        /// Совмещает должность?
        /// </summary>
        private readonly bool combinationOfPosition;
        /// <summary>
        /// Совмещает должность?
        /// </summary>
        /// <returns>
        /// <para><c>True</c> - Совмещает</para>
        /// <para><c>False</c> - нет</para>
        /// </returns>
        public bool GetIsCombination()
        {
            return combinationOfPosition;
        }

        /// <summary>
        /// Статус сотрудника. 
        /// </summary>
        /// <remarks>
        /// Занимает эту должность или нет
        /// </remarks>
        private bool status;
        /// <summary>
        /// Получить статус сотрудника. 
        /// </summary>
        /// <returns>
        /// <para><c>True</c> - Занимает должность</para>
        /// <para><c>False</c> - нет</para>
        /// </returns>
        public bool GetStatus()
        {
            return status;
        }

        /// <summary>
        /// Тип записи
        /// </summary>
        private readonly RecordType recordType;
        /// <summary>
        /// Получить тип изменения
        /// </summary>
        /// <returns>Тип записи</returns>
        /// <remarks>
        /// Смотреть <see cref="RecordType"/>.
        /// </remarks>
        public RecordType GetRecordType()
        {
            return recordType;
        }
        #endregion



        /// <summary>
        /// Приватный конструктор Динамики
        /// </summary>
        /// <param name="prevpreviousChange">Предыдущая динамика</param>
        /// <param name="numberOrder">Номер приказа</param>
        /// <param name="employee">Сотрудник</param>
        /// <param name="position">Должность</param>
        /// <param name="combinationOfPosition">Совмещенная должность?</param>
        /// <param name="status">Статус сотрудника</param>
        /// <param name="recordType">Тип записи</param>
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
        /// <returns>
        /// <para><c>True</c> - Возможно изменить</para>
        /// <para><c>False</c> - нет</para>
        /// </returns>
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
        /// <returns>
        /// <para><c>True</c> -  удалили флаг работающего человека,</para>
        /// <para><c>False</c> - нет получилось</para>
        /// </returns>
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
