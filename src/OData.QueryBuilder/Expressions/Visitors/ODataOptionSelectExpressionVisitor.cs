using OData.QueryBuilder.Options;

namespace OData.QueryBuilder.Expressions.Visitors
{
    internal class ODataOptionSelectExpressionVisitor: ODataOptionExpressionVisitor
    {
        public ODataOptionSelectExpressionVisitor(ODataQueryBuilderOptions odataQueryBuilderOptions)
            : base(odataQueryBuilderOptions)
        {
        }
    }
}
