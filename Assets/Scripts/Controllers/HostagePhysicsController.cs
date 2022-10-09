using System;
using Enum;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class HostagePhysicsController : MonoBehaviour
    {
        [SerializeField]
        private HostageManager hostageManager;
        private void OnTriggerEnter(Collider other)
        {

            if (other.CompareTag("Player") && hostageManager.CurrentType == HostageType.HostageWaiting)
            {
                
                hostageManager.AddToHostageStack();
            }
            
        }
    }
}