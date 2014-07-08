using BagSavior.Configuration;

namespace BagSavior.Library
{
    /// <summary>
    /// Default implementation of the BaseBagCalculator.
    /// </summary>
    public class BagCalculator : BaseBagCalculator
    {
        /// <summary>
        /// Specific Constructor.
        /// </summary>
        /// <param name="appSettingsManager">The AppSettingsManager object to base calculations off of.</param>
        public BagCalculator(IAppSettingsManager appSettingsManager) : base(appSettingsManager)
        {

        }
    }
}
