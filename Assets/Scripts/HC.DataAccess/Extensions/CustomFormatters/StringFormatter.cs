namespace DataAccess.Extensions.CustomFormatters
{
    public class StringFormatter : BaseTypeFormatter<string>
    {
        public override string WriteFormat(string value)
        {
            return $"'{value}'";
        }

        public override string ReadFormat(object value)
        {
            return value.ToString();
        }
    }
}