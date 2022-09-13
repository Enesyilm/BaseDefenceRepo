using System;
using DG.Tweening;
using Enums;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerManager manager;
        [SerializeField] private new Collider collider;
        [SerializeField] private new Rigidbody rigidbody;

        #endregion

        #region Private Variables

        private int _frontyardLayer;
        private int _baseLayer;

        #endregion

        #endregion

        private void Awake()
        {
            GetLayerReferences();
        }

        private void GetLayerReferences()
        {
            _frontyardLayer = LayerMask.NameToLayer("Frontyard");
            _baseLayer = LayerMask.NameToLayer("Base");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("GateEnter"))
            {
                gameObject.layer = _baseLayer;
            }
            if (other.CompareTag("GateExit"))
            {
                gameObject.layer = _frontyardLayer;
            }
        }

        public void SetPhysicsData()
        {
        }
    }
}