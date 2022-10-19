using System.Collections;
using System.Collections.Generic;
using Controllers;
using Controllers.Player;
using Data.UnityObjects;
using Data.ValueObjects.PlayerData;
using Data.ValueObjects.WeaponData;
using Datas.ValueObject;
using Enum;
using Keys;
using Signals;
using UnityEngine;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public AreaType CurrentAreaType = AreaType.BaseDefense;
        
        public WeaponTypes WeaponType;
        
        public List<IDamageable> EnemyList = new List<IDamageable>();
        
        public Transform EnemyTarget;
        
        public bool HasEnemyTarget = false;

        #endregion

        #region Serialized Variables

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
        private PlayerHealthController healthController;
        #endregion

        #region Private Variables
        
        private PlayerData _data;
        private PlayerMovementData _movementData;

        private WeaponData _weaponData;
        
        #endregion
        
        #endregion
        private void Awake()
        {
            _data = GetPlayerData();
            _movementData=GetPlayerMovementData();
            _weaponData = GetWeaponData();
          
            Init();
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
            InputSignals.Instance.onInputHandlerChange += OnDisableMovement;
        }
        private void UnsubscribeEvents()
        {
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
            movementController.UpdateInputValues(inputParams);
            animationController.PlayAnimation(inputParams);
            AimEnemy();
        }
        public void SetEnemyTarget()
        {
            shootingController.SetEnemyTargetTransform();
            animationController.AimTarget(true);
            AimEnemy();
        }
        private void AimEnemy() => movementController.LookAtTarget(!HasEnemyTarget ? null : EnemyList[0]?.GetTransform());
        public void CheckAreaStatus(AreaType areaType) => meshController.ChangeAreaStatus(CurrentAreaType = areaType);
        private void OnDisableMovement(InputHandlers inputHandler) => movementController.DisableMovement(inputHandler);
        public void SetTurretAnimation(bool onTurret) => animationController.PlayTurretAnimation(onTurret);
        public void OnUpdateHealth(ScoreTypes scoreType,int amount) => healthController.UpdateHealth(scoreType,amount);

        public void PlayerDeath()
        {
            animationController.ChangeAnimations(PlayerAnimationStates.Death);
            movementController.DisableMovement(InputHandlers.Turret);
            movementController.PlayerDeath();
            HasEnemyTarget = false;
            EnemyList.Clear();
            //CurrentAreaType = AreaType.BaseDefense;
            meshController.ChangeAreaStatus(AreaType.BaseDefense );
            
        }

        public IEnumerator StartHealing()
        {
            Debug.Log("StartHealting");
            
            yield return new WaitForSeconds(_data.PlayerHealingOffset);
            if (healthController.Health >=100&&LayerMask.NameToLayer("Frontyard")==gameObject.layer) yield break;
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

        
    }
}