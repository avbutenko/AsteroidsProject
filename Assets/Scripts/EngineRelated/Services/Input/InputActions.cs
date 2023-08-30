//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.5.1
//     from Assets/Scripts/EngineRelated/Services/Input/InputActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace AsteroidsProject.EngineRelated.Services
{
    public partial class @InputActions: IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @InputActions()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputActions"",
    ""maps"": [
        {
            ""name"": ""Ship"",
            ""id"": ""7e7ae587-8f3d-4561-b40f-1356e62409dc"",
            ""actions"": [
                {
                    ""name"": ""Accelerate"",
                    ""type"": ""Button"",
                    ""id"": ""bb8a397f-6f0b-4098-964e-963065525695"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Rotate"",
                    ""type"": ""Button"",
                    ""id"": ""c1c0f2d0-e0e8-4240-845d-977ef1c2e259"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Direction"",
                    ""id"": ""2ec67c00-c510-4288-9b2f-cfa9e448a02e"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""9a5db4d7-8381-4db1-9d7a-a97e1d2bd489"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""4c78a668-ffdc-49be-b8cb-01632ed45fad"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""7799cb1e-5da4-4d0c-91f4-349faed59cc9"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Accelerate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // Ship
            m_Ship = asset.FindActionMap("Ship", throwIfNotFound: true);
            m_Ship_Accelerate = m_Ship.FindAction("Accelerate", throwIfNotFound: true);
            m_Ship_Rotate = m_Ship.FindAction("Rotate", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }

        public IEnumerable<InputBinding> bindings => asset.bindings;

        public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
        {
            return asset.FindAction(actionNameOrId, throwIfNotFound);
        }

        public int FindBinding(InputBinding bindingMask, out InputAction action)
        {
            return asset.FindBinding(bindingMask, out action);
        }

        // Ship
        private readonly InputActionMap m_Ship;
        private List<IShipActions> m_ShipActionsCallbackInterfaces = new List<IShipActions>();
        private readonly InputAction m_Ship_Accelerate;
        private readonly InputAction m_Ship_Rotate;
        public struct ShipActions
        {
            private @InputActions m_Wrapper;
            public ShipActions(@InputActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @Accelerate => m_Wrapper.m_Ship_Accelerate;
            public InputAction @Rotate => m_Wrapper.m_Ship_Rotate;
            public InputActionMap Get() { return m_Wrapper.m_Ship; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(ShipActions set) { return set.Get(); }
            public void AddCallbacks(IShipActions instance)
            {
                if (instance == null || m_Wrapper.m_ShipActionsCallbackInterfaces.Contains(instance)) return;
                m_Wrapper.m_ShipActionsCallbackInterfaces.Add(instance);
                @Accelerate.started += instance.OnAccelerate;
                @Accelerate.performed += instance.OnAccelerate;
                @Accelerate.canceled += instance.OnAccelerate;
                @Rotate.started += instance.OnRotate;
                @Rotate.performed += instance.OnRotate;
                @Rotate.canceled += instance.OnRotate;
            }

            private void UnregisterCallbacks(IShipActions instance)
            {
                @Accelerate.started -= instance.OnAccelerate;
                @Accelerate.performed -= instance.OnAccelerate;
                @Accelerate.canceled -= instance.OnAccelerate;
                @Rotate.started -= instance.OnRotate;
                @Rotate.performed -= instance.OnRotate;
                @Rotate.canceled -= instance.OnRotate;
            }

            public void RemoveCallbacks(IShipActions instance)
            {
                if (m_Wrapper.m_ShipActionsCallbackInterfaces.Remove(instance))
                    UnregisterCallbacks(instance);
            }

            public void SetCallbacks(IShipActions instance)
            {
                foreach (var item in m_Wrapper.m_ShipActionsCallbackInterfaces)
                    UnregisterCallbacks(item);
                m_Wrapper.m_ShipActionsCallbackInterfaces.Clear();
                AddCallbacks(instance);
            }
        }
        public ShipActions @Ship => new ShipActions(this);
        public interface IShipActions
        {
            void OnAccelerate(InputAction.CallbackContext context);
            void OnRotate(InputAction.CallbackContext context);
        }
    }
}
