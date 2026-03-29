using System.Linq.Expressions;

namespace ExampleApp.Infrastructure.Services;

public class NHibExpressionVisitorSource : INHibExpressionVisitorSource
{
    public ExpressionVisitor Visitor { get; } = SquashWhereQueryableVisitor.Value;
}