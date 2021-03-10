// GENERATED AUTOMATICALLY FROM 'Assets/PlayerController.inputactions'

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
            ""name"": ""Gameplay"",
            ""id"": ""b53ad473-e50f-4477-a55c-05286465db5c"",
            ""actions"": [
                {
                    ""name"": ""Up"",
                    ""type"": ""Button"",
                    ""id"": ""858cf016-c64c-49c2-ac8c-90f39d1ed741"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Down"",
                    ""type"": ""Button"",
                    ""id"": ""10909051-9df8-4f7f-bf56-0f9c64fd789e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Left"",
                    ""type"": ""Button"",
                    ""id"": ""415068e3-71b3-4335-817a-28fc296dc2f2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Right"",
                    ""type"": ""Button"",
                    ""id"": ""2660d30f-6a16-4e71-a77a-896219676253"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Rotate"",
                    ""type"": ""Button"",
                    ""id"": ""a7723ecb-eb06-4223-a602-5c8a499cb5c7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Start"",
                    ""type"": ""Button"",
                    ""id"": ""be19d366-5413-430d-84ba-0ec1ea88df22"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Gravitate"",
                    ""type"": ""Button"",
                    ""id"": ""75984ee3-6bf8-4d29-b12c-ddbd1ef3db1c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Destroy"",
                    ""type"": ""Button"",
                    ""id"": ""cf6d12b9-a2c2-4420-aa72-1d44f27f3db2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""fc0f71ee-677c-41bb-962e-8f99a2d17441"",
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
                    ""id"": ""827e4dc3-bc7a-403f-9a3b-07b598b12f08"",
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
                    ""id"": ""83a8db32-fbd2-4177-803e-9002589d7f54"",
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
                    ""id"": ""77b79e5c-1318-4e4c-ba49-4a29b49bd13b"",
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
                    ""id"": ""fd2bfaeb-b6cc-438d-b524-5e12088c5f23"",
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
                    ""id"": ""cfd53d38-cf00-4b2a-9b56-3edd640322e5"",
                    ""path"": ""<Gamepad>/rightStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Start"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""72b5bc6d-1fd1-46e7-ac72-6b9ac5ac1068"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Gravitate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f948b03e-9eb7-4007-91b1-119f079d23f6"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Destroy"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_Up = m_Gameplay.FindAction("Up", throwIfNotFound: true);
        m_Gameplay_Down = m_Gameplay.FindAction("Down", throwIfNotFound: true);
        m_Gameplay_Left = m_Gameplay.FindAction("Left", throwIfNotFound: true);
        m_Gameplay_Right = m_Gameplay.FindAction("Right", throwIfNotFound: true);
        m_Gameplay_Rotate = m_Gameplay.FindAction("Rotate", throwIfNotFound: true);
        m_Gameplay_Start = m_Gameplay.FindAction("Start", throwIfNotFound: true);
        m_Gameplay_Gravitate = m_Gameplay.FindAction("Gravitate", throwIfNotFound: true);
        m_Gameplay_Destroy = m_Gameplay.FindAction("Destroy", throwIfNotFound: true);
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

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_Up;
    private readonly InputAction m_Gameplay_Down;
    private readonly InputAction m_Gameplay_Left;
    private readonly InputAction m_Gameplay_Right;
    private readonly InputAction m_Gameplay_Rotate;
    private readonly InputAction m_Gameplay_Start;
    private readonly InputAction m_Gameplay_Gravitate;
    private readonly InputAction m_Gameplay_Destroy;
    public struct GameplayActions
    {
        private @PlayerController m_Wrapper;
        public GameplayActions(@PlayerController wrapper) { m_Wrapper = wrapper; }
        public InputAction @Up => m_Wrapper.m_Gameplay_Up;
        public InputAction @Down => m_Wrapper.m_Gameplay_Down;
        public InputAction @Left => m_Wrapper.m_Gameplay_Left;
        public InputAction @Right => m_Wrapper.m_Gameplay_Right;
        public InputAction @Rotate => m_Wrapper.m_Gameplay_Rotate;
        public InputAction @Start => m_Wrapper.m_Gameplay_Start;
        public InputAction @Gravitate => m_Wrapper.m_Gameplay_Gravitate;
        public InputAction @Destroy => m_Wrapper.m_Gameplay_Destroy;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @Up.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnUp;
                @Up.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnUp;
                @Up.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnUp;
                @Down.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDown;
                @Down.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDown;
                @Down.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDown;
                @Left.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeft;
                @Left.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeft;
                @Left.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeft;
                @Right.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRight;
                @Right.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRight;
                @Right.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRight;
                @Rotate.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRotate;
                @Rotate.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRotate;
                @Rotate.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRotate;
                @Start.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnStart;
                @Start.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnStart;
                @Start.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnStart;
                @Gravitate.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnGravitate;
                @Gravitate.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnGravitate;
                @Gravitate.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnGravitate;
                @Destroy.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDestroy;
                @Destroy.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDestroy;
                @Destroy.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDestroy;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
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
                @Rotate.started += instance.OnRotate;
                @Rotate.performed += instance.OnRotate;
                @Rotate.canceled += instance.OnRotate;
                @Start.started += instance.OnStart;
                @Start.performed += instance.OnStart;
                @Start.canceled += instance.OnStart;
                @Gravitate.started += instance.OnGravitate;
                @Gravitate.performed += instance.OnGravitate;
                @Gravitate.canceled += instance.OnGravitate;
                @Destroy.started += instance.OnDestroy;
                @Destroy.performed += instance.OnDestroy;
                @Destroy.canceled += instance.OnDestroy;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    public interface IGameplayActions
    {
        void OnUp(InputAction.CallbackContext context);
        void OnDown(InputAction.CallbackContext context);
        void OnLeft(InputAction.CallbackContext context);
        void OnRight(InputAction.CallbackContext context);
        void OnRotate(InputAction.CallbackContext context);
        void OnStart(InputAction.CallbackContext context);
        void OnGravitate(InputAction.CallbackContext context);
        void OnDestroy(InputAction.CallbackContext context);
    }
}
