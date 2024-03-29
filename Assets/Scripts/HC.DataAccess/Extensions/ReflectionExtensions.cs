using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DataAccess.DataAccess.Extensions
{
    public static class ReflectionExtensions
    {
        public static IReadOnlyDictionary<string, PropertyInfo> AsPropertiesDictionary(object instance, params string[] excludeProps)
        {
            return instance.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty | BindingFlags.GetProperty)
                .Where(x => !excludeProps.Contains(x.Name))
                .ToDictionary(x => x.Name, v => v);
        }

        public static bool IsNumericType(this Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }
    }
}