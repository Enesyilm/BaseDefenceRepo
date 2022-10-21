using System;
using Cinemachine;
using Signals;
using UnityEngine;

namespace Managers.CameraManager
{
    public class CameraManager : MonoBehaviour
    {        
        #region Self Variables
     
        #region Public Variables
        
        #endregion
        #region Serialized Variables
        [SerializeField]
        CinemachineStateDrivenCamera stateDrivenCamera;
        [SerializeField]
        Animator animator;

        [SerializeField] private GameObject player;
        #endregion
        #region Private Variables
        #endregion
        #endregion

        private void Awake()
        {
            stateDrivenCamera.Follow = player.transform;
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onChangeCameraState+=OnGamePlayState;
        }
        
        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onChangeCameraState-=OnGamePlayState;
        }

        #endregion
        private void OnGamePlayState(CameraStates cameraStates )
        {
            animator.Play(cameraStates.ToString());
        }

    }
}