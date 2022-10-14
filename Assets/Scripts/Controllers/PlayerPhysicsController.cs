// using System;
// using DG.Tweening;
// using Enums;
// using Managers;
// using Signals;
// using UnityEngine;
//
// namespace Controllers
// {
//     public class PlayerPhysicsController1 : MonoBehaviour
//     {
//         #region Self Variables
//
//         #region Serialized Variables
//
//         [SerializeField] private PlayerManager playerManager;
//         // [SerializeField] private new Collider collider;
//         // [SerializeField] private new Rigidbody rigidbody;
//
//         #endregion
//
//         #region Private Variables
//
//         private int _frontyardLayer;
//         private int _baseLayer;
//
//         #endregion
//
//         #endregion
//
//         private void Awake()
//         {
//             GetLayerReferences();
//         }
//
//         private void GetLayerReferences()
//         {
//             _frontyardLayer = LayerMask.NameToLayer("Frontyard");
//             _baseLayer = LayerMask.NameToLayer("Base");
//         }
//
//         private void OnTriggerEnter(Collider other)
//         {
//             if (other.CompareTag("GateEnter"))
//             {
//                 gameObject.layer = _baseLayer;
//             }
//             if (other.CompareTag("MineEntrance"))
//             {
//                 playerManager.SendHostageToMineBase(other.transform.position);
//             }
//             if (other.CompareTag("MilitaryEntrance"))
//             {
//                 playerManager.SendHostageToMilitaryBase();
//             }
//             if (other.CompareTag("GateExit"))
//             {
//                 gameObject.layer = _frontyardLayer;
//             }
//             if (other.CompareTag("Turret"))
//             {
//
//                 playerManager.IsEnterTurret(other.GetComponentInParent<TurretManager>());
//
//
//             }
//         }
//         
//         public void SetPhysicsData()
//         {
//         }
//     }
// }