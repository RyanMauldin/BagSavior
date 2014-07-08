using System.Collections.Generic;

namespace BagSavior.Library
{
    /// <summary>
    /// This model is used for deserialization of json data.
    /// </summary>
    public class ProductModel
    {
        /// <summary>
        /// The number of products that can be placed in a bag.
        /// </summary>
        public int BagStrength { get; set; }

        /// <summary>
        /// A list of products to be bagged.
        /// </summary>
        public List<string> ItemTypes { get; set; }

        /// <summary>
        /// Default Constructor.
        /// </summary>
        public ProductModel()
        {
            ItemTypes = new List<string>();
        }
    }
}
