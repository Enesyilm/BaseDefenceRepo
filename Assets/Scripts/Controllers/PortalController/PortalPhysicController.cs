using System;
using Managers;
using Signals;
using UnityEngine;

namespace DefaultNamespace
{
    public class PortalPhysicController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerManager playerManager))
            {
                //CoreGameSignals.Instance.onNextLevel?.Invoke;
            }
        }
    }
}