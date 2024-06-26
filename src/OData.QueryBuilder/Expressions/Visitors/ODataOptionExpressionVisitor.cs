﻿using OData.QueryBuilder.Conventions.Constants;
using OData.QueryBuilder.Extensions;
using OData.QueryBuilder.Options;
using System.Linq.Expressions;

namespace OData.QueryBuilder.Expressions.Visitors
{
    internal class ODataOptionExpressionVisitor : ODataExpressionVisitor
    {
        public ODataOptionExpressionVisitor(ODataQueryBuilderOptions odataQueryBuilderOptions)
            : base(odataQueryBuilderOptions)
        {
        }

        protected override string VisitUnaryExpression(LambdaExpression topExpression, UnaryExpression unaryExpression)
        {
            var odataOperator = unaryExpression.NodeType.ToODataOperator();
            var whitespace = odataOperator != default ? " " : default;

            return $"{odataOperator}{whitespace}{VisitExpression(topExpression, unaryExpression.Operand)}";
        }

        protected override string VisitNewExpression(LambdaExpression topExpression, NewExpression newExpression)
        {
            var names = new string[newExpression.Members.Count];

            for (var i = 0; i < newExpression.Members.Count; i++)
            {
                names[i] = newExpression.Members[i].Name;
            }

            return string.Join(QuerySeparators.StringComma, names);
        }
    }
}
