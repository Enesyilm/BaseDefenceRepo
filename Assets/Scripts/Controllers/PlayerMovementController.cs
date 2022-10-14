// using System;
// using Data.ValueObjects;
// using Data.ValueObjects.PlayerData;
// using Datas.ValueObject;
// using Enum;
// using Keys;
// using Managers;
// using UnityEngine;
//
// namespace Controllers
// {
//     public class PlayerMovementController1 : MonoBehaviour
//     {
//         #region Self Variables
//
//         #region Public Variables
//
//         #endregion
//
//         #region Serialized Variables
//
//         [SerializeField] private PlayerManager manager;
//         [SerializeField] private Transform playerMesh;
//         [SerializeField] private new Rigidbody rigidbody;
//
//         #endregion
//
//         [Header("Data")] private PlayerMovementData _movementData;
//          private bool _isReadyToMove, _isReadyToPlay;
//          private float _xinputValue;
//          private float _zinputValue;
//          private Vector2 _clampValues;
//          private GameObject _currentParent;
//          private TurretStatus _turretStatus;
//
//         #endregion
//
//         public void SetMovementData(PlayerMovementData dataMovementData)
//         {
//             _movementData = dataMovementData;
//             
//         }
//
//         private void Awake()
//         {
//             SetCurrentParrent();
//         }
//
//         public void EnableMovement()
//         {
//             _isReadyToMove = true;
//         }
//
//         public void DeactiveMovement()
//         {
//             _isReadyToMove = false;
//         }
//
//         public void UpdateInputValue(XZInputParams inputParam)
//         {
//             
//             _xinputValue = inputParam.XValue;
//             _zinputValue = inputParam.ZValue;
//         }
//
//         public void IsReadyToPlay(bool state)
//         {
//             _isReadyToPlay = state;
//         }
//
//         private void FixedUpdate()
//         {
//             //DecideMovementState();
//             if (_isReadyToMove)
//             {
//                 if (_turretStatus == TurretStatus.Inplace) 
//                 {
//                     rigidbody.velocity = Vector3.zero;
//                     transform.localRotation = new Quaternion(0, -180, 0, 0); 
//
//                     return; 
//                 }
//                 Move();
//             }
//             else
//             {
//                 Stop();
//             }
//         }
//
//         // private void DecideMovementState()
//         // {
//         //     if (_isReadyToMove)
//         //     {
//         //         if (_turretStatus == TurretStatus.Inplace) 
//         //         {
//         //             rigidbody.velocity = Vector3.zero; 
//         //
//         //             transform.rotation = new Quaternion(0, 0, 0, 0); 
//         //
//         //             return; 
//         //         }
//         //
//         //         Move();
//         //         Rotate();
//         //
//         //     }
//         //     else if (rigidbody.velocity != Vector3.zero)
//         //     {
//         // }
//
//         private void Move()
//         {
//             var velocity = rigidbody.velocity;
//             velocity = new Vector3(-_xinputValue * _movementData.Speed, velocity.y,
//                 -_zinputValue*_movementData.Speed);
//             rigidbody.velocity = velocity;
//             if (velocity != Vector3.zero) {
//                 PlayerRotation(velocity);
//             }
//             
//         }
//
//         private void PlayerRotation(Vector3 velocity)
//         {
//             Quaternion toRotation = Quaternion.LookRotation(new Vector3(-_xinputValue * _movementData.Speed, velocity.y,
//                 -_zinputValue * _movementData.Speed));
//             manager.transform.rotation = toRotation;
//             
//         }
//
//         private void Stop()
//         {
//             rigidbody.velocity = Vector3.zero;
//             rigidbody.angularVelocity = Vector3.zero;
//         }
//
//         public void OnReset()
//         {
//             Stop();
//             _isReadyToPlay = false;
//             _isReadyToMove = false;
//         }
//
//         public void EnterToTurret(GameObject turretObj)
//                 {
//                     Vector3 turretPos = turretObj.transform.position;
//         
//                     transform.position = new Vector3(turretPos.x, transform.position.y, turretPos.z + 2f);
//                     transform.parent = turretObj.transform;
//                     //transform.rotation =Quaternion.LookRotation(Vector3.back);
//                     //TurretManager ÜZerinde IsUsingByPlayer trueya cekilmeli veya movement player içindeki yapı değişmeli
//                     _turretStatus = TurretStatus.Inplace;
//                 }
//         
//                 public void CheckIfPlayerExitFromTurret()//We exit as joystick İnput
//                 {
//                     if ((_movementData.ExitClampLeftSide < _xinputValue && _movementData.ExitClampBackSide > _zinputValue) && (_movementData.ExitClampRightSide > _xinputValue && _movementData.ExitClampBackSide >_zinputValue))
//                     {
//                         transform.parent = _currentParent.transform;
//                         manager.ExitFromTurret();
//                         _turretStatus = TurretStatus.OutPlace;
//                     }
//                 }
//                 private void SetCurrentParrent()
//                 {
//                     _currentParent = transform.parent.gameObject;
//                 }
//     }
// }