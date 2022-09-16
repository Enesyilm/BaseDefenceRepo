using System;
using System.Timers;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class DropZonePhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] 
        private DropZoneManager dropZoneManager;

        #endregion
        #region Private Variables

        

        #endregion

        #endregion
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                dropZoneManager.CalculateTimer();
            }
        }

       
    }
}