//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Scripts/Movement/InputSystem/PlayerControls.inputactions
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

public partial class @PlayerControls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Mech"",
            ""id"": ""ebda69a6-97cf-49d0-9de3-61c86da24e59"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""322211bc-d52b-4dcb-a053-7b332ba2309b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""7a221219-2fdd-4fe6-b67c-eb3cd274b746"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""02599f40-c85f-47d5-955e-89ec4472d3de"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""PassThrough"",
                    ""id"": ""f0db64f5-2fc2-4829-909f-a3ec36b512f4"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""63fc8695-0de3-43ce-b595-caadbc456e53"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Un-board"",
                    ""type"": ""Button"",
                    ""id"": ""f04adb56-1774-4ab6-904e-b10a4f413e5b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""c5aa915f-abe8-468f-b2da-948cd74ea57b"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""91dbeb0e-9f8a-46c6-b082-3e7c4100ca0e"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""3b9ac9c9-e268-4247-8c92-9a13d9a319cf"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""f3ca0d82-4ae8-48ff-a57e-3aa0a2baa094"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""0ceeb97a-5261-4319-a3f0-02a17e220bba"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""d8f13bf8-1e72-467d-b79d-92300a31c0f4"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6d2fe74d-5c50-46d8-8629-37c9dbefea09"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b3d6589a-c247-4891-a522-649f24585923"",
                    ""path"": ""<Pointer>/delta"",
                    ""interactions"": """",
                    ""processors"": ""InvertVector2(invertX=false)"",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""75fab8ff-96e3-40db-a96a-b4dbd7355f77"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""20f1a014-c63e-4f6e-a0b1-c00b6b787870"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Un-board"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard and Mouse"",
            ""bindingGroup"": ""Keyboard and Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Mech
        m_Mech = asset.FindActionMap("Mech", throwIfNotFound: true);
        m_Mech_Move = m_Mech.FindAction("Move", throwIfNotFound: true);
        m_Mech_Look = m_Mech.FindAction("Look", throwIfNotFound: true);
        m_Mech_Jump = m_Mech.FindAction("Jump", throwIfNotFound: true);
        m_Mech_Sprint = m_Mech.FindAction("Sprint", throwIfNotFound: true);
        m_Mech_Fire = m_Mech.FindAction("Fire", throwIfNotFound: true);
        m_Mech_Unboard = m_Mech.FindAction("Un-board", throwIfNotFound: true);
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

    // Mech
    private readonly InputActionMap m_Mech;
    private IMechActions m_MechActionsCallbackInterface;
    private readonly InputAction m_Mech_Move;
    private readonly InputAction m_Mech_Look;
    private readonly InputAction m_Mech_Jump;
    private readonly InputAction m_Mech_Sprint;
    private readonly InputAction m_Mech_Fire;
    private readonly InputAction m_Mech_Unboard;
    public struct MechActions
    {
        private @PlayerControls m_Wrapper;
        public MechActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Mech_Move;
        public InputAction @Look => m_Wrapper.m_Mech_Look;
        public InputAction @Jump => m_Wrapper.m_Mech_Jump;
        public InputAction @Sprint => m_Wrapper.m_Mech_Sprint;
        public InputAction @Fire => m_Wrapper.m_Mech_Fire;
        public InputAction @Unboard => m_Wrapper.m_Mech_Unboard;
        public InputActionMap Get() { return m_Wrapper.m_Mech; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MechActions set) { return set.Get(); }
        public void SetCallbacks(IMechActions instance)
        {
            if (m_Wrapper.m_MechActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_MechActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_MechActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_MechActionsCallbackInterface.OnMove;
                @Look.started -= m_Wrapper.m_MechActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_MechActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_MechActionsCallbackInterface.OnLook;
                @Jump.started -= m_Wrapper.m_MechActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_MechActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_MechActionsCallbackInterface.OnJump;
                @Sprint.started -= m_Wrapper.m_MechActionsCallbackInterface.OnSprint;
                @Sprint.performed -= m_Wrapper.m_MechActionsCallbackInterface.OnSprint;
                @Sprint.canceled -= m_Wrapper.m_MechActionsCallbackInterface.OnSprint;
                @Fire.started -= m_Wrapper.m_MechActionsCallbackInterface.OnFire;
                @Fire.performed -= m_Wrapper.m_MechActionsCallbackInterface.OnFire;
                @Fire.canceled -= m_Wrapper.m_MechActionsCallbackInterface.OnFire;
                @Unboard.started -= m_Wrapper.m_MechActionsCallbackInterface.OnUnboard;
                @Unboard.performed -= m_Wrapper.m_MechActionsCallbackInterface.OnUnboard;
                @Unboard.canceled -= m_Wrapper.m_MechActionsCallbackInterface.OnUnboard;
            }
            m_Wrapper.m_MechActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
                @Fire.started += instance.OnFire;
                @Fire.performed += instance.OnFire;
                @Fire.canceled += instance.OnFire;
                @Unboard.started += instance.OnUnboard;
                @Unboard.performed += instance.OnUnboard;
                @Unboard.canceled += instance.OnUnboard;
            }
        }
    }
    public MechActions @Mech => new MechActions(this);
    private int m_KeyboardandMouseSchemeIndex = -1;
    public InputControlScheme KeyboardandMouseScheme
    {
        get
        {
            if (m_KeyboardandMouseSchemeIndex == -1) m_KeyboardandMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard and Mouse");
            return asset.controlSchemes[m_KeyboardandMouseSchemeIndex];
        }
    }
    public interface IMechActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnFire(InputAction.CallbackContext context);
        void OnUnboard(InputAction.CallbackContext context);
    }
}
