using Controllers;
using Datas.UnityObject;
using Datas.ValueObject;
using Keys;
using Signals;
using UnityEngine;

namespace Managers
{
    public class TurretManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public bool IsUsingByPlayer=false; 
        

        #endregion
        #region SerializeField Variables
        [SerializeField]
        private TurretMovementController _movementController;
        [SerializeField]
        private TurretAutoAttackController _otoAtackController;
        [SerializeField]
        private TurretShootController ShootController;
        #endregion

        #region Private Variables
        private TurretData turretData;
        #endregion

        #endregion

        #region Get&SetData
        private void Awake() => Init();

        private void Init()
        {
            turretData = GetTurretData();
            SetMovementData();
            OtoAtackData();
            GattalingRotateData();
        }

        private TurretData GetTurretData() => Resources.Load<CD_Turret>("Data/CD_Turret").turretDatas;

        private void SetMovementData() => _movementController.SetMovementDatas(turretData.MovementDatas);

        private void OtoAtackData() => _otoAtackController.SetOtoAtackDatas(turretData.TurretOtoAtackDatas);

        private void GattalingRotateData() => ShootController.SetGattalingRotateDatas(turretData.gattalingRotateDatas);

        public GameObject GetGameObject() => gameObject;
        #endregion

        #region Event Subscription
        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            InputSignals.Instance.onInputTaken += OnGetInputValues;
            TurretSignals.Instance.onPressTurretButton += OnPressTurretButton;
            TurretSignals.Instance.onDeadEnemy += OnDeadEnemy;//Ä°nterfacele gelcek
        }

        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onInputTaken -= OnGetInputValues;
            TurretSignals.Instance.onPressTurretButton -= OnPressTurretButton;
            TurretSignals.Instance.onDeadEnemy -= OnDeadEnemy;

        }

        private void OnDisable() => UnsubscribeEvents();

        #endregion

        #region SubsciribeMethods
        public void OnPressTurretButton()
        {
            transform.GetChild(0).gameObject.SetActive(true);
            GetComponent<Collider>().enabled = false;
        }

        private void OnDeadEnemy() => IsEnemyExitTurretRange();

        #endregion

        #region BotController
        public void IsFollowEnemyInTurretRange()
        {
            ShootController.ActiveGattaling();
            //transform.GetComponentInChildren<AmmoContaynerManager>().IsTurretAttack();
            _otoAtackController.FollowToEnemy();
        }

        public void IsEnemyEnterTurretRange(GameObject enemy) => _otoAtackController.AddDeathList(enemy);
        public void IsEnemyExitTurretRange()
        {
            _otoAtackController.RemoveDeathList();
            ShootController.DeactiveGattaling();
        } 
        #endregion

        #region PlayerController

        private void OnGetInputValues(XZInputParams value)
        {
            if (!IsUsingByPlayer)
            {
                return;
            }
            else
            {
                Debug.Log("Çalıştı");
                _movementController.ActiveTurretWithPlayer(value);
            }
            
        }
        public void IsEnterUser() => IsUsingByPlayer=true;

        public void IsExitUser()
        {
            IsUsingByPlayer=false;
            _movementController.ResetTurret();
        }
        #endregion
    }
}