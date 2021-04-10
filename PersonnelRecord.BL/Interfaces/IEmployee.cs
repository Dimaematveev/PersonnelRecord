using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelRecord.BL.Interfaces
{
    /// <summary>
    /// Интерфейс Сотрудник
    /// </summary>
    interface IEmployee
    {
        string FIO { get; }

        void AddWork(int prikaz);
    }
}
