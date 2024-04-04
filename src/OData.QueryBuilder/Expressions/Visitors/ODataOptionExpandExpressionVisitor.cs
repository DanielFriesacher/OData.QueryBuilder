using OData.QueryBuilder.Options;

namespace OData.QueryBuilder.Expressions.Visitors
{
    internal class ODataOptionExpandExpressionVisitor : ODataOptionExpressionVisitor
    {
        public ODataOptionExpandExpressionVisitor(ODataQueryBuilderOptions odataQueryBuilderOptions)
            : base(odataQueryBuilderOptions)
        {
        }
    }
}
