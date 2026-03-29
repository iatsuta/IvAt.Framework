using System.Linq.Expressions;

namespace ExampleApp.Infrastructure.Services;

public interface INHibExpressionVisitorSource
{
    ExpressionVisitor Visitor { get; }
}