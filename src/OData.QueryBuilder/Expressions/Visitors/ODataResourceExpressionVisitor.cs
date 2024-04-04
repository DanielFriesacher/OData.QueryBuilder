using OData.QueryBuilder.Options;

namespace OData.QueryBuilder.Expressions.Visitors
{
    internal class ODataResourceExpressionVisitor: ODataExpressionVisitor
    {
        public ODataResourceExpressionVisitor(ODataQueryBuilderOptions odataQueryBuilderOptions)
            :base(odataQueryBuilderOptions)
        {
        }
    }
}
