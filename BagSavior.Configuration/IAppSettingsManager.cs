namespace BagSavior.Configuration
{
    /// <summary>
    /// An interface that defines the basic configuration values needed to drive the bagging program.
    /// </summary>
    public interface IAppSettingsManager
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
    }
}
