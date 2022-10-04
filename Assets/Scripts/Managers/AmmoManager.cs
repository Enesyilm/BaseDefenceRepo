// using AI;
// using Controllers;
// using Data.UnityObjects;
// using Datas.ValueObject;
// using Enums;
// using Interfaces;
// using Signals;
// using AIBrain;
// using Data.ValueObjects.AiData;
// using Enum;
// using FrameworkGoat;
// using UnityEngine;
//
// namespace Managers
// {
//     
//
//     public class AmmoManager : MonoBehaviour
//     {
//         #region Self-Private Variabels
//         [SerializeField]
//         private CD_AI cD_AI;
//
//         private int counter;
//
//         private AmmoWorkerAIData _ammoWorkerAIData;
//
//         private GameObject _targetStack;
//
//         private AmmoWorkerStackStatus _playerAmmoStackStatus;
//         #endregion
//         internal void Awake() => Init();
//
//         private void Init() => _ammoWorkerAIData = cD_AI.AmmoWorkerAIData;
//
//         #region Event Subscription
//         private void OnEnable() => SubscribeEvents();
//         private void SubscribeEvents()
//         {
//             AmmoManagerSignals.Instance.onSetConteynerList += OnSetConteynerList;   
//             AmmoManagerSignals.Instance.onPlayerEnterAmmoWorkerCreaterArea += OnPlayerEnterAmmoWorkerCreaterArea;
//             
//         }
//         private void UnsubscribeEvents()
//         {
//             AmmoManagerSignals.Instance.onSetConteynerList -= OnSetConteynerList;
//             AmmoManagerSignals.Instance.onPlayerEnterAmmoWorkerCreaterArea -= OnPlayerEnterAmmoWorkerCreaterArea;
//         }
//         private void OnDisable() => UnsubscribeEvents();
//
//         #endregion
//
//         public void IsExitOnTurretStack(AmmoWorkerStackController ammoWorkerStackController) => ammoWorkerStackController.SetClearWorkerStackList();
//
//         internal void IsAmmoWorkerStackEmpty(AmmoWorkerAIBrain ammoWorkerBrain) => ammoWorkerBrain.IsStackFul(_playerAmmoStackStatus);
//
//         internal void IsSetTargetTurretContayner(AmmoWorkerAIBrain ammoWorkerBrain) => ammoWorkerBrain.SetTargetTurretContayner(_targetStack);
//
//         internal void IsEnterTurretStack(AmmoWorkerAIBrain ammoWorkerBrain) => ammoWorkerBrain.IsLoadTurret(true);
//
//         internal void IsExitTurretStack(AmmoWorkerAIBrain ammoWorkerBrain) => ammoWorkerBrain.IsLoadTurret(false);
//
//         internal void IsAmmoEnterAmmoWareHouse(AmmoWorkerAIBrain ammoWorkerBrain) => ammoWorkerBrain.SetTriggerInfo(true);
//
//         internal void IsAmmoExitAmmoWareHouse(AmmoWorkerAIBrain ammoWorkerBrain) => ammoWorkerBrain.SetTriggerInfo(false);
//
//         internal void IsStayOnAmmoWareHouse(AmmoWorkerAIBrain ammoWorkerBrain,AmmoWorkerStackController ammoWorkerStackController)
//         {           
//             
//             if (counter < _ammoWorkerAIData.MaxStackCount)
//             {
//                
//                 ammoWorkerBrain.IsStackFul(AmmoWorkerStackStatus.Empty);
//
//                 ammoWorkerStackController.AddStack(_ammoWorkerAIData.AmmoWareHouse, ammoWorkerBrain.gameObject.transform, GetObject(PoolType.Ammo.ToString()));
//
//                 counter++;
//             }
//             else
//             {
//                 ammoWorkerBrain.IsStackFul(AmmoWorkerStackStatus.Full);
//             }
//
//         }
//         private void OnSetConteynerList(GameObject targetStack)
//         {   
//             _targetStack = targetStack;
//             _playerAmmoStackStatus = AmmoWorkerStackStatus.Empty;
//
//         }
//
//         private void OnPlayerEnterAmmoWorkerCreaterArea(Transform workerCreater) => AddAmmaWorker(workerCreater);
//
//         public GameObject GetObject(string poolName) => ObjectPoolManager.Instance.GetObject<GameObject>(poolName);
//
//         public void AddAmmaWorker(Transform workerCreater)
//         {
//             GameObject ammoWorker = GetObject(PoolObjectType.AmmoWorkerAI.ToString());
//
//             ammoWorker.transform.position = workerCreater.position;
//            
//         }
//       
//         public void ResetItems() => counter = 0;
//
//
//
//
//         #region Subscirabe Event methods
//
//         #endregion
//
//         #region Physics Methods
//
//         #endregion
//
//         #region SendInfo
//
//         #endregion
//
//
//     }
// }