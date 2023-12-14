using BrushSizeUnlimiter.Systems;
using Colossal.UI;
using Colossal.UI.Binding;
using Game;
using Game.Audio;
using Game.SceneFlow;
using Game.UI.Editor;
using Game.UI.InGame;
using Game.UI.Menu;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;

namespace BrushSizeUnlimiter.Patches
{
    // This example patch adds the loading of a custom ECS System after the AudioManager has
    // its "OnGameLoadingComplete" method called. We're just using it as a entrypoint, and
    // it won't affect anything related to audio.
    [HarmonyPatch(typeof(AudioManager), "OnGameLoadingComplete")]
    internal class AudioManager_OnGameLoadingComplete
    {
        static void Postfix(AudioManager __instance, Colossal.Serialization.Entities.Purpose purpose, GameMode mode)
        {
            if (!mode.IsGameOrEditor())
                return;

            // Here we add our custom ECS System to the game's ECS World, so it's "online" at runtime
            __instance.World.GetOrCreateSystem<BrushSizeUnlimiterSystem>();
        }
    }



    //patch ingame brush tool
    [HarmonyPatch(typeof(ToolUISystem))]
    [HarmonyPatch("<OnCreate>b__17_15")]
    public static class OnCreatePatch
    {
        static bool Prefix(ref float __result)
        {
            __result = 15000f;
            return false;
      }
       
    }

    
    //patch devmode brush tool
    [HarmonyPatch(typeof(EditorToolOptionsUISystem))]
    [HarmonyPatch("OnCreate")]
    public static class OnCreatePatchDevMode
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var codes = new List<CodeInstruction>(instructions);
            var newInstruction = new CodeInstruction(OpCodes.Ldc_I4, 10000);

            for (var i = 0; i < codes.Count; i++)
            {
              if (codes[i].opcode == OpCodes.Ldc_I4 && (int)codes[i].operand == 0x1F4)
              {
                    codes.RemoveAt(i);
                    codes.Insert(i, newInstruction);

                }
            }
            return codes.AsEnumerable();
        }
    }
   
}