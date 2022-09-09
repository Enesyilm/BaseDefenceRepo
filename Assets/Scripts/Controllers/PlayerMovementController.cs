using System;
using Data.ValueObjects;
using Data.ValueObjects.PlayerData;
using Keys;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField] private PlayerManager manager;
        [SerializeField] private Transform playerMesh;
        [SerializeField] private new Rigidbody rigidbody;

        #endregion

        [Header("Data")] private PlayerData _movementData;
         private bool _isReadyToMove, _isReadyToPlay;
         private float _xinputValue;
         private float _zinputValue;
         private Vector2 _clampValues;

        #endregion

        public void SetMovementData(PlayerData dataMovementData)
        {
            _movementData = dataMovementData;
        }

        public void EnableMovement()
        {
            _isReadyToMove = true;
        }

        public void DeactiveMovement()
        {
            _isReadyToMove = false;
        }

        public void UpdateInputValue(XZInputParams inputParam)
        {
            _xinputValue = inputParam.XValue;
            _zinputValue = inputParam.ZValue;
        }

        public void IsReadyToPlay(bool state)
        {
            _isReadyToPlay = state;
        }

        private void FixedUpdate()
        {
            if (_isReadyToMove)
            {
                Move();
            }
            else
            {
                Stop();
            }
        }

        private void Move()
        {
            var velocity = rigidbody.velocity;
            velocity = new Vector3(-_xinputValue * _movementData.PlayerSpeed, velocity.y,
                -_zinputValue*_movementData.PlayerSpeed);
            rigidbody.velocity = velocity;
            if (velocity != Vector3.zero) {
                PlayerRotation(velocity);
            }
        }

        private void PlayerRotation(Vector3 velocity)
        {
            Quaternion toRotation = Quaternion.LookRotation(new Vector3(-_xinputValue * _movementData.PlayerSpeed, velocity.y,
                -_zinputValue * _movementData.PlayerSpeed));
            playerMesh.rotation = toRotation;
            
        }

        private void Stop()
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }

        public void OnReset()
        {
            Stop();
            _isReadyToPlay = false;
            _isReadyToMove = false;
        }
    }
}