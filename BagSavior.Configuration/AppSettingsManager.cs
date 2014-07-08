using System;
using System.Configuration;

namespace BagSavior.Configuration
{
    /// <summary>
    /// A class that defines and drives the basic configuration values needed for the bagging program.
    /// </summary>
    public class AppSettingsManager : IAppSettingsManager
    {
        // Default values for the configuration in case the app.config file is not present,
        // or if the values are not within a valid base range.
        private const int DefaultMinBagStrength = 1;
        private const int DefaultMaxBagStrength = 50;
        private const int DefaultMaxItemTypes = 50;
        private const int DefaultMaxItemTypeNameLength = 50;
        private const string DefaultItemTypeNameValidation = "^[A-Z]*$";

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
        public string ItemTypeNameValidation { get; private set; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public AppSettingsManager()
        {
            // Pull vaues from App.config, and if the value is not present or is invalid, use the default.
            int minBagStrength;
            MinBagStrength = Int32.TryParse(ConfigurationManager.AppSettings["MinBagStrength"], out minBagStrength)
                ? minBagStrength
                : DefaultMinBagStrength;

            if (MinBagStrength < 1)
                MinBagStrength = DefaultMinBagStrength;

            int maxBagStrength;
            MaxBagStrength = Int32.TryParse(ConfigurationManager.AppSettings["MaxBagStrength"], out maxBagStrength)
                ? maxBagStrength
                : DefaultMaxBagStrength;

            if (MaxBagStrength < 1)
                MaxBagStrength = DefaultMaxBagStrength;

            if (MinBagStrength > MaxBagStrength)
            {
                var temp = MinBagStrength;
                MinBagStrength = MaxBagStrength;
                MaxBagStrength = temp;
            }

            int maxItemTypes;
            MaxItemTypes = Int32.TryParse(ConfigurationManager.AppSettings["MaxItemTypes"], out maxItemTypes)
                ? maxItemTypes
                : DefaultMaxItemTypes;

            if (maxItemTypes < 1)
                MaxItemTypes = DefaultMaxItemTypes;

            int maxItemTypeNameLength;
            MaxItemTypeNameLength = Int32.TryParse(ConfigurationManager.AppSettings["MaxItemTypeNameLength"], out maxItemTypeNameLength)
                ? maxItemTypeNameLength
                : DefaultMaxItemTypeNameLength;

            if (maxItemTypeNameLength < 1)
                MaxItemTypeNameLength = DefaultMaxItemTypeNameLength;

            ItemTypeNameValidation = ConfigurationManager.AppSettings["ItemTypeNameValidation"];
            if (String.IsNullOrEmpty(ItemTypeNameValidation))
                ItemTypeNameValidation = DefaultItemTypeNameValidation;
        }
    }
}
