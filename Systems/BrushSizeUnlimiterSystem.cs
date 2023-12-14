using Colossal.UI;
using Colossal.UI.Binding;
using Game;
using Game.Audio;
using Game.Prefabs;
using Game.Simulation;
using Game.UI.InGame;
using Unity.Entities;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting;

namespace BrushSizeUnlimiter.Systems
{
    public class BrushSizeUnlimiterSystem : GameSystemBase
    {
        private ToolUISystem toolUI;

        protected override void OnCreate()
        {
            base.OnCreate();
            CreateKeyBinding();
            // Example on how to get a existing ECS System from the ECS World
            this.toolUI = World.GetExistingSystemManaged<ToolUISystem>();
        }

        private void CreateKeyBinding()
        {
            var inputAction = new InputAction("MyModHotkeyPress");
            inputAction.AddBinding("<Keyboard>/n");
            inputAction.performed += OnHotkeyPress;
            inputAction.Enable();
        }

        private void OnHotkeyPress(InputAction.CallbackContext obj)
        {
            UnityEngine.Debug.Log("You pressed the hotkey, very cool! Good job matey");

            //AddUpdateBinding(new GetterValueBinding<float>("tool", "brushSizeMax", () => (!m_ToolSystem.actionMode.IsEditor()) ? 1000f : 5000f));
        }

        protected override void OnUpdate() { }
    }
}
