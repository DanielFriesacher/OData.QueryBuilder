﻿using OData.QueryBuilder.Constants;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace OData.QueryBuilder.Extensions
{
    internal static class MemberExpressionExtensions
    {
        public static string ToODataQuery(this MemberExpression memberExpression, string queryString)
        {
            var memberExpressionValue = memberExpression.GetValue();

            if (memberExpressionValue != default)
            {
                if (memberExpressionValue is string @string)
                {
                    return $"'{@string}'";
                }

                if (memberExpressionValue is bool @bool)
                {
                    return $"{@bool}".ToLower();
                }

                if (memberExpressionValue is DateTime dateTime)
                {
                    return $"{dateTime:s}Z";
                }

                if (memberExpressionValue is DateTimeOffset dateTimeOffset)
                {
                    return $"{dateTimeOffset:s}Z";
                }

                if (memberExpressionValue is IEnumerable<int> intValues)
                {
                    var intValuesString = string.Join(ODataQuerySeparators.CommaString, intValues);

                    return !string.IsNullOrEmpty(intValuesString) ? intValuesString : default;
                }

                if (memberExpressionValue is IEnumerable<string> stringValues)
                {
                    var stringValuesString = string.Join($"'{ODataQuerySeparators.CommaString}'", stringValues);

                    return !string.IsNullOrEmpty(stringValuesString) ? $"'{stringValuesString}'" : default;
                }

                return $"{memberExpressionValue}";
            }

            var parentMemberExpressionQuery = memberExpression.Expression.ToODataQuery(queryString);

            if (string.IsNullOrEmpty(parentMemberExpressionQuery))
            {
                return memberExpression.Member.Name;
            }

            return memberExpression.Member.DeclaringType.IsNullableType() ?
                parentMemberExpressionQuery
                :
                $"{parentMemberExpressionQuery}/{memberExpression.Member.Name}";
        }

        public static object GetValue(this MemberExpression memberExpression)
        {
            switch (memberExpression.Expression)
            {
                case ConstantExpression ce:
                    return memberExpression.Member.GetValue(ce.Value);
                case MemberExpression me:
                    return memberExpression.Member.GetValue(GetValue(me));
                default:
                    return memberExpression.Member.GetValue();

            }
        }
    }
}
