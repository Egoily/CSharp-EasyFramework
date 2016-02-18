using System;


namespace com.etak.core.model.revenueManagement
{
    /// <summary>
    /// Entity that represents the relationship between two products
    /// </summary>
    public class ProductDependencyRelation
    {
        /// <summary>
        /// Identifier of the reationship contains the SourceProduct and the destination product
        /// </summary>
        public virtual Int32 Id { get; set; }

        /// <summary>
        /// Product that has a relationship
        /// </summary>
        public virtual Product SourceProduct { get; set; }

        /// <summary>
        /// Product the destination of the relationship
        /// </summary>
        public virtual Product RelatedProduct { get; set; }

        /// <summary>
        /// The type of relation between SourceProduct and RelatedProduct
        /// </summary>
        public virtual ProductRelationTypes RelationType { get; set; }

        /// <summary>
        /// The type of strategy to follow when there's customer associaciation conflict
        /// </summary>
        public virtual ProductConflictResolutionsStrategies ConflictResolutionStrategy { get; set; }

        /// <summary>
        /// Lower bound for the cardinality of the product relationship (open interval)
        /// </summary>
        public virtual Int32 MinOccurs { get; set; }

        /// <summary>
        /// Upper bound for the cardinality of the product relationship (closed interval)
        /// </summary>
        public virtual Int32 MaxOccurs { get; set; }

        /// <summary>
        /// Overrides Equals and checks if
        /// SourceProduct, RelatedProduct, MinOccurs and MaxOccurs to determine
        /// if the compared object is the same. (NH requires composite keys have Equals and HashCode overriden)
        /// </summary>
        /// <param name="obj">the object to compare to current instance</param>
        /// <returns>True if the instances are the same, false otherwise</returns>
        public override bool Equals(object obj)
        {
            ProductDependencyRelation compObj = obj as ProductDependencyRelation;
            if (compObj == null)
                return false;

            if (compObj.Id.Equals(Id) &&
                compObj.MinOccurs == MinOccurs &&
                compObj.MaxOccurs == MaxOccurs)
                return true;

            return false;
        }

        /// <summary>
        /// Calculates a hascode based on SourceProduct, RelatedProduct, MinOccurs, MaxOccurs,  ConflictResolutionStrategy and RelationType
        /// (NH requires composite keys have Equals and HashCode overriden)
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            Int32 hashCode = 0;
            if (Id != null)
                hashCode = Id.GetHashCode();

            hashCode += MinOccurs * 35 + MaxOccurs * 87 + ConflictResolutionStrategy.GetHashCode() + RelationType.GetHashCode();
            return hashCode;
        }

        
    }
}
