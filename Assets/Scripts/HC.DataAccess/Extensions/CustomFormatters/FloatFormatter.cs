namespace HC.DataAccess.Extensions.CustomFormatters
{
    public class FloatFormatter : BaseTypeFormatter<float>
    {
        public override string WriteFormat(float value)
        {
            return value.ToString().Replace(",", ".");
        }

        public override float ReadFormat(object value)
        {
            return (float)value;
        }
    }
}