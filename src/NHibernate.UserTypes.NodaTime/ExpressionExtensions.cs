using System;
using System.Linq.Expressions;

namespace NHibernate.UserTypes.NodaTime
{
    public static class ExpressionExtensions
    {
        public static string GetPropertyName<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> expression)
        {
            if (expression.Body.NodeType == ExpressionType.MemberAccess)
                return ((MemberExpression) expression.Body).Member.Name;
            if (expression.Body.NodeType == ExpressionType.Convert && expression.Body.Type == typeof (TProperty))
                return ((MemberExpression) ((UnaryExpression) expression.Body).Operand).Member.Name;
            throw new ArgumentException($"Invalid expression type: Expected ExpressionType.MemberAccess, Found {(object) expression.Body.NodeType}", nameof (expression));
        }
    }
}