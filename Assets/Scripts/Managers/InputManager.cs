using System;
using System.Collections.Generic;
using Data.UnityObjects;
using Data.ValueObjects;
using Data.ValueObjects;
using Keys;
using Signals;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [Header("Data")] public InputData Data;

        #endregion

        #region Serialized Variables

        [SerializeField] private bool isReadyForTouch, isFirstTimeTouchTaken;
        [SerializeField] private FloatingJoystick floatingJoystick;

        #endregion

        #region Private Variables
        
        private Vector3 _inputPosition=Vector3.zero;

        #endregion

        #endregion


        private void Awake()
        {
            Data = GetInputData();
        }

        private InputData GetInputData() => Resources.Load<CD_Input>("Data/CD_Input").InputData;


        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onReset += OnReset;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onReset -= OnReset;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void Update()
        {
            TwoSidedMovement();
        }

        private void TwoSidedMovement()
        {
            float horizonDeltaInput = Math.Abs(floatingJoystick.Horizontal - _inputPosition.x);
            float verticalDeltaInput = Math.Abs(floatingJoystick.Vertical - _inputPosition.z);
            
            if (verticalDeltaInput>Data.InputPrecision||horizonDeltaInput>Data.InputPrecision)
            {
                _inputPosition = new Vector3(floatingJoystick.Horizontal,0,floatingJoystick.Vertical);
                     InputSignals.Instance.onInputTakenActive?.Invoke(true);
                InputSignals.Instance.onInputTaken?.Invoke(new XZInputParams(){
                    XValue = _inputPosition.x,
                    ZValue = _inputPosition.z
                    
                });
                if (!isFirstTimeTouchTaken)
                {
                    InputSignals.Instance.onFirstTimeTouchTaken?.Invoke();
                }
            }
            else if(floatingJoystick.Vertical==0||floatingJoystick.Horizontal==0)
            {
                InputSignals.Instance.onInputTakenActive?.Invoke(false);
            }
        }
        private void OnReset()
        {
            isFirstTimeTouchTaken = false;
        }
    }
}