using BrushSizeUnlimiter.Locale;
using Colossal.IO.AssetDatabase;
using Game.Modding;
using Game.SceneFlow;
using Game.Settings;
using Game;
using System;
using System.Collections.Generic;
using System.Text;
using BrushSizeUnlimiter.Systems;
using Unity.Entities;

namespace BrushSizeUnlimiter
{
    [FileLocation(nameof(BrushSizeUnlimiter))]
    public class MyOptions : ModSetting
    {
        private float _maxBrushSize;
        private bool brushPreviewMod;
        private BrushSizeUnlimiterSystem m_brushSizeUnlimiterSystem;

        public MyOptions(IMod mod)
            : base(mod)
        {
            SetDefaults();
        }

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

        [SettingsUISection("BrushPreviewMod")]
        public bool BrushPreviewMod
        {
            get => brushPreviewMod;
            set
            {
                brushPreviewMod = value;
                MakeSureSave = new Random().Next();
            }
        }

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
