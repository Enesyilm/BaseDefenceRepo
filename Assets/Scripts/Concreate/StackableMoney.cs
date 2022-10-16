using System;
using Abstracts;
using DG.Tweening;
using Signals;
using UnityEngine;

namespace Controllers.StackableControllers
{
    public class StackableMoney : AStackable
    {
        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] private BoxCollider collider;
        public override GameObject SendToStack(Transform transform1)
        {
            throw new NotImplementedException();
        }

        public override void SendPosition(Transform transform)
        {
            base.SendPosition(transform);
        }
        private void OnEnable()
        {
            SendPosition(this.transform);
        }

        public override void SetInit(Transform initTransform, Vector3 position)
        {
            base.SetInit(initTransform, position);
        }

        public override void SetVibration(bool isVibrate)
        {
            base.SetVibration(isVibrate);
        }

        public override void SetSound()
        {
            base.SetSound();
        }

        public override void EmitParticle()
        {
            base.EmitParticle();
        }

        public override void PlayAnimation()
        {
            base.PlayAnimation();
        }

        public GameObject SendToStack()
        {
            //transform.localRotation = new Quaternion(0, 0, 0, 1);
            rigidbody.useGravity = false;
            rigidbody.isKinematic = true;
            collider.enabled = false;
            return transform.gameObject;
        }

        private void EditPhysics()
        {
            rigidbody.useGravity = false;
            rigidbody.isKinematic = true;
        }
    }
}