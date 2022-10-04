using System;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class MilitaryBaseAttackButtonPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        
        #endregion

        #region Serialized Variables

        [SerializeField] private MilitaryBaseManager manager;
        
        #endregion

        #region Private Variables

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                SoldierAISignals.Instance.onSoldierActivation?.Invoke();
            }
        }
    }
}