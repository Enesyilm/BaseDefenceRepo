using System.Collections.Generic;
using Controllers;
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

        public IDamageable DamageableEnemy;
        public AreaType currentAreaType = AreaType.BaseDefense;
        
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
        #endregion

        #region Private Variables
        
        private PlayerData _data;

        private WeaponData _weaponData;

        private AreaType _nextState = AreaType.BattleOn;
         private PlayerMovementData _movementData;

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
        private WeaponData GetWeaponData() => Resources.Load<CD_Weapon>("Data/CD_Weapon").WeaponData[(int)WeaponType];
        private PlayerMovementData GetPlayerMovementData() => Resources.Load<CD_Player>("Data/CD_Player").PlayerMovementData;
        private void Init()
        {
            currentAreaType = AreaType.BaseDefense;
            SetDataToControllers();
        }
        private void SetDataToControllers()
        {
            movementController.SetMovementData(_movementData);
            weaponController.SetWeaponData(_weaponData);
            meshController.SetWeaponData(_weaponData);
        }
        #region Event Subscription
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            InputSignals.Instance.onInputTaken += OnGetInputValues;
        }
        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onInputTaken -= OnGetInputValues;
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
            if (!HasEnemyTarget) return;
            AimEnemy();
        }
        public void CheckAreaStatus(AreaType AreaStatus)
        {
            currentAreaType = AreaStatus;
            meshController.ChangeAreaStatus(AreaStatus);
        }
        public void SetEnemyTarget()
        {
            shootingController.SetEnemyTargetTransform();
            animationController.AimTarget(true);
            AimEnemy();
        }
        private void AimEnemy()
        { 
            if (EnemyList.Count != 0)
            {
                var transformEnemy = EnemyList[0].GetTransform();
                movementController.RotateThePlayer(transformEnemy);
            }
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