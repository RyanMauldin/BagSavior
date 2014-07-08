using System.Collections.Generic;

namespace BagSavior.Library
{
    /// <summary>
    /// An interface that defines the basic functionality needed to calculate the number of bags needed.
    /// </summary>
    public interface IBagCalculator
    {
        /// <summary>
        /// The minimum bag strength allowed.
        /// </summary>
        int MinBagStrength { get; }

        /// <summary>
        /// The maximum bag strength allowed.
        /// </summary>
        int MaxBagStrength { get; }

        /// <summary>
        /// The maximum number of item types allowed.
        /// </summary>
        int MaxItemTypes { get; }

        /// <summary>
        /// The maximum number of characters allowed in an item name.
        /// </summary>
        int MaxItemTypeNameLength { get; }

        /// <summary>
        /// The validation string to use for validating the item type name.
        /// </summary>
        string ItemTypeNameValidation { get; }

        /// <summary>
        /// Method that calculates the number of bags needed to successfully bag the products.
        /// </summary>
        /// <param name="itemType">A list of products to bag.</param>
        /// <param name="bagStrength">The strength of the bag, determined by how many products can be placed in a bag.</param>
        /// <returns>
        /// Returns the number of bags needed to bag all the items or products.
        /// </returns>
        int GetNumberOfBags(IEnumerable<string> itemType, int bagStrength);

        /// <summary>
        /// Validates the item or product name.
        /// </summary>
        /// <param name="value">The value or item name to test for validity.</param>
        /// <returns>
        /// Returns true for a valid item name, and returns false for an invalid item name.
        /// </returns>
        bool IsValidItemType(string value);
    }
}
