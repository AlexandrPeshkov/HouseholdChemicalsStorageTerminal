using System;

namespace DataAccess.Extensions.CustomFormatters
{
    public class BooleanFormatter : BaseTypeFormatter<bool>
    {
        public override string WriteFormat(bool value)
        {
            return value ? "1" : "0";
        }

        public override bool ReadFormat(object value)
        {
            if (int.TryParse(value.ToString(), out var intValue))
            {
                return intValue == 1;
            }

            throw new FormatException($"{value} не может быть преобразован в bool");

            return false;
        }
    }
}