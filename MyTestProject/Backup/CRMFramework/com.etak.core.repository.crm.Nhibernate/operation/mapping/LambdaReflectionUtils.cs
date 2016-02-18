using System;
using System.Linq.Expressions;

namespace com.etak.core.repository.crm.Nhibernate.operation.mapping
{
    internal static class LambdaReflectionUtils
    {
        static internal Expression<Func<TSource, TReferenced>> PropertyGetLambda<TSource, TReferenced>(string propertyName)
        {
            var param = Expression.Parameter(typeof(TSource), "entity");
            var expression = Expression.Property(param, propertyName);
            var castedProperty = Expression.Convert(expression, typeof(TReferenced));
            return (Expression<Func<TSource, TReferenced>>)Expression.Lambda(castedProperty, param);
        }
    }
}
