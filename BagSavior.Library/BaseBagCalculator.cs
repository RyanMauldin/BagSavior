using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using BagSavior.Configuration;

namespace BagSavior.Library
{
    /// <summary>
    /// This class holds all base functionality for a bag calculator.
    /// </summary>
    public abstract class BaseBagCalculator : IBagCalculator
    {
        /// <summary>
        /// Regular expression object needed to verify an item type name.
        /// </summary>
        private Regex _regex;

        /// <summary>
        /// The item type name regular expression validation string.
        /// </summary>
        private string _itemTypeNameValidation;

        /// <summary>
        /// The minimum bag strength allowed.
        /// </summary>
        public int MinBagStrength { get; private set; }

        /// <summary>
        /// The maximum bag strength allowed.
        /// </summary>
        public int MaxBagStrength { get; private set; }

        /// <summary>
        /// The maximum number of item types allowed.
        /// </summary>
        public int MaxItemTypes { get; private set; }

        /// <summary>
        /// The maximum number of characters allowed in an item name.
        /// </summary>
        public int MaxItemTypeNameLength { get; private set; }

        /// <summary>
        /// The validation string to use for validating the item type name.
        /// </summary>
        public string ItemTypeNameValidation
        {
            get { return _itemTypeNameValidation; }
            private set
            {
                _itemTypeNameValidation = value;
                if (string.IsNullOrEmpty(value))
                    value = "";
                _regex = new Regex(value);
            }
        }

        /// <summary>
        /// Specific Constructor.
        /// </summary>
        /// <param name="appSettingsManager">The AppSettingsManager object to base calculations off of.</param>
        protected BaseBagCalculator(IAppSettingsManager appSettingsManager)
        {
            if (appSettingsManager == null)
                throw new Exception("An AppSettingsManager object must be provided.");

            // Initialize variables.
            MinBagStrength = appSettingsManager.MinBagStrength;
            MaxBagStrength = appSettingsManager.MaxBagStrength;
            MaxItemTypes = appSettingsManager.MaxItemTypes;
            MaxItemTypeNameLength = appSettingsManager.MaxItemTypeNameLength;
            ItemTypeNameValidation = appSettingsManager.ItemTypeNameValidation;
            _regex = new Regex(ItemTypeNameValidation);
        }

        /// <summary>
        /// Method that calculates the number of bags needed to successfully bag the products.
        /// </summary>
        /// <param name="itemType">A list of products to bag.</param>
        /// <param name="bagStrength">The strength of the bag, determined by how many products can be placed in a bag.</param>
        /// <returns>
        /// Returns the number of bags needed to bag all the items or products.
        /// </returns>
        public virtual int GetNumberOfBags(IEnumerable<string> itemType, int bagStrength)
        {
            // Basic Validation
            if (itemType == null)
                return 0;

            var itemTypes = itemType.ToList();
            var count = itemTypes.Count();
            if (count == 0)
                return 0;

            if (count > MaxItemTypes)
                throw new Exception(
                    string.Format("There were too many itemTypes to analyze. Present: {0}, Max: {1}",
                    count, MaxItemTypes));

            // Keep bag strength in an appropriate range.
            if (bagStrength < MinBagStrength)
                bagStrength = MinBagStrength;

            if (bagStrength > MaxBagStrength)
                bagStrength = MaxBagStrength;

            // Do not divide by 0
            if (bagStrength <= 0)
                bagStrength = 1;

            var strength = (decimal) bagStrength;

            // Loop through each item present and add to item dictionary
            // that tracks the total number of distinct items and their count.
            var items = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            foreach (var item in itemTypes)
            {
                // Verify the item type name.
                if (!IsValidItemType(item))
                    throw new Exception(
                        string.Format("ItemTypes contains an invalid item name: \"{0}\"",
                        string.IsNullOrEmpty(item) ? string.Empty : item));

                // If the dictionary already contains the key, increment the count.
                if (items.ContainsKey(item))
                {
                    int itemTypeCount;
                    if (items.TryGetValue(item, out itemTypeCount))
                    {
                        itemTypeCount++;
                        items[item] = itemTypeCount;
                        continue;
                    }

                    throw new Exception(
                        string.Format("Unable to fetch a value from the items dictionary that should be present. Value: {0}",
                        item));
                }
                
                // Otherwise, simply add the item.
                items.Add(item, 1);
            }

            // Calculate the total number of bags needed.
            return (int)items.Sum(p => Math.Ceiling(p.Value / strength));
        }

        /// <summary>
        /// Validates the item or product name.
        /// </summary>
        /// <param name="value">The value or item name to test for validity.</param>
        /// <returns>
        /// Returns true for a valid item name, and returns false for an invalid item name.
        /// </returns>
        public virtual bool IsValidItemType(string value)
        {
            if (String.IsNullOrEmpty(value))
                return false;

            value = value.Trim();
            if (String.IsNullOrEmpty(value))
                return false;

            return value.Length <= MaxItemTypeNameLength 
                && _regex.IsMatch(value);
        }
    }
}
