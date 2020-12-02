// GENERATED AUTOMATICALLY FROM 'Assets/Input/InputMaster.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputMaster : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMaster"",
    ""maps"": [
        {
            ""name"": ""Player 1"",
            ""id"": ""ea62288e-3cba-44a1-806d-83fd5dd12565"",
            ""actions"": [
                {
                    ""name"": ""Charge Press"",
                    ""type"": ""Button"",
                    ""id"": ""945027eb-dd9f-49c2-9a24-3da27ccbbb47"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Turning"",
                    ""type"": ""Button"",
                    ""id"": ""f08a9c74-195d-4e2e-8383-b4fb0d3cd689"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Horn Press"",
                    ""type"": ""Button"",
                    ""id"": ""0770a0b6-8535-4e2b-b32e-6065388e9b05"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""cc67c6e2-1426-409f-a6f3-e043da17e331"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Charge Press"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""da0e76fd-7dbc-4954-8b90-e3b98c710fca"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Charge Press"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""ADkeys"",
                    ""id"": ""7ec12a73-1203-4737-8d1b-cd09c5007c9a"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Turning"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""0ae91a21-ae00-4b86-bf37-42294d93fb65"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Turning"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""12eae6fc-8b44-4dc4-a2f0-4de634700b1f"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Turning"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""LAnalogStick"",
                    ""id"": ""c5545baf-c6f0-4302-8814-6860fb98d41c"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Turning"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""20833a5b-8d52-4d67-86a5-80688383a7e5"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Turning"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""f385dda8-2493-47ef-a296-6da77506920b"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Turning"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""463b5e89-30fa-4a33-89ae-c5f1290ad532"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Horn Press"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""007ef950-931e-4d27-ba43-07ea727b35ec"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Horn Press"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Player 2"",
            ""id"": ""d266ce56-5f2c-40ed-8faa-849dffedfbc0"",
            ""actions"": [
                {
                    ""name"": ""Charge Press"",
                    ""type"": ""Button"",
                    ""id"": ""d5bfb650-7647-466c-9ffd-ce2ccc3a4eb5"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Turning"",
                    ""type"": ""Button"",
                    ""id"": ""f30ab654-c8c9-44bc-8e62-4ddc042353a1"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Horn Press"",
                    ""type"": ""Button"",
                    ""id"": ""3e0dab5a-4573-45a2-89eb-e3f7c320a313"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""3cac0c0f-8a5a-45da-8f85-90ee2497e95f"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Charge Press"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cad14acb-189c-4162-b782-1a27f5beebbd"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Charge Press"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Arrow Keys"",
                    ""id"": ""83c817a7-0283-44ad-9362-d05028ec77a5"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Turning"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""5301af1f-d1fa-4271-b1ba-6488ecf3674c"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Turning"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""e61d2028-f886-4034-9092-ff56f079e049"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Turning"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""LAnalogStick"",
                    ""id"": ""208a512b-4e7d-4981-9bc3-7e48c91d8dd2"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Turning"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""55e9023f-a0b4-412a-b226-451c694c739b"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Turning"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""35815f4c-e419-4368-bb4b-6ff72aabbc48"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Turning"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""5ddd3038-4394-44e1-9bdb-1f4d51360339"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Horn Press"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""408511ae-9bd1-4c39-a7fd-2e7cf99817c8"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Horn Press"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Menu"",
            ""id"": ""042e07ea-de28-4065-8be1-4b5f27ce3c45"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""c8fbb4a6-01f0-4851-92b2-e6bf6117119e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Submit"",
                    ""type"": ""Button"",
                    ""id"": ""a4c6b56d-fbe6-4664-b574-8fbd05f50be3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""7d205cf5-fc22-4263-9e68-738686a7f92e"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9505b0c8-3316-4779-83c1-92b4b50c6301"",
                    ""path"": ""<Gamepad>/dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Arrow Keys"",
                    ""id"": ""0f12b02c-bb69-45ef-aead-096623db6e6f"",
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
                    ""id"": ""9a3de24b-7da6-4c6a-a4fe-7cd0fdb1b8e2"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""d9d89df4-2058-4d35-87fb-d9b870635e74"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""a4acd3f5-7839-475d-8992-f1e4cc35cb9a"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""ef4743c0-0e6f-4c9a-90af-62d4670d6c65"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""05e44e78-4af2-4b2b-bc7a-3c7d9f7ac01e"",
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
                    ""id"": ""9935fb8e-45a5-4816-baea-7aa18475e911"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""3eeb8cfb-dbc3-4acc-9b60-c93d8869c6e4"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""77539ff8-86d7-4b68-b85d-206f1b55c500"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""13a41e73-61ab-4057-bd62-da355f089a69"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""eaedacba-a73d-4e64-89fa-c78038d6e0c4"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""81dfefc9-28f4-45b6-9fd1-c9b577adfeb9"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3a2cfa58-2c4c-4df5-bcf5-3e946aa3893a"",
                    ""path"": ""<Keyboard>/numpadEnter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c57684a4-ecec-4258-8d97-4b34e081e7bd"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
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
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player 1
        m_Player1 = asset.FindActionMap("Player 1", throwIfNotFound: true);
        m_Player1_ChargePress = m_Player1.FindAction("Charge Press", throwIfNotFound: true);
        m_Player1_Turning = m_Player1.FindAction("Turning", throwIfNotFound: true);
        m_Player1_HornPress = m_Player1.FindAction("Horn Press", throwIfNotFound: true);
        // Player 2
        m_Player2 = asset.FindActionMap("Player 2", throwIfNotFound: true);
        m_Player2_ChargePress = m_Player2.FindAction("Charge Press", throwIfNotFound: true);
        m_Player2_Turning = m_Player2.FindAction("Turning", throwIfNotFound: true);
        m_Player2_HornPress = m_Player2.FindAction("Horn Press", throwIfNotFound: true);
        // Menu
        m_Menu = asset.FindActionMap("Menu", throwIfNotFound: true);
        m_Menu_Move = m_Menu.FindAction("Move", throwIfNotFound: true);
        m_Menu_Submit = m_Menu.FindAction("Submit", throwIfNotFound: true);
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

    // Player 1
    private readonly InputActionMap m_Player1;
    private IPlayer1Actions m_Player1ActionsCallbackInterface;
    private readonly InputAction m_Player1_ChargePress;
    private readonly InputAction m_Player1_Turning;
    private readonly InputAction m_Player1_HornPress;
    public struct Player1Actions
    {
        private @InputMaster m_Wrapper;
        public Player1Actions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @ChargePress => m_Wrapper.m_Player1_ChargePress;
        public InputAction @Turning => m_Wrapper.m_Player1_Turning;
        public InputAction @HornPress => m_Wrapper.m_Player1_HornPress;
        public InputActionMap Get() { return m_Wrapper.m_Player1; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Player1Actions set) { return set.Get(); }
        public void SetCallbacks(IPlayer1Actions instance)
        {
            if (m_Wrapper.m_Player1ActionsCallbackInterface != null)
            {
                @ChargePress.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnChargePress;
                @ChargePress.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnChargePress;
                @ChargePress.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnChargePress;
                @Turning.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnTurning;
                @Turning.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnTurning;
                @Turning.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnTurning;
                @HornPress.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnHornPress;
                @HornPress.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnHornPress;
                @HornPress.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnHornPress;
            }
            m_Wrapper.m_Player1ActionsCallbackInterface = instance;
            if (instance != null)
            {
                @ChargePress.started += instance.OnChargePress;
                @ChargePress.performed += instance.OnChargePress;
                @ChargePress.canceled += instance.OnChargePress;
                @Turning.started += instance.OnTurning;
                @Turning.performed += instance.OnTurning;
                @Turning.canceled += instance.OnTurning;
                @HornPress.started += instance.OnHornPress;
                @HornPress.performed += instance.OnHornPress;
                @HornPress.canceled += instance.OnHornPress;
            }
        }
    }
    public Player1Actions @Player1 => new Player1Actions(this);

    // Player 2
    private readonly InputActionMap m_Player2;
    private IPlayer2Actions m_Player2ActionsCallbackInterface;
    private readonly InputAction m_Player2_ChargePress;
    private readonly InputAction m_Player2_Turning;
    private readonly InputAction m_Player2_HornPress;
    public struct Player2Actions
    {
        private @InputMaster m_Wrapper;
        public Player2Actions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @ChargePress => m_Wrapper.m_Player2_ChargePress;
        public InputAction @Turning => m_Wrapper.m_Player2_Turning;
        public InputAction @HornPress => m_Wrapper.m_Player2_HornPress;
        public InputActionMap Get() { return m_Wrapper.m_Player2; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Player2Actions set) { return set.Get(); }
        public void SetCallbacks(IPlayer2Actions instance)
        {
            if (m_Wrapper.m_Player2ActionsCallbackInterface != null)
            {
                @ChargePress.started -= m_Wrapper.m_Player2ActionsCallbackInterface.OnChargePress;
                @ChargePress.performed -= m_Wrapper.m_Player2ActionsCallbackInterface.OnChargePress;
                @ChargePress.canceled -= m_Wrapper.m_Player2ActionsCallbackInterface.OnChargePress;
                @Turning.started -= m_Wrapper.m_Player2ActionsCallbackInterface.OnTurning;
                @Turning.performed -= m_Wrapper.m_Player2ActionsCallbackInterface.OnTurning;
                @Turning.canceled -= m_Wrapper.m_Player2ActionsCallbackInterface.OnTurning;
                @HornPress.started -= m_Wrapper.m_Player2ActionsCallbackInterface.OnHornPress;
                @HornPress.performed -= m_Wrapper.m_Player2ActionsCallbackInterface.OnHornPress;
                @HornPress.canceled -= m_Wrapper.m_Player2ActionsCallbackInterface.OnHornPress;
            }
            m_Wrapper.m_Player2ActionsCallbackInterface = instance;
            if (instance != null)
            {
                @ChargePress.started += instance.OnChargePress;
                @ChargePress.performed += instance.OnChargePress;
                @ChargePress.canceled += instance.OnChargePress;
                @Turning.started += instance.OnTurning;
                @Turning.performed += instance.OnTurning;
                @Turning.canceled += instance.OnTurning;
                @HornPress.started += instance.OnHornPress;
                @HornPress.performed += instance.OnHornPress;
                @HornPress.canceled += instance.OnHornPress;
            }
        }
    }
    public Player2Actions @Player2 => new Player2Actions(this);

    // Menu
    private readonly InputActionMap m_Menu;
    private IMenuActions m_MenuActionsCallbackInterface;
    private readonly InputAction m_Menu_Move;
    private readonly InputAction m_Menu_Submit;
    public struct MenuActions
    {
        private @InputMaster m_Wrapper;
        public MenuActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Menu_Move;
        public InputAction @Submit => m_Wrapper.m_Menu_Submit;
        public InputActionMap Get() { return m_Wrapper.m_Menu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenuActions set) { return set.Get(); }
        public void SetCallbacks(IMenuActions instance)
        {
            if (m_Wrapper.m_MenuActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnMove;
                @Submit.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnSubmit;
                @Submit.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnSubmit;
                @Submit.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnSubmit;
            }
            m_Wrapper.m_MenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Submit.started += instance.OnSubmit;
                @Submit.performed += instance.OnSubmit;
                @Submit.canceled += instance.OnSubmit;
            }
        }
    }
    public MenuActions @Menu => new MenuActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IPlayer1Actions
    {
        void OnChargePress(InputAction.CallbackContext context);
        void OnTurning(InputAction.CallbackContext context);
        void OnHornPress(InputAction.CallbackContext context);
    }
    public interface IPlayer2Actions
    {
        void OnChargePress(InputAction.CallbackContext context);
        void OnTurning(InputAction.CallbackContext context);
        void OnHornPress(InputAction.CallbackContext context);
    }
    public interface IMenuActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnSubmit(InputAction.CallbackContext context);
    }
}
