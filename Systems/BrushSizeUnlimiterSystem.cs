using Game;
using Game.UI.Editor;
using Game.UI.Widgets;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.Entities;

namespace BrushSizeUnlimiter.Systems
{
    public partial class BrushSizeUnlimiterSystem : GameSystemBase
    {

        protected override void OnCreate()
        {
            base.OnCreate();
            SetDevUIMaxBrushSize();
        }

        public void SetDevUIMaxBrushSize()
        {
            int maxSize = 10000;
            if (Mod.Options != null)
            {
                maxSize = (int)Mod.Options.MaxBrushSize;
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
