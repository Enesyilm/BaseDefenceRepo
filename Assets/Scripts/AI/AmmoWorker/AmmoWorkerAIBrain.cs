// using Managers;
// using States;
// using System;
// using AI;
// using AIBrain.States;
// using Data.UnityObjects;
// using Data.ValueObjects.AiData;
// using Enum;
// using UnityEditorInternal;
// using UnityEngine;
// using UnityEngine.AI;
// using StateMachine = AI.StateMachine;
//
// namespace AIBrain
// {
//     public class AmmoWorkerAIBrain : MonoBehaviour
//     {
//         #region Self Variables
//
//         #region SerilizeField Variables
//
//         [SerializeField]
//         private NavMeshAgent _agent;
//         [SerializeField]
//         private Animator _animator;
//         [SerializeField]
//         private AmmoManager ammoManager;
//         #endregion
//
//         #region Private Variables 
//
//         int counter;
//         [SerializeField]
//         private bool  _inplaceWorker;
//         private bool _isLoadTurretContayner;
//         private GameObject _targetTurretContayner;
//
//  
//         #endregion
//
//         #region State Instance
//
//         private MoveToWarehouse _moveToWareHouse;
//         private TakeAmmo _takeAmmo;
//
//         private MoveToAvailableContainer _moveToAvaliableConteyner;
//
//         private AmmoWorkerStackStatus _playerAmmaStackStatus;
//
//         private LoadContainer _loadTurret;
//         private ReadyState _readyState;
//         private AmmoWorkerAIData _ammoWorkerAIData;
//
//
//        
//
//         private StateMachine _statemachine;
//
//         #endregion
//         #endregion
//
//         #region GetReferans
//         private void Awake()
//         {
//             InitBrain();
//         }
//         public void InitBrain()
//         {
//             _ammoWorkerAIData = Resources.Load<CD_AI>("Data/CD_AI").AmmoWorkerAIData;
//             GetStatesReferences();
//             TransitionofState();
//         }
//         public void IsStackFul(AmmoWorkerStackStatus status) => _playerAmmaStackStatus = status;
//
//         public void SetTriggerInfo(bool IsInPlaceWareHouse) => _inplaceWorker = IsInPlaceWareHouse;
//
//
//         internal void IsLoadTurret(bool isLoadTurretContayner)
//         {
//             _isLoadTurretContayner = isLoadTurretContayner;
//         }
//
//         public void SetTargetTurretContayner(GameObject targetTurretContayner)
//         {
//             
//
//             _moveToAvaliableConteyner.SetData(targetTurretContayner);
//             _targetTurretContayner = targetTurretContayner;
//         }
//
//         internal  void GetStatesReferences()
//         {
//             _statemachine = new StateMachine();
//
//             _readyState = new ReadyState();
//
//             _moveToWareHouse = new MoveToWarehouse(_agent, _animator, _ammoWorkerAIData.MovementSpeed, 
//                                                     _ammoWorkerAIData.AmmoWareHouse, _ammoWorkerAIData.AmmoWorker,this);
//
//             _takeAmmo = new TakeAmmo(_agent,_animator);
//
//             _moveToAvaliableConteyner = new MoveToAvailableContainer(_agent, _animator, _ammoWorkerAIData.MovementSpeed);
//
//             _loadTurret = new LoadContainer(_agent, _animator, _ammoWorkerAIData.MovementSpeed, _ammoWorkerAIData.AmmoWareHouse);
//
//             //_fullAmmo = new FullAmmo(_agent, _animator, _ammoWorkerAIData.MovementSpeed);
//
//         }
//
//     
//
//         #endregion
//
//         #region StateEngine
//
//         internal  void TransitionofState()
//         {
//    
//
//             #region Transtion
//
//             At(_readyState, _moveToWareHouse, IsAmmoWorkerBorn());
//
//             At(_moveToWareHouse, _takeAmmo, WhenAmmoWorkerInAmmoWareHouse());
//
//             At(_takeAmmo, _moveToAvaliableConteyner,WhenAmmoWorkerStackFull());
//
//             At(_moveToAvaliableConteyner, _loadTurret, IsAmmoWorkerInContayner());
//
//             At(_loadTurret, _moveToWareHouse, WhenAmmoDichargeStack());
//
//             //if (_playerAmmaStackStatus == PlayerAmmaStackStatus.Full)
//             //{
//             //    _statemachine.AddAnyTransition(_fullAmmo, HasNoEmtyTarget());//bak buna 
//             //}
//
//             _statemachine.SetState(_readyState);
//
//             void At(IState to, IState from, Func<bool> condition) => _statemachine.AddTransition(to, from, condition);
//
//             #endregion
//
//             #region Conditions
//
//             Func<bool> IsAmmoWorkerBorn() => () => _ammoWorkerAIData.AmmoWareHouse.transform != null;
//
//             Func<bool> WhenAmmoWorkerInAmmoWareHouse() => () => _inplaceWorker == true && _ammoWorkerAIData.AmmoWareHouse.transform != null;
//
//             Func<bool> WhenAmmoWorkerStackFull() => () => _targetTurretContayner != null && _playerAmmaStackStatus == AmmoWorkerStackStatus.Full;
//
//             Func<bool> IsAmmoWorkerInContayner() => () => _targetTurretContayner != null && _isLoadTurretContayner==true;
//
//             Func<bool> WhenAmmoDichargeStack() => () =>  _playerAmmaStackStatus == AmmoWorkerStackStatus.Empty;
//
//             //Func<bool> HasNoEmtyTarget() => () => _targetTurretContayner == null;
//
//             #endregion
//         }
//
//         public void Update()
//         {
//
//             _statemachine.Tick();
//         }
//         #endregion
//
//
//
//     }
// }