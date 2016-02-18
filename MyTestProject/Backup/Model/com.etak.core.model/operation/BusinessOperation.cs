using System;

namespace com.etak.core.model.operation
{
    /// <summary>
    /// Catalog definition of all the BusinessOperations
    /// </summary>
    public abstract class BusinessOperation
    {
        private static readonly Int32[] Hashes = { -2, 55, 5434, -9775, 43423,-444, 2345, 423,88,-32};

        

        Int32 GetCustomHashCode ()
        {
            Int32 customHashCode = -1;

            Char[] letters = OperationDiscriminator.ToCharArray();
            for(int i=0; i< letters.Length; i++)
                customHashCode += Hashes[i] * letters[i];

            return customHashCode;
        }

        /// <summary>
        /// Unique Id of the operation, used as operation discriminator.
        /// </summary>
        public virtual Int32 Id {
            get { return GetCustomHashCode(); }
            set { }
        }

        /// <summary>
        /// Logical code of the operation
        /// </summary>
        public abstract String OperationCode { get; set; }

        /// <summary>
        /// Unique Id of the operation
        /// </summary>
        public abstract String OperationDiscriminator { get; }

        /// <summary>
        /// Text descriptions for the operation.
        /// </summary>
        public virtual MultiLingualDescription Descriptions { get; set; }

    }
}
