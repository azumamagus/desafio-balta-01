using System.Linq.Expressions;

namespace Balta.IBGE.Application.Extensions;

public static class LinqExtension
{
    public static Expression<Func<T, bool>> Combine<T>(
        this Expression<Func<T, bool>> predicate,
        bool conditional,
        Func<Expression, Expression, BinaryExpression> combination,
        Expression<Func<T, bool>> withPredicate)
    {
        if (!conditional)
            return predicate;

        var invocation = Expression.Invoke(withPredicate, predicate.Parameters);
        var combined = combination(predicate.Body, invocation);
        
        return Expression.Lambda<Func<T, bool>>(combined, predicate.Parameters);
    }
}