using Game.Modding;
using Game;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Settings;
using BrushSizeUnlimiter.Locale;
using Colossal.IO.AssetDatabase;
using Game.SceneFlow;
using System.Runtime.InteropServices.ComTypes;

namespace BrushSizeUnlimiter
{
    public class Mod : IMod
    {
        private Harmony? _harmony;
        public static MyOptions? Options { get; set; }

        public void OnLoad(UpdateSystem updateSystem)
        {

            _harmony = new($"{nameof(BrushSizeUnlimiter)}.{nameof(Mod)}");

            _harmony.PatchAll(typeof(Mod).Assembly);

            Options = new(this);
            Options.RegisterInOptionsUI();
            GameManager.instance.localizationManager.AddSource("en-US", new LocaleEN(Options));

            AssetDatabase.global.LoadSettings(nameof(BrushSizeUnlimiter), Options, new MyOptions(this));
        }

        public void OnDispose()
        {
            _harmony?.UnpatchAll($"{nameof(BrushSizeUnlimiter)}.{nameof(Mod)}");
        }
    }
}
