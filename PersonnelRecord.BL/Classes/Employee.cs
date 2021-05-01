using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonnelRecord.BL.Classes
{
    /// <summary>
    /// Класс Сотрудник
    /// </summary>
    public class Employee
    {
        #region Поля
        /// <summary>
        ///  ID Сотрудника или таб. номер
        /// </summary>
        private int id;
        /// <summary>
        /// Получить ID сотрудника или таб.номер
        /// </summary>
        /// <returns>ID сотрудника(Таб.номер)</returns>
        public int GetID()
        {
            return id;
        }

        /// <summary>
        /// ФИО
        /// </summary>
        private string fullName;
        /// <summary>
        /// Получить ФИО
        /// </summary>
        /// <returns>Строка с ФИО сотрудника</returns>
        public string GetFullName()
        {
            return fullName;
        }

        /// <summary>
        /// Дата рождения
        /// </summary>
        private DateTime birthday;
        /// <summary>
        /// Получить Дату рождения
        /// </summary>
        /// <returns>Дата рождения</returns>
        public DateTime GetBirthday()
        {
            return birthday;
        }

        /// <summary>
        /// Список динамики сотрудника
        /// </summary>
        private List<Change> changes;
        /// <summary>
        /// Получить неизменяемый список динамики сотрудника
        /// </summary>
        /// <returns>Список динамики</returns>
        public IReadOnlyList<Change> GetChanges()
        {
            return changes.AsReadOnly();
        }
        #endregion

        /// <summary>
        /// Получить список должностей сотрудника на которых он находится
        /// </summary>
        /// <returns>Список должностей</returns>
        public IReadOnlyList<Position> GetListCurrentPositions()
        {
            return changes.Where(x => x.GetStatus()).Select(x => x.GetPosition()).ToList().AsReadOnly();
        }



        public Employee(int id, string fullName, DateTime birthday)
        {
            if (string.IsNullOrWhiteSpace(fullName))
            {
                throw new ArgumentNullException(nameof(fullName), "Имя человека не может быть пустым!!");
            }
            if (birthday.Date > DateTime.Today.AddYears(-18))
            {
                throw new ArgumentException("Возраст человека должен быть больше 18 лет!!", nameof(birthday));
            }
            if (birthday.Date <= DateTime.Today.AddYears(-100))
            {
                throw new ArgumentException("Возраст человека должен быть меньше 100 лет!!", nameof(birthday));
            }
            this.id = id;
            this.fullName = fullName;
            this.birthday = birthday.Date;
            changes = new List<Change>();
        }

        /// <summary>
        /// Принятие на работу. 
        /// Основная должность.
        /// </summary>
        /// <param name="numberOrder">Номер приказа</param>
        /// <param name="position">Должность</param>
        /// <returns>Новая динамика</returns>
        public Change Recruitment(int numberOrder, Position position)
        {
           
            if (numberOrder <= 0) 
            {
                throw new ArgumentException("Номер приказа должен быть положительным числом!", nameof(numberOrder));
            }
            if (position == null)
            {
                throw new ArgumentNullException(nameof(position), "Должность не может быть пустой");
            }
            if (GetChanges().Where(x => x.GetStatus() && !x.GetIsCombination()).Count() == 1)
            {
                throw new ArgumentException("Нельзя добавить основную должность если уже есть основная!", nameof(position));
            }
            if (position.GetIsPositionBusy())
            {
                throw new ArgumentNullException(nameof(position), "Должность не может быть занята!");
            }
            var change = Change.Recruitment(numberOrder, this, position, false);
            changes.Add(change);
            return change;
        }

        /// <summary>
        /// Добавить должность на совмещение. 
        /// Должность по совмещению.
        /// </summary>
        /// <param name="numberOrder">Номер приказа</param>
        /// <param name="position">Должность</param>
        /// <returns>Новая динамика</returns>
        public Change AddPosition(int numberOrder, Position position)
        {
            
            if (numberOrder <= 0)
            {
                throw new ArgumentException("Номер приказа должен быть положительным числом!", nameof(numberOrder));
            }
            if (position == null)
            {
                throw new ArgumentNullException(nameof(position), "Должность не может быть пустой");
            }

            if (GetChanges().Where(x => x.GetStatus() && !x.GetIsCombination()).Count() == 0)
            {
                throw new ArgumentException("Нельзя добавить доп должность если нет основной!", nameof(position));
            }
            if (position.GetIsPositionBusy())
            {
                throw new ArgumentNullException(nameof(position), "Должность не может быть занята!");
            }

            var change = Change.Recruitment(numberOrder, this, position, true);
            changes.Add(change);
            return change;
        }



        /// <summary>
        /// Изменение должности (перевод)
        /// </summary>
        /// <param name="numberOrder">Номер приказа</param>
        /// <param name="oldPosition">Старая должность</param>
        /// <param name="newPosition">Новая должность</param>
        /// <returns>Новая динамика</returns>
        public Change ChangePosition(int numberOrder, Position oldPosition, Position newPosition)
        {
            if (numberOrder <= 0)
            {
                throw new ArgumentException("Номер приказа должен быть положительным числом!", nameof(numberOrder));
            }
            if (newPosition == null)
            {
                throw new ArgumentNullException(nameof(newPosition), "Новая Должность не может быть пустой");
            }
            if (oldPosition == null)
            {
                throw new ArgumentNullException(nameof(oldPosition), "Старая Должность не может быть пустой");
            }

            if (oldPosition == newPosition)
            {
                throw new ArgumentException("Ставим сотрудника на туже должность!", nameof(newPosition));
            }

            if (newPosition.GetIsPositionBusy())
            {
                throw new ArgumentException( "Новая Должность не может быть занята!", nameof(newPosition));
            }

            var OldChange = changes.Where(x => x.GetStatus()).FirstOrDefault(x => x.GetPosition() == oldPosition);
            if (OldChange == null)
            {
                throw new ArgumentException("Такой должности нет у этого сотрудника!", nameof(oldPosition));
            }
            
            var change = Change.Transfer(numberOrder, this, OldChange, newPosition);
            changes.Add(change);
            return change;
        }

        //TODO: не знаю как сделать проверку на то что удаляем главную должность)
        /// <summary>
        /// Увольнение с должности
        /// </summary>
        /// <param name="numberOrder">Номер приказа</param>
        /// <param name="oldPosition">Старая должность</param>
        /// <returns>Новая динамика</returns>
        public Change Dismissal(int numberOrder, Position oldPosition)
        {
            if (numberOrder <= 0)
            {
                throw new ArgumentException("Номер приказа должен быть положительным числом!", nameof(numberOrder));
            }
            if (oldPosition == null)
            {
                throw new ArgumentNullException(nameof(oldPosition), "Должность не может быть пустой");
            }
            var OldChange = changes.Where(x => x.GetStatus()).FirstOrDefault(x => x.GetPosition() == oldPosition);
            if (OldChange == null)
            {
                throw new ArgumentException("Такой должности нет у этого сотрудника!", nameof(oldPosition));
            }


           
            var change = Change.Dismissal(numberOrder, this, OldChange);
            changes.Add(change);
            return change;
        }


        //TODO:Только буквы???
        /// <summary>
        /// Изменение ФИО
        /// </summary>
        /// <param name="newFullName">Новое ФИО</param>
        /// <returns>True - изменили, false - нет</returns>
        public bool ChangeFullName(string newFullName)
        {
            if (newFullName == fullName)
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(newFullName))
            {
                return false;
            }

            fullName = newFullName;

            return true;
        }


        


    }
}
