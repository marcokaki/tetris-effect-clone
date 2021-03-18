// GENERATED AUTOMATICALLY FROM 'Assets/Input/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
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
                    ""id"": ""f24e1630-6d60-4343-8ff4-792e03116550"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Down"",
                    ""type"": ""Button"",
                    ""id"": ""6950517d-7178-4338-8917-2e475dac6ac2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Left"",
                    ""type"": ""Button"",
                    ""id"": ""62ca53b1-ee83-4ab3-94bb-77ceb16107d9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Right"",
                    ""type"": ""Button"",
                    ""id"": ""b02400e8-fb59-4964-9009-aec9313cc1dd"",
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
                    ""id"": ""06977f88-e835-4b06-962a-4b65d67b4d28"",
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
                    ""id"": ""bf17f80c-ebad-435a-bf4b-f14d5234c32b"",
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
                    ""id"": ""ea589c1e-90de-4119-8073-017e6e75ed33"",
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
                    ""id"": ""74b2aadf-3682-4129-ac0c-44c596b23686"",
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
                    ""id"": ""1e040ea2-48bc-41f4-a5db-60a4eb87ca6e"",
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
                    ""id"": ""709008f5-8e9a-49d4-8d1b-6f7eee795e7a"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""830f7e26-0852-4f55-a9eb-7ad0758a2c35"",
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
                    ""id"": ""068ae630-0947-490d-82d8-7dd49e6e0423"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Right"",
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
        m_Tetromino_Left = m_Tetromino.FindAction("Left", throwIfNotFound: true);
        m_Tetromino_Right = m_Tetromino.FindAction("Right", throwIfNotFound: true);
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
    private readonly InputAction m_Tetromino_Left;
    private readonly InputAction m_Tetromino_Right;
    public struct TetrominoActions
    {
        private @Controls m_Wrapper;
        public TetrominoActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Rotate => m_Wrapper.m_Tetromino_Rotate;
        public InputAction @Up => m_Wrapper.m_Tetromino_Up;
        public InputAction @Down => m_Wrapper.m_Tetromino_Down;
        public InputAction @Left => m_Wrapper.m_Tetromino_Left;
        public InputAction @Right => m_Wrapper.m_Tetromino_Right;
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
                @Left.started -= m_Wrapper.m_TetrominoActionsCallbackInterface.OnLeft;
                @Left.performed -= m_Wrapper.m_TetrominoActionsCallbackInterface.OnLeft;
                @Left.canceled -= m_Wrapper.m_TetrominoActionsCallbackInterface.OnLeft;
                @Right.started -= m_Wrapper.m_TetrominoActionsCallbackInterface.OnRight;
                @Right.performed -= m_Wrapper.m_TetrominoActionsCallbackInterface.OnRight;
                @Right.canceled -= m_Wrapper.m_TetrominoActionsCallbackInterface.OnRight;
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
                @Left.started += instance.OnLeft;
                @Left.performed += instance.OnLeft;
                @Left.canceled += instance.OnLeft;
                @Right.started += instance.OnRight;
                @Right.performed += instance.OnRight;
                @Right.canceled += instance.OnRight;
            }
        }
    }
    public TetrominoActions @Tetromino => new TetrominoActions(this);
    public interface ITetrominoActions
    {
        void OnRotate(InputAction.CallbackContext context);
        void OnUp(InputAction.CallbackContext context);
        void OnDown(InputAction.CallbackContext context);
        void OnLeft(InputAction.CallbackContext context);
        void OnRight(InputAction.CallbackContext context);
    }
}
