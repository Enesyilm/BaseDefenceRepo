using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using Controllers.Player;
using Data.UnityObjects;
using Data.ValueObjects.PlayerData;
using Data.ValueObjects.WeaponData;
using Datas.ValueObject;
using DG.Tweening;
using Enum;
using Keys;
using Managers.CoreGameManagers;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using Photon.Pun;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        public bool IsPlayerDeath;
        public AreaType CurrentAreaType = AreaType.BaseDefense;
        
        public WeaponTypes WeaponType;
        
        public List<IDamageable> EnemyList = new List<IDamageable>();
        
        public Transform EnemyTarget;
        
        public IDamageable Damageable;

        #endregion

        #region Serialized Variables
        public PlayerType playerType;
        [SerializeField] 
        private PlayerMeshController meshController;
        [SerializeField] 
        private PlayerAnimationController animationController;
        [SerializeField] 
        private PlayerWeaponController weaponController;
        [SerializeField] 
        private PlayerShootingController shootingController;
        [SerializeField]
        private PlayerMovementController movementController;
        [SerializeField]
        private PlayerPhysicsController physicsController;
        [SerializeField]
        private PlayerHealthController healthController;
        [SerializeField]
        private PlayerCollectorController collectorController;
        [SerializeField] MoneyStackerController playerMoneyStackerController;
        #endregion

        #region Private Variables
        
        private PlayerData _data;
        private PlayerMovementData _movementData;

        private WeaponData _weaponData;
        private PhotonView pw;
        
        #endregion
        
        #endregion
        private void Awake()
        {
            _data = GetPlayerData();
            _movementData=GetPlayerMovementData();
            _weaponData = GetWeaponData();
          
            Init();
        }

        private void Start()
        {
            pw = GetComponent<PhotonView>();
        }

        private PlayerData GetPlayerData() => Resources.Load<CD_Player>("Data/CD_Player").PlayerData;
        private PlayerMovementData GetPlayerMovementData() => Resources.Load<CD_Player>("Data/CD_Player").PlayerMovementData;
        private WeaponData GetWeaponData() => Resources.Load<CD_Weapon>("Data/CD_Weapon").WeaponData[(int)WeaponType];
        private void Init() => SetDataToControllers();
        private void SetDataToControllers()
        {
            movementController.SetMovementData(_movementData);
            weaponController.SetWeaponData(_weaponData);
            meshController.SetWeaponData(_weaponData);
            healthController.SetHealthData(_data);
        }
        #region Event Subscription
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            InputSignals.Instance.onInputTaken += OnGetInputValues;
            InputSignals.Instance.onInputTakenWasd += OnWASDInputValues;
            InputSignals.Instance.onInputHandlerChange += OnDisableMovement;
            UISignals.Instance.onChangeWeapon+=OnChangeWeapon;
        }

        
        [Button]
        private void OnChangeWeapon(WeaponTypes arg0)
        {
            WeaponType = WeaponTypes.PumpBullet;
            _weaponData=GetWeaponData();
            weaponController.SetWeaponData(_weaponData);
            meshController.SetWeaponData(_weaponData);
        }

        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onInputTakenWasd -= OnWASDInputValues;
            InputSignals.Instance.onInputTaken -= OnGetInputValues;
            InputSignals.Instance.onInputHandlerChange -= OnDisableMovement;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
        private void OnGetInputValues(XZInputParams inputParams)
        {
            if (playerType == PlayerType.joyStick)
            {
                Debug.Log("joy");
                movementController.UpdateInputValues(inputParams);
                animationController.PlayAnimation(inputParams);
            }
            //AimEnemy();
        }
        private void OnWASDInputValues(XZInputParams inputParams)
        {
            if (playerType == PlayerType.wasd)
            {
                Debug.Log("was");
                Debug.Log("was"+inputParams.XValue);
                movementController.UpdateInputValues(inputParams);
                animationController.PlayAnimation(inputParams);
            }
            
        }
        public void SetEnemyTarget()
        {
            shootingController.SetEnemyTargetTransform();
            animationController.AimTarget(true);
            //AimEnemy();
        }
        //private void AimEnemy() => movementController.LookAtTarget(!EnemyTarget ? null : EnemyList[0]?.GetTransform());
        public void CheckAreaStatus(AreaType areaType) => meshController.ChangeAreaStatus(CurrentAreaType = areaType);
        private void OnDisableMovement(InputHandlers inputHandler) => movementController.DisableMovement(inputHandler);
        public void SetTurretAnimation(bool onTurret) => animationController.PlayTurretAnimation(onTurret);
        public void OnUpdateHealth(ScoreTypes scoreType,int amount) => healthController.UpdateHealthAmount(scoreType,amount);

        public void PlayerDeath()
        {
            animationController.ChangeAnimations(PlayerAnimationStates.Death);
            movementController.DisableMovement(InputHandlers.Turret);
            movementController.PlayerDeath();
            meshController.ChangeAreaStatus(AreaType.BaseDefense);
            collectorController.collider.enabled=false;
            playerMoneyStackerController.ResetStack();
            CheckAreaStatus(AreaType.BaseDefense);
            CoreGameSignals.Instance.onResetPlayerStack.Invoke();
            EnemyTarget = null;
            EnemyList.Clear();
            UISignals.Instance.onHealthBarVisibility?.Invoke(false);
            //meshController.ChangeAreaStatus(AreaType.BaseDefense);
            physicsController.ResetPlayerLayer();
            DOVirtual.DelayedCall(3f, () =>
            {
                collectorController.collider.enabled=true;
                EnemyTarget = null;
                IsPlayerDeath = false;
                healthController.UpdateHealthAmount(ScoreTypes.IncScore,_data.PlayerHealth);
                transform.position = Vector3.forward*3;
                Debug.Log("Delayed call");
                animationController.ChangeAnimations(PlayerAnimationStates.Idle);
                
            });
            //CoreGameSignals.Instance.onReset?.Invoke();
            
        }

        public void ChangeAnimation(PlayerAnimationStates playerAnimationStates)
        {
            animationController.ChangeAnimations(playerAnimationStates);
        }
        

        public IEnumerator StartHealing()
        {
            Debug.Log("StartHealting");
            
            yield return new WaitForSeconds(_data.PlayerHealingOffset);
            if (healthController.Health >=100||LayerMask.NameToLayer("Frontyard")==gameObject.layer) yield break;
            StartCoroutine(StartHealing());
            OnUpdateHealth(ScoreTypes.IncScore,_data.PlayerHealingRate);
        }
        public void SendHostageToMineBase(Vector3 centerOfGatePo)
        {
            HostageSignals.Instance.onSendHostageToMineBase.Invoke(centerOfGatePo);
        }
        public void SendHostageToMilitaryBase()
         {
             HostageSignals.Instance.onSendHostageStackToMilitaryBase?.Invoke();
         }

        public void SetLayer()
        {
            
        }

        
    }
}