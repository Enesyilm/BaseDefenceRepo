using System;
using Data.ValueObject;
using Data.ValueObjects;
using Enum;
using Enums;
using Keys;
using Photon.Pun;
using Signals;
using UnityEngine;

namespace Managers.CoreGameManagers
{
    enum PlayerType
    {
        wasd,
        joyStick
    }
    public class InputManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [Header("Data")] public InputData Data;

        #endregion Public Variables

        #region Serialized Variables

        [SerializeField] private FloatingJoystick joystick;

        
        [SerializeField] private bool isReadyForTouch=true;//OnPlayde True olacak
        private InputHandlers _inputHandlers = InputHandlers.Character;


        #endregion Serialized Variables

        #region Private Variables

        private bool _isTouching;

        private bool _hasTouched;
        private PhotonView pw;


        #endregion Private Variables

        #endregion Self Variables
        

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void Start()
        {
            pw = GetComponent<PhotonView>();
        }

        private void SubscribeEvents()
        {
       
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
            InputSignals.Instance.onInputHandlerChange += OnInputHandlerChange;

        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
            InputSignals.Instance.onInputHandlerChange -= OnInputHandlerChange;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion Event Subscriptions

        private void Update()
        {   
            //if (!isReadyForTouch) return;
            HandleWasd();
            if (Input.GetMouseButton(0) && !_hasTouched)
            {
                _hasTouched = true;
                InputSignals.Instance.onInputTakenActive?.Invoke(true);
            }

            if (!_hasTouched)
            {
                InputSignals.Instance.onInputTakenActive?.Invoke(false);
                return;
            }
            Debug.Log("ıupup");

                HandleJoystickInput();

                _hasTouched = joystick.Direction.sqrMagnitude > 0;                         
            
        }

        private void HandleWasd()
        {
            
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            InputSignals.Instance.onInputTakenWasd?.Invoke(new XZInputParams()
            {
                        
                XValue = horizontal,
                ZValue = vertical
                //InputValues = new Vector2(joystick.Horizontal, joystick.Vertical)
            });
        }

        private void HandleJoystickInput()
        {
            switch (_inputHandlers)
            {
                case InputHandlers.Character:
                    InputSignals.Instance.onInputTaken?.Invoke(new XZInputParams()
                    {
                        
                        XValue = joystick.Horizontal,
                        ZValue = joystick.Vertical
                        //InputValues = new Vector2(joystick.Horizontal, joystick.Vertical)
                    });
                    break;
                
                
                case InputHandlers.Turret when joystick.Vertical <= -0.6f:                                      
                    _inputHandlers = InputHandlers.Character;                                                 
                    InputSignals.Instance.onCharacterInputRelease?.Invoke();
                    return;
                
                case InputHandlers.Turret:
                    InputSignals.Instance.onJoystickInputDraggedforTurret?.Invoke(new XZInputParams()
                    {
                        XValue = joystick.Horizontal,
                        ZValue = joystick.Vertical
                        //InputValues = new Vector2(joystick.Horizontal,joystick.Vertical)
                    });
                    if (joystick.Direction.sqrMagnitude != 0)
                    {
                        InputSignals.Instance.onInputTaken?.Invoke(new XZInputParams()
                        {
                            XValue = 0,
                            ZValue = 0
                        });
                    }
                    break;
                
                case InputHandlers.Drone:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnInputHandlerChange(InputHandlers inputHandlers)
        {
            _inputHandlers = inputHandlers;
        }
        private void OnPlay() => isReadyForTouch = true;
        
        private void OnReset()
        {
            _isTouching = false;
            isReadyForTouch = false;
        }
        
    }
}

