using Controllers;
using Data.UnityObjects;
using Data.UnityObjects;
using Data.ValueObjects;
using Data.ValueObjects.PlayerData;
using Enums;
using Keys;
using Signals;
using UnityEngine;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [Header("Data")] public PlayerData Data;

        #endregion

        #region Serialized Variables

        [Space] [SerializeField] private PlayerMovementController movementController;
        [SerializeField] private PlayerPhysicsController physicsController;
        //
        [SerializeField] private PlayerAnimationController animationController;
        // [SerializeField] private ForceBallsToPool poolForcer;

        #endregion

        #endregion


        private void Awake()
        {
            Data = GetPlayerData();
            SendPlayerDataToControllers();
        }

        private PlayerData GetPlayerData() => Resources.Load<CD_Player>("Data/CD_Player").PlayerData;

        private void SendPlayerDataToControllers()
        {
            movementController.SetMovementData(Data);
            physicsController.SetPhysicsData();
            //poolForcer.SetForceData(Data.ForceData);
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InputSignals.Instance.onInputTaken += OnActivateMovement;
            InputSignals.Instance.onInputReleased += OnDeactiveMovement;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
            CoreGameSignals.Instance.onLevelFailed += OnLevelFailed;
            CoreGameSignals.Instance.onStageAreaReached += OnStageAreaReached;
            CoreGameSignals.Instance.onStageSuccessful += OnStageSuccessful;
        }

        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onInputTaken -= OnActivateMovement;
            InputSignals.Instance.onInputReleased -= OnDeactiveMovement;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;
            CoreGameSignals.Instance.onLevelFailed -= OnLevelFailed;
            CoreGameSignals.Instance.onStageAreaReached -= OnStageAreaReached;
            CoreGameSignals.Instance.onStageSuccessful -= OnStageSuccessful;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        #region Movement Controller

        private void OnActivateMovement(XZInputParams _xzInputParams)
        {
            movementController.EnableMovement();
            OnGetInputValues(_xzInputParams);
        }

        private void OnDeactiveMovement()
        {
            movementController.DeactiveMovement();
        }

        private void OnGetInputValues(XZInputParams inputParams)
        {
            if (inputParams.XValue!=0 || inputParams.ZValue!=0)
            {
                ChangePlayerAnimation(PlayerAnimationTypes.Walk);       
            }
            else
            {
                ChangePlayerAnimation(PlayerAnimationTypes.Idle);     
            }
            movementController.UpdateInputValue(inputParams);
        }

        #endregion

        private void ChangePlayerAnimation(PlayerAnimationTypes _playerAnimationTypes)
        {
            animationController.PlayAnimation(_playerAnimationTypes);
        }

        private void OnPlay()
        {
            movementController.IsReadyToPlay(true);
        }

        private void OnLevelSuccessful()
        {
            movementController.IsReadyToPlay(false);
        }

        private void OnLevelFailed()
        {
            movementController.IsReadyToPlay(false);
        }

        private void OnStageSuccessful()
        {
            movementController.IsReadyToPlay(true);
        }

        private void OnStageAreaReached()
        {
            movementController.IsReadyToPlay(false);
        }

        private void OnReset()
        {
            movementController.OnReset();
        }
    }
}