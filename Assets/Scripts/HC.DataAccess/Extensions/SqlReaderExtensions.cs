using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using HC.DataAccess.Extensions.CustomFormatters;
using HC.DataAccess.Interfaces;

namespace HC.DataAccess.HC.DataAccess.Extensions
{
    public static class SqlReaderExtensions
    {
        private static readonly Dictionary<Type, ICustomTypeFormatter> _formatters = new Dictionary<Type, ICustomTypeFormatter>()
        {
            { typeof(bool), new BooleanFormatter() },
            { typeof(string), new StringFormatter() },
            { typeof(DateTime), new DateTimeFormatter() },
            { typeof(float), new FloatFormatter() }
        };

        public static T ReadValue<T>(this DbDataReader reader, string fieldName)
        {
            int fieldIndex;

            try
            {
                fieldIndex = reader.GetOrdinal(fieldName);
            }
            catch
            {
                return default;
            }

            if (reader.IsDBNull(fieldIndex))
            {
                return default;
            }
            else
            {
                var readData = reader.GetValue(fieldIndex);

                if (readData is T typedData)
                {
                    return typedData;
                }
                else
                {
                    try
                    {
                        return (T)Convert.ChangeType(readData, typeof(T));
                    }
                    catch (InvalidCastException)
                    {
                        return default;
                    }
                }
            }
        }

        public static object ReadValue(this DbDataReader reader, string fieldName, Type fieldType)
        {
            int fieldIndex;

            try
            {
                fieldIndex = reader.GetOrdinal(fieldName);
            }
            catch
            {
                return default;
            }

            if (reader.IsDBNull(fieldIndex))
            {
                return default;
            }
            else
            {
                var value = reader.GetValue(fieldIndex);

                if (_formatters.TryGetValue(fieldType, out var formatter))
                {
                    value = formatter.ForReading(value);
                    return value;
                }
                else
                {
                    if (value.GetType() == fieldType)
                    {
                        return value;
                    }
                    else
                    {
                        //TODO: CustomFormatters
                        try
                        {
                            return Convert.ChangeType(value, fieldType);
                        }
                        catch (InvalidCastException)
                        {
                            return default;
                        }
                    }
                }
            }
        }

        public static IReadOnlyDictionary<string, string> ParseToQueryArgs(object entity)
        {
            var properties = ReflectionExtensions.AsPropertiesDictionary(entity, nameof(IDbEntity.Id));

            var queryProperties = new Dictionary<string, string>();

            foreach (var propertyInfo in properties)
            {
                var value = propertyInfo.Value.GetValue(entity);
                string strValue = null;
                var propertyType = propertyInfo.Value.PropertyType;

                if (_formatters.TryGetValue(propertyType, out var formatter))
                {
                    strValue = formatter.ForWriting(value);
                }
                else
                {
                    strValue = value.ToString();
                }

                queryProperties.Add(propertyInfo.Key, strValue);
            }

            return queryProperties;
        }

        public static string ParsePredicate<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class
        {
            string predicateMask = "{0} = {1}";

            var queryTranslator = new QueryTranslator();
            var sql = queryTranslator.Translate(predicate);
            return sql;
        }
    }
}