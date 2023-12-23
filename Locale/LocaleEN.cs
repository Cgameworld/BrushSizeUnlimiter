//adapted from TreeController

namespace BrushSizeUnlimiter.Locale
{
    using System.Collections.Generic;
    using BrushSizeUnlimiter;
    using Colossal;

    public class LocaleEN : IDictionarySource
    {
        private readonly MyOptions m_Setting;
        public LocaleEN(MyOptions options)
        {
            m_Setting = options;
        }

        public IEnumerable<KeyValuePair<string, string>> ReadEntries(IList<IDictionaryEntryError> errors, Dictionary<string, int> indexCounts)
        {
            // \n for making new lines doesn't work - hacky way to make new lines
            string brushSizeDescription = "Sets the maximum size the brush can be set to in the game/dev UI." + new string('\r', 210) + "Note: The larger the brush is, the more performance degrades";
            string brushPreviewModDescription = "Having this enabled massively improves performance when using large object brushes (>1500 size) by temporarily setting the brush strength in the background while hovering to 0." + new string('\r', 105) + "Warning: Disabling this will make large object brushes extremely laggy." + new string('\r', 180) + "Disable if there is compatibility issues";

            return new Dictionary<string, string>
            {
                { m_Setting.GetSettingsLocaleID(), "Brush Size Unlimiter" },
                { m_Setting.GetOptionLabelLocaleID(nameof(MyOptions.MaxBrushSize)), "Maximum Brush Size" },
                { m_Setting.GetOptionDescLocaleID(nameof(MyOptions.MaxBrushSize)), brushSizeDescription},
                { m_Setting.GetOptionLabelLocaleID(nameof(MyOptions.BrushPreviewMod)), "Hide Object Placement Preview With Large Brush Sizes" },
                { m_Setting.GetOptionDescLocaleID(nameof(MyOptions.BrushPreviewMod)), brushPreviewModDescription}
            };

        }
        public void Unload()
        {
        }
    }
}