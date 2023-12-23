using BrushSizeUnlimiter.Systems;
using Colossal.UI;
using Colossal.UI.Binding;
using Game;
using Game.Audio;
using Game.Common;
using Game.SceneFlow;
using Game.Tools;
using Game.UI.Editor;
using Game.UI.InGame;
using Game.UI.Menu;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

    [HarmonyPatch(typeof(SystemOrder), nameof(SystemOrder.Initialize))]
    internal class InjectSystemsPatch
    {
        static void Postfix(UpdateSystem updateSystem)
        {
            MyMod.Instance.OnCreateWorld(updateSystem);
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

    //set brush strength to 0 when hovering
    [HarmonyPatch(typeof(ObjectToolSystem))]
    public static class ObjectToolPatch
    {
        static float brushStrengthTemp = 0f;
        static Type type = typeof(ObjectToolSystem);
        static FieldInfo fieldInfo = type.GetField("m_State", BindingFlags.NonPublic | BindingFlags.Instance);

        [HarmonyPatch("Update")]
        [HarmonyPrefix]
        public static void Prefix(ObjectToolSystem __instance)
        {
            int stateValue = Convert.ToInt32(fieldInfo.GetValue(__instance));

            if (__instance.actualMode == ObjectToolSystem.Mode.Brush)
            {
                if (stateValue == 2 || stateValue == 3)
                {
                    __instance.brushStrength = brushStrengthTemp;
                }
                else
                {
                    brushStrengthTemp = __instance.brushStrength;
                    __instance.brushStrength = 0f;
                }
            }

        }

        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        public static void Postfix(ObjectToolSystem __instance)
        {
            int stateValue = Convert.ToInt32(fieldInfo.GetValue(__instance));

            if (__instance.actualMode == ObjectToolSystem.Mode.Brush)
            {
                if (stateValue != 2 || stateValue != 3)
                {
                    __instance.brushStrength = brushStrengthTemp;
                }
            }
        }
    }

}