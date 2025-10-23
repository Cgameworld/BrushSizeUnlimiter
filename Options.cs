using BrushSizeUnlimiter.Locale;
using BrushSizeUnlimiter.Systems;
using Colossal.IO.AssetDatabase;
using Game;
using Game.Modding;
using Game.SceneFlow;
using Game.Settings;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Unity.Entities;

namespace BrushSizeUnlimiter
{
    [FileLocation(nameof(BrushSizeUnlimiter))]
    [SettingsUIShowGroupName(GeneralSettingsGroup, OtherSettingsGroup)]
    public class MyOptions : ModSetting
    {
        public const string GeneralSettingsGroup = "GeneralSettingsGroup";
        public const string OtherSettingsGroup = "OtherSettingsGroup";

        private float _maxBrushSize;
        private bool brushPreviewMod;
        private BrushSizeUnlimiterSystem m_brushSizeUnlimiterSystem;

        public MyOptions(IMod mod)
            : base(mod)
        {
            SetDefaults();
        }
        [SettingsUISection(GeneralSettingsGroup)]
        [SettingsUISlider(min = 1000f, max = 20000f, step = 500f, unit = "integer")]
        public float MaxBrushSize
        {
            get => _maxBrushSize;
            set
            {
                _maxBrushSize = value;
                MakeSureSave = new Random().Next();
            }
        }

        [SettingsUISection(GeneralSettingsGroup)]
        public bool BrushPreviewMod
        {
            get => brushPreviewMod;
            set
            {
                brushPreviewMod = value;
                MakeSureSave = new Random().Next();
            }
        }

        [SettingsUISection(OtherSettingsGroup)]
        public string ModVersion => Assembly.GetExecutingAssembly().GetName().Version.ToString();

        //sometimes saving doesn't happen when changing values to their default? - hack to guarantee
        [SettingsUIHidden]
        public int MakeSureSave { get; set; }

        public override void SetDefaults()
        {
            MakeSureSave = 0;
            MaxBrushSize = 10000f;
            BrushPreviewMod = true;
        }
    }
}
