﻿using BrushSizeUnlimiter.Locale;
using Colossal.IO.AssetDatabase;
using Game.Modding;
using Game.SceneFlow;
using Game.Settings;
using Game;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrushSizeUnlimiter
{
    [FileLocation(nameof(BrushSizeUnlimiter))]
    public class MyOptions : ModSetting
    {
        public MyOptions(IMod mod)
            : base(mod)
        {
            SetDefaults();
        }

        [SettingsUISlider(min = 1000f, max = 50000f, step = 500f, unit = "integer")]
        public float MaxBrushSize { get; set; }

        [SettingsUISection("BrushPreviewMod")]
        public bool BrushPreviewMod { get; set; }

        public override void SetDefaults()
        {
            MaxBrushSize = 10000f;
            BrushPreviewMod = true;
        }
    }
    public class MyMod : IMod
    {
        public static MyOptions Options { get; set; }

        public static MyMod Instance { get; private set; }

        public void OnDisable()
        {
        }

        public void OnDispose()
        {
        }

        public void OnEnable()
        {
        }

        public void OnLoad()
        {
            Instance = this;
        }
        public void OnCreateWorld(UpdateSystem updateSystem)
        {
            UnityEngine.Debug.Log("BrushSizeUnlimiter Options Loaded");
            Options = new(this);
            Options.RegisterInOptionsUI();
            AssetDatabase.global.LoadSettings("Options", Options, new MyOptions(this));


            foreach (var lang in GameManager.instance.localizationManager.GetSupportedLocales())
            {
                GameManager.instance.localizationManager.AddSource(lang, new LocaleEN(Options));
            }

            AssetDatabase.global.SaveSettingsNow();


        }
    }
}