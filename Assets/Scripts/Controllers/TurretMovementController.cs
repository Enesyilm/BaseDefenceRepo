using Keys;
using UnityEngine;
using System;
using Enums;
using System.Collections.Generic;
using DG.Tweening;
using Datas.ValueObject;

namespace Controllers
{

    public class TurretMovementController : MonoBehaviour
    {
        private float _horizontalInput;
        private float _verticalInput;
        private TurretMovementData _movementDatas;



        public void SetMovementDatas(TurretMovementData movementDatas) => _movementDatas = movementDatas;

        public void SetInputParams(XZInputParams input)//it can be turn on interface
        {
            _horizontalInput = input.XValue;
            _verticalInput = input.ZValue;
          
        }

        private void EnterToTaret()//It can be abstract
        {
            if ((-0.9f < _horizontalInput && _verticalInput > 0.3f) && (+0.9f > _horizontalInput && _verticalInput > 0.3f))
            {
                Rotate();
            }
           
        }
        private void Rotate()
        {
            Vector3 _movementDirection = new Vector3(_horizontalInput, 0, _verticalInput);
            if (_movementDirection == Vector3.zero) return;
            Quaternion _toRotation = Quaternion.LookRotation(_movementDirection, Vector3.up);
            transform.parent.rotation = Quaternion.RotateTowards(transform.parent.rotation, _toRotation, _movementDatas.TurretTurnSpeed);
        }

        public void ActiveTurretWithPlayer(XZInputParams input)
        {
            SetInputParams(input);
            
            EnterToTaret();
        }

        public void ResetTurret()
        {
            transform.parent.rotation = new Quaternion(0, 0, 0, 0);

        }
    }
}