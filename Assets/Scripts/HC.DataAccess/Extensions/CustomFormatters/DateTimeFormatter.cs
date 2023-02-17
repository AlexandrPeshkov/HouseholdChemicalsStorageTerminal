using System;

namespace DataAccess.Extensions.CustomFormatters
{
    public class DateTimeFormatter : BaseTypeFormatter<DateTime>
    {
        public override string WriteFormat(DateTime value)
        {
            return $"'{value}'";
        }

        public override DateTime ReadFormat(object value)
        {
            if (DateTime.TryParse(value.ToString(), out var dateTime))
            {
                return dateTime;
            }

            throw new FormatException($"{value} не может быть преобразован в DateTime");
        }
    }
}