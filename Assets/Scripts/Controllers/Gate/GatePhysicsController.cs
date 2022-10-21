using System;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class GatePhysicsController : MonoBehaviour
    {
        [SerializeField] private GateManager gateManager;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                gateManager.PlayGateAnimation(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            gateManager.PlayGateAnimation(false);
        }
    }
}