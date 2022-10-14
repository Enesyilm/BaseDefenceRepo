using Data.ValueObject;
using Datas.ValueObject;
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

        [SerializeField] private new Rigidbody rigidbody;
        [SerializeField] private PlayerManager manager;
        #endregion

        #region Private Variables
        
        private PlayerMovementData _data;

        private Vector2 _inputVector;

        private bool _isReadyToMove;
        
        #endregion

        #endregion
        public void SetMovementData(PlayerMovementData movementData)
        {
            _data = movementData;
        }
        public void UpdateInputValues(XZInputParams inputParams)
        {
            _inputVector = new Vector2(inputParams.XValue,inputParams.ZValue);
            EnableMovement(_inputVector.sqrMagnitude > 0);
            if (manager.HasEnemyTarget) return;
            RotatePlayer(inputParams);
           
        }
        private void RotatePlayer(XZInputParams inputParams)
        {
            Vector3 movementDirection = new Vector3(-inputParams.XValue, 0, -inputParams.ZValue);
            if (movementDirection == Vector3.zero) return;
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 30);
        } 
        public void RotateThePlayer(Transform enemyTransform)
        {
            transform.LookAt(enemyTransform, Vector3.up*3f);
        }
        private void EnableMovement(bool movementStatus)
        {
            _isReadyToMove = movementStatus;
        }
        private void FixedUpdate()
        {
            PlayerMove();
        }
        private void PlayerMove()
        {
            if (_isReadyToMove)
            {
                var velocity = rigidbody.velocity; 
                velocity = new Vector3(-_inputVector.x,velocity.y, -_inputVector.y)*_data.Speed;
                rigidbody.velocity = velocity;
            }
            else if(rigidbody.velocity != Vector3.zero)
            {
                rigidbody.velocity = Vector3.zero;
            }
        }
    }
}