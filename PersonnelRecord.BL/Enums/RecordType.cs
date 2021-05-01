namespace PersonnelRecord.BL.Enums
{
    /// <summary>
    /// Тип записи
    /// </summary>
    public enum RecordType : sbyte
    {
        /// <summary>
        /// Указывает что было произведено увольнение с должности
        /// </summary>
        Увольнение = -1,
        /// <summary>
        /// Указывает что был произведен перевод на другую должность
        /// </summary>
        Изменение = 0,
        /// <summary>
        /// Указывает что было произведено принятие на должность
        /// </summary>
        Найм = 1,
    }
}
