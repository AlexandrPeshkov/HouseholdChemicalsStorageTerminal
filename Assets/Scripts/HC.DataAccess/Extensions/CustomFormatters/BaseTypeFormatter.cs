namespace HC.DataAccess.Extensions.CustomFormatters
{
    public abstract class BaseTypeFormatter<T> : ICustomTypeFormatter
    {
        public abstract string WriteFormat(T value);

        public abstract T ReadFormat(object value);

        string ICustomTypeFormatter.ForWriting(object value)
        {
            return WriteFormat((T)value);
        }

        object ICustomTypeFormatter.ForReading(object value)
        {
            return ReadFormat(value);
        }
    }

    public interface ICustomTypeFormatter
    {
        string ForWriting(object value);

        object ForReading(object value);
    }
}