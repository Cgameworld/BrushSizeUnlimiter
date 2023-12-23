using Colossal.UI;
using Colossal.UI.Binding;
using Game;
using Game.Audio;
using Game.Prefabs;
using Game.Simulation;
using Game.UI.Editor;
using Game.UI.InGame;
using Game.UI.Widgets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting;

namespace BrushSizeUnlimiter.Systems
{
    public class BrushSizeUnlimiterSystem : GameSystemBase
    {

        protected override void OnCreate()
        {
            base.OnCreate();
            SetDevUIMaxBrushSize();
        }

        public void SetDevUIMaxBrushSize()
        {
            int maxSize = 10000;
            if (MyMod.Options != null)
            {
                maxSize = (int)MyMod.Options.MaxBrushSize;
            }

            FieldInfo field = typeof(EditorToolOptionsUISystem).GetField("m_BrushOptions", BindingFlags.NonPublic | BindingFlags.Instance);
            List<IWidget> m_BrushOptions = (List<IWidget>)field.GetValue(World.GetExistingSystemManaged<EditorToolOptionsUISystem>());

            if (m_BrushOptions != null)
            {
                m_BrushOptions.OfType<IntSliderField>().First().max = maxSize;
            }
        }
        protected override void OnUpdate() { }
    }
}
