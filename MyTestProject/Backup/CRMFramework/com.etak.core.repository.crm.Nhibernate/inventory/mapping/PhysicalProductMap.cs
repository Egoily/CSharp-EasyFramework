using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using com.etak.core.model.inventory;

namespace com.etak.core.repository.crm.Nhibernate.inventory.mapping
{
    /// <summary>
    /// Class to map to Nhibernate class PhysicalProduct
    /// </summary>
    public class PhysicalProductMap : SubclassMap<PhysicalProduct>
    {
        /// <summary>
        /// The constructor called by fluent to map the class. 
        /// </summary>
        public PhysicalProductMap()
        {
            DiscriminatorValue(1);
            References(x => x.PhysicalResourceSpecification, "SPECIFICATIONID");
        }
    }
}
