// using AI;
// using Managers;
// using System.Collections;
// using System.Threading.Tasks;
// using AIBrain;
// using UnityEngine;
//
// namespace Controllers
// {
//     public class AmmoWorkerPhysicsController : MonoBehaviour
//     {
//
//         private AmmoManager _ammoManager;
//
//         private float _timer = 0.4f;
//
//         private void OnTriggerEnter(Collider other)
//         {
//
//             if (other.TryGetComponent(typeof(AmmoManagerPhysicsController), out Component ammoManagment))//it must change
//             {
//                 _ammoManager = other.gameObject.GetComponent<AmmoManager>();
//
//                 _ammoManager.IsAmmoEnterAmmoWareHouse(transform.parent.GetComponent<AmmoWorkerAIBrain>());
//              
//                 _ammoManager.IsSetTargetTurretContayner(transform.parent.GetComponent<AmmoWorkerAIBrain>());
//             }
//             if (other.TryGetComponent(typeof(TurretStackPhysicsControl), out Component ammoContayenr))//it must change
//             {
//                _ammoManager.IsEnterTurretStack(transform.parent.GetComponent<AmmoWorkerAIBrain>());
//             }
//         }
//
//
//         private void OnTriggerExit(Collider other)
//         {
//            
//             if (other.TryGetComponent(typeof(AmmoManagerPhysicsController), out Component ammoManagment))//it must change
//             {
//                 _ammoManager = other.gameObject.GetComponent<AmmoManager>();
//
//                 _ammoManager.IsAmmoExitAmmoWareHouse(transform.parent.GetComponent<AmmoWorkerAIBrain>());
//
//                 _ammoManager.ResetItems();
//             }
//             if (other.TryGetComponent(typeof(TurretStackPhysicsControl), out Component ammoContayenr))//it must change
//             {
//                 _ammoManager.IsExitTurretStack(transform.parent.GetComponent<AmmoWorkerAIBrain>());
//
//                 _ammoManager.IsExitOnTurretStack(transform.parent.GetComponent<AmmoWorkerStackController>());
//             }
//         }
//
//         private void OnTriggerStay(Collider other)
//         {
//             
//             if (other.TryGetComponent(typeof(AmmoManagerPhysicsController), out Component ammoManagment))//it must change
//             {
//                 _ammoManager = other.gameObject.GetComponent<AmmoManager>();
//
//                 _timer -= Time.deltaTime;
//
//                 if (_timer < 0)
//                 {
//                     _timer = 0.1f;
//            
//                     _ammoManager.IsStayOnAmmoWareHouse(transform.parent.GetComponent<AmmoWorkerAIBrain>(),
//                                                         transform.parent.GetComponent<AmmoWorkerStackController>());
//                 }
//             }
//
//             if (other.TryGetComponent(typeof(TurretStackPhysicsControl), out Component ammoContayenr))//it must change
//             {
//                 _ammoManager.IsAmmoWorkerStackEmpty(transform.parent.GetComponent<AmmoWorkerAIBrain>());
//             }
//
//         }
//
//
//
//     }
// }