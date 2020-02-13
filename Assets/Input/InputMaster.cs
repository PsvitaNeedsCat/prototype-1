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
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Charge Release"",
                    ""type"": ""Button"",
                    ""id"": ""fed07bc1-71a5-4d53-bc47-14497e542dea"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                },
                {
                    ""name"": ""Turning"",
                    ""type"": ""Button"",
                    ""id"": ""f08a9c74-195d-4e2e-8383-b4fb0d3cd689"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""Horn Press"",
                    ""type"": ""Button"",
                    ""id"": ""0770a0b6-8535-4e2b-b32e-6065388e9b05"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Horn Release"",
                    ""type"": ""Button"",
                    ""id"": ""7fe8a4a9-a194-4474-b9d0-f5dd6d8bf732"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
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
                },
                {
                    ""name"": """",
                    ""id"": ""7e0c9e8f-4b2c-4269-aaf3-ea72b654a3f6"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Charge Release"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""72f117b2-7ba2-4b59-9d97-d0b060ef545a"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Charge Release"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3e2f13c6-585e-489c-aac0-9f0ada65b290"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Horn Release"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""716f067e-0865-492b-82a7-4887e81c6b2f"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Horn Release"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Player 2"",
            ""id"": ""ccc3d2d6-0df9-4ddc-a436-abb9cc6c039e"",
            ""actions"": [
                {
                    ""name"": ""Charge Press"",
                    ""type"": ""Button"",
                    ""id"": ""2a3a8539-bd80-431b-9f31-2040051b6973"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Charge Release"",
                    ""type"": ""Button"",
                    ""id"": ""f30483f3-5ca4-4f25-b064-05253a6f1635"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                },
                {
                    ""name"": ""Turning"",
                    ""type"": ""Button"",
                    ""id"": ""7937e464-ef7a-41fe-9874-6c9c8f0e9e80"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""Horn Press"",
                    ""type"": ""Button"",
                    ""id"": ""4d31aee6-0b62-40b4-844c-0eac7e0862e9"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Horn Release"",
                    ""type"": ""Button"",
                    ""id"": ""2f93a7ff-7b5e-4577-ae33-3987a024ad5f"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c2eec967-b95f-4ea9-85c0-69bb165732b6"",
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
                    ""id"": ""400664ae-5347-444a-acab-d49018049b77"",
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
                    ""id"": ""1cbfc4bb-d82d-42dd-8fa2-5eb4ca808ecf"",
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
                    ""id"": ""399d5494-df6a-4f49-8134-257e840ef956"",
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
                    ""id"": ""0fc0fae2-a46f-44b8-b426-e4c8bd2a47ae"",
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
                    ""id"": ""a5f31671-77fd-4ac6-992d-2ce941ae69e8"",
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
                    ""id"": ""09467955-9210-4892-9b7b-7c7fd6d07263"",
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
                    ""id"": ""b6dcc73d-1407-44fa-bf59-3ed0c3ee0c86"",
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
                    ""id"": ""69b44743-cf3e-4030-acce-f83477526278"",
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
                    ""id"": ""d63bfce2-023f-4d26-a76d-2547c1fab0df"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Horn Press"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""185b9a8c-fd21-41b8-9a71-9046f12a0e4a"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Charge Release"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""82ae8871-f67f-4f8b-bc49-ca75fd7c1ba1"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Charge Release"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8455f6e1-0e61-480d-8dff-e4c0c627c19c"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Horn Release"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f6be23c8-5864-45bd-a726-51b027c65d12"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Horn Release"",
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
        m_Player1_ChargeRelease = m_Player1.FindAction("Charge Release", throwIfNotFound: true);
        m_Player1_Turning = m_Player1.FindAction("Turning", throwIfNotFound: true);
        m_Player1_HornPress = m_Player1.FindAction("Horn Press", throwIfNotFound: true);
        m_Player1_HornRelease = m_Player1.FindAction("Horn Release", throwIfNotFound: true);
        // Player 2
        m_Player2 = asset.FindActionMap("Player 2", throwIfNotFound: true);
        m_Player2_ChargePress = m_Player2.FindAction("Charge Press", throwIfNotFound: true);
        m_Player2_ChargeRelease = m_Player2.FindAction("Charge Release", throwIfNotFound: true);
        m_Player2_Turning = m_Player2.FindAction("Turning", throwIfNotFound: true);
        m_Player2_HornPress = m_Player2.FindAction("Horn Press", throwIfNotFound: true);
        m_Player2_HornRelease = m_Player2.FindAction("Horn Release", throwIfNotFound: true);
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
    private readonly InputAction m_Player1_ChargeRelease;
    private readonly InputAction m_Player1_Turning;
    private readonly InputAction m_Player1_HornPress;
    private readonly InputAction m_Player1_HornRelease;
    public struct Player1Actions
    {
        private @InputMaster m_Wrapper;
        public Player1Actions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @ChargePress => m_Wrapper.m_Player1_ChargePress;
        public InputAction @ChargeRelease => m_Wrapper.m_Player1_ChargeRelease;
        public InputAction @Turning => m_Wrapper.m_Player1_Turning;
        public InputAction @HornPress => m_Wrapper.m_Player1_HornPress;
        public InputAction @HornRelease => m_Wrapper.m_Player1_HornRelease;
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
                @ChargeRelease.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnChargeRelease;
                @ChargeRelease.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnChargeRelease;
                @ChargeRelease.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnChargeRelease;
                @Turning.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnTurning;
                @Turning.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnTurning;
                @Turning.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnTurning;
                @HornPress.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnHornPress;
                @HornPress.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnHornPress;
                @HornPress.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnHornPress;
                @HornRelease.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnHornRelease;
                @HornRelease.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnHornRelease;
                @HornRelease.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnHornRelease;
            }
            m_Wrapper.m_Player1ActionsCallbackInterface = instance;
            if (instance != null)
            {
                @ChargePress.started += instance.OnChargePress;
                @ChargePress.performed += instance.OnChargePress;
                @ChargePress.canceled += instance.OnChargePress;
                @ChargeRelease.started += instance.OnChargeRelease;
                @ChargeRelease.performed += instance.OnChargeRelease;
                @ChargeRelease.canceled += instance.OnChargeRelease;
                @Turning.started += instance.OnTurning;
                @Turning.performed += instance.OnTurning;
                @Turning.canceled += instance.OnTurning;
                @HornPress.started += instance.OnHornPress;
                @HornPress.performed += instance.OnHornPress;
                @HornPress.canceled += instance.OnHornPress;
                @HornRelease.started += instance.OnHornRelease;
                @HornRelease.performed += instance.OnHornRelease;
                @HornRelease.canceled += instance.OnHornRelease;
            }
        }
    }
    public Player1Actions @Player1 => new Player1Actions(this);

    // Player 2
    private readonly InputActionMap m_Player2;
    private IPlayer2Actions m_Player2ActionsCallbackInterface;
    private readonly InputAction m_Player2_ChargePress;
    private readonly InputAction m_Player2_ChargeRelease;
    private readonly InputAction m_Player2_Turning;
    private readonly InputAction m_Player2_HornPress;
    private readonly InputAction m_Player2_HornRelease;
    public struct Player2Actions
    {
        private @InputMaster m_Wrapper;
        public Player2Actions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @ChargePress => m_Wrapper.m_Player2_ChargePress;
        public InputAction @ChargeRelease => m_Wrapper.m_Player2_ChargeRelease;
        public InputAction @Turning => m_Wrapper.m_Player2_Turning;
        public InputAction @HornPress => m_Wrapper.m_Player2_HornPress;
        public InputAction @HornRelease => m_Wrapper.m_Player2_HornRelease;
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
                @ChargeRelease.started -= m_Wrapper.m_Player2ActionsCallbackInterface.OnChargeRelease;
                @ChargeRelease.performed -= m_Wrapper.m_Player2ActionsCallbackInterface.OnChargeRelease;
                @ChargeRelease.canceled -= m_Wrapper.m_Player2ActionsCallbackInterface.OnChargeRelease;
                @Turning.started -= m_Wrapper.m_Player2ActionsCallbackInterface.OnTurning;
                @Turning.performed -= m_Wrapper.m_Player2ActionsCallbackInterface.OnTurning;
                @Turning.canceled -= m_Wrapper.m_Player2ActionsCallbackInterface.OnTurning;
                @HornPress.started -= m_Wrapper.m_Player2ActionsCallbackInterface.OnHornPress;
                @HornPress.performed -= m_Wrapper.m_Player2ActionsCallbackInterface.OnHornPress;
                @HornPress.canceled -= m_Wrapper.m_Player2ActionsCallbackInterface.OnHornPress;
                @HornRelease.started -= m_Wrapper.m_Player2ActionsCallbackInterface.OnHornRelease;
                @HornRelease.performed -= m_Wrapper.m_Player2ActionsCallbackInterface.OnHornRelease;
                @HornRelease.canceled -= m_Wrapper.m_Player2ActionsCallbackInterface.OnHornRelease;
            }
            m_Wrapper.m_Player2ActionsCallbackInterface = instance;
            if (instance != null)
            {
                @ChargePress.started += instance.OnChargePress;
                @ChargePress.performed += instance.OnChargePress;
                @ChargePress.canceled += instance.OnChargePress;
                @ChargeRelease.started += instance.OnChargeRelease;
                @ChargeRelease.performed += instance.OnChargeRelease;
                @ChargeRelease.canceled += instance.OnChargeRelease;
                @Turning.started += instance.OnTurning;
                @Turning.performed += instance.OnTurning;
                @Turning.canceled += instance.OnTurning;
                @HornPress.started += instance.OnHornPress;
                @HornPress.performed += instance.OnHornPress;
                @HornPress.canceled += instance.OnHornPress;
                @HornRelease.started += instance.OnHornRelease;
                @HornRelease.performed += instance.OnHornRelease;
                @HornRelease.canceled += instance.OnHornRelease;
            }
        }
    }
    public Player2Actions @Player2 => new Player2Actions(this);
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
        void OnChargeRelease(InputAction.CallbackContext context);
        void OnTurning(InputAction.CallbackContext context);
        void OnHornPress(InputAction.CallbackContext context);
        void OnHornRelease(InputAction.CallbackContext context);
    }
    public interface IPlayer2Actions
    {
        void OnChargePress(InputAction.CallbackContext context);
        void OnChargeRelease(InputAction.CallbackContext context);
        void OnTurning(InputAction.CallbackContext context);
        void OnHornPress(InputAction.CallbackContext context);
        void OnHornRelease(InputAction.CallbackContext context);
    }
}
