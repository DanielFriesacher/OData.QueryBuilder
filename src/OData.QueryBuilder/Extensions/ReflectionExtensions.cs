using Newtonsoft.Json;
using OData.QueryBuilder.Options;
using System;
using System.Reflection;

namespace OData.QueryBuilder.Extensions
{
    internal static class ReflectionExtensions
    {
        public static object GetValue(this MemberInfo memberInfo, object obj = default) => memberInfo switch
        {
            FieldInfo fieldInfo => fieldInfo.GetValue(obj),
            PropertyInfo propertyInfo => propertyInfo.GetValue(obj, default),
            _ => default,
        };

        public static string ResolveMemberName(this MemberInfo memberInfo, ODataQueryBuilderOptions options)
        {
            if (options.UseJsonPropertyAttributeNames)
            {
                var jsonPropertyAttribute = memberInfo.GetCustomAttribute<JsonPropertyAttribute>();
                if (jsonPropertyAttribute != null)
                {
                    return jsonPropertyAttribute.PropertyName;
                }
            }

            return memberInfo.Name;
        }

        public static bool IsNullableType(this Type type) =>
            Nullable.GetUnderlyingType(type) != default;
    }
}