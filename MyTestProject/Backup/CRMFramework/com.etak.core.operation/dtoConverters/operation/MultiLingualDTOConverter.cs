using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.etak.core.model;
using com.etak.core.model.revenueManagement;
using com.etak.core.operation.contract;

namespace com.etak.core.operation.dtoConverters.operation
{
    /// <summary>
    /// Convert form MultiLingualDescription to TextualDescription
    /// </summary>
    public class MultiLingualDTOConverter : ITypeConverter<MultiLingualDescription, IList<TextualDescription>>
    {
        /// <summary>
        /// Given a multilingualDescription, return a list of TextualDescriptions that represents this
        /// multilingual subscription
        /// </summary>
        /// <param name="source">The MultiLingualDescription to be transformed</param>
        /// <returns>A list of TextualDescription that represetns the multilingual description</returns>
        public IList<TextualDescription> Convert(MultiLingualDescription source)
        {
            if (source.Texts == null)
                return new List<TextualDescription>();


            return (from e in source.Texts
                select new TextualDescription()
                {
                    LanguageCode = e.Language,
                    Text = e.Text,
                }).ToList();
        }
    }
}
