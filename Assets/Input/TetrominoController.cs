// GENERATED AUTOMATICALLY FROM 'Assets/Input/PlayerController.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerController : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerController()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerController"",
    ""maps"": [
        {
            ""name"": ""Tetromino"",
            ""id"": ""1d0512d0-4c99-42dd-b503-8ac59870bd17"",
            ""actions"": [
                {
                    ""name"": ""Rotate"",
                    ""type"": ""Button"",
                    ""id"": ""b8baa271-ecff-415e-94c0-0b8999d8db2e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Up"",
                    ""type"": ""Button"",
                    ""id"": ""708a629e-6741-4367-bd77-cedd0d824810"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Down"",
                    ""type"": ""Button"",
                    ""id"": ""053c32b6-44e9-4738-8a4d-5a0f1340b59d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Right"",
                    ""type"": ""Button"",
                    ""id"": ""7da1d0c8-8532-41aa-a4b2-1634696c0a10"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Left"",
                    ""type"": ""Button"",
                    ""id"": ""614d4e01-7ddb-47c9-9238-2b570047b7f4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c5b48ba6-245d-4267-9a56-cf67264be424"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""38e363b2-b07a-4c0b-9850-c388f1e1ec4d"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""02506330-24bc-4b8f-be70-b9ed11444112"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fb53cd25-776e-46c5-8cc6-0c8c64f8e4ed"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1b0d414f-d5b9-4ea7-95f6-43e4d39d175e"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b1d8edbb-6bad-466b-82ce-c227e2a6f121"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4e5d4b0e-8016-4f11-84fb-0f0c47d09ac4"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8db11eb1-87b9-4d78-9127-9127ea2eded4"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e1bbfe10-edde-4535-9f7c-27bb94efeed7"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2544031d-6891-4391-bcdd-1faee5077e17"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Tetromino
        m_Tetromino = asset.FindActionMap("Tetromino", throwIfNotFound: true);
        m_Tetromino_Rotate = m_Tetromino.FindAction("Rotate", throwIfNotFound: true);
        m_Tetromino_Up = m_Tetromino.FindAction("Up", throwIfNotFound: true);
        m_Tetromino_Down = m_Tetromino.FindAction("Down", throwIfNotFound: true);
        m_Tetromino_Right = m_Tetromino.FindAction("Right", throwIfNotFound: true);
        m_Tetromino_Left = m_Tetromino.FindAction("Left", throwIfNotFound: true);
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

    // Tetromino
    private readonly InputActionMap m_Tetromino;
    private ITetrominoActions m_TetrominoActionsCallbackInterface;
    private readonly InputAction m_Tetromino_Rotate;
    private readonly InputAction m_Tetromino_Up;
    private readonly InputAction m_Tetromino_Down;
    private readonly InputAction m_Tetromino_Right;
    private readonly InputAction m_Tetromino_Left;
    public struct TetrominoActions
    {
        private @PlayerController m_Wrapper;
        public TetrominoActions(@PlayerController wrapper) { m_Wrapper = wrapper; }
        public InputAction @Rotate => m_Wrapper.m_Tetromino_Rotate;
        public InputAction @Up => m_Wrapper.m_Tetromino_Up;
        public InputAction @Down => m_Wrapper.m_Tetromino_Down;
        public InputAction @Right => m_Wrapper.m_Tetromino_Right;
        public InputAction @Left => m_Wrapper.m_Tetromino_Left;
        public InputActionMap Get() { return m_Wrapper.m_Tetromino; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TetrominoActions set) { return set.Get(); }
        public void SetCallbacks(ITetrominoActions instance)
        {
            if (m_Wrapper.m_TetrominoActionsCallbackInterface != null)
            {
                @Rotate.started -= m_Wrapper.m_TetrominoActionsCallbackInterface.OnRotate;
                @Rotate.performed -= m_Wrapper.m_TetrominoActionsCallbackInterface.OnRotate;
                @Rotate.canceled -= m_Wrapper.m_TetrominoActionsCallbackInterface.OnRotate;
                @Up.started -= m_Wrapper.m_TetrominoActionsCallbackInterface.OnUp;
                @Up.performed -= m_Wrapper.m_TetrominoActionsCallbackInterface.OnUp;
                @Up.canceled -= m_Wrapper.m_TetrominoActionsCallbackInterface.OnUp;
                @Down.started -= m_Wrapper.m_TetrominoActionsCallbackInterface.OnDown;
                @Down.performed -= m_Wrapper.m_TetrominoActionsCallbackInterface.OnDown;
                @Down.canceled -= m_Wrapper.m_TetrominoActionsCallbackInterface.OnDown;
                @Right.started -= m_Wrapper.m_TetrominoActionsCallbackInterface.OnRight;
                @Right.performed -= m_Wrapper.m_TetrominoActionsCallbackInterface.OnRight;
                @Right.canceled -= m_Wrapper.m_TetrominoActionsCallbackInterface.OnRight;
                @Left.started -= m_Wrapper.m_TetrominoActionsCallbackInterface.OnLeft;
                @Left.performed -= m_Wrapper.m_TetrominoActionsCallbackInterface.OnLeft;
                @Left.canceled -= m_Wrapper.m_TetrominoActionsCallbackInterface.OnLeft;
            }
            m_Wrapper.m_TetrominoActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Rotate.started += instance.OnRotate;
                @Rotate.performed += instance.OnRotate;
                @Rotate.canceled += instance.OnRotate;
                @Up.started += instance.OnUp;
                @Up.performed += instance.OnUp;
                @Up.canceled += instance.OnUp;
                @Down.started += instance.OnDown;
                @Down.performed += instance.OnDown;
                @Down.canceled += instance.OnDown;
                @Right.started += instance.OnRight;
                @Right.performed += instance.OnRight;
                @Right.canceled += instance.OnRight;
                @Left.started += instance.OnLeft;
                @Left.performed += instance.OnLeft;
                @Left.canceled += instance.OnLeft;
            }
        }
    }
    public TetrominoActions @Tetromino => new TetrominoActions(this);
    public interface ITetrominoActions
    {
        void OnRotate(InputAction.CallbackContext context);
        void OnUp(InputAction.CallbackContext context);
        void OnDown(InputAction.CallbackContext context);
        void OnRight(InputAction.CallbackContext context);
        void OnLeft(InputAction.CallbackContext context);
    }
}
