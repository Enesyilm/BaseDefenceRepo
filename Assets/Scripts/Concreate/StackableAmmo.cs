using Abstracts;
using Interfaces;
using UnityEngine;

namespace Concreate
{
    public class StackableAmmo:AStackable
    {
        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] private BoxCollider collider;

        public void SetInit(Transform initTransform, Vector3 position)
        {
            throw new System.NotImplementedException();
        }

        public void SetVibration(bool isVibrate)
        {
            throw new System.NotImplementedException();
        }

        public void SetSound()
        {
            throw new System.NotImplementedException();
        }

        public void EmitParticle()
        {
            throw new System.NotImplementedException();
        }

        public void PlayAnimation()
        {
            throw new System.NotImplementedException();
        }

        public override GameObject SendToStack(Transform transform)
        {
            //transform.localRotation = new Quaternion(0, 0, 0, 1);
            rigidbody.useGravity = false;
            rigidbody.isKinematic = true;
            //collider.enabled = false;

            transform.gameObject.layer = LayerMask.NameToLayer("Ammo/AmmoTaken");

            return transform.gameObject;
        }


        public GameObject SendToStack()
        {
            rigidbody.useGravity = false;
            rigidbody.isKinematic = true;
            //collider.enabled = false;

            transform.gameObject.layer = LayerMask.NameToLayer("Ammo/AmmoOnDynamicStack");

            return transform.gameObject;

        }

        private void EditPhysics()
        {
            rigidbody.useGravity = false;
            rigidbody.isKinematic = true;
        }
    }
}