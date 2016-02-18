using System;
using System.Linq.Expressions;
using MongoDB.Bson.Serialization;

namespace com.etak.core.repository.mongo.extensions
{
    /// <summary>
    /// Extension helpers for MongoDB class mapping
    /// </summary>
    public static class BsonClassMapExtentions
    {
        public static BsonMemberMap MapPropertyIgnoreDefault<TClass, TMember>(this BsonClassMap<TClass> map, Expression<Func<TClass, TMember>> propertyLambda, String property)
        {
            return (map.MapProperty(propertyLambda).SetIgnoreIfDefault(true).SetElementName(property));       
        }
    }
}
