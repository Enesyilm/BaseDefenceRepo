using System;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class DropZonePhysicController : MonoBehaviour
    {
        [SerializeField]
        private DropZoneManager dropZoneManager;
        [SerializeField]
        private BoxCollider collider;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(dropZoneManager.CurrentExpectedTag))
            {
                dropZoneManager.AddNewItemToStack(other.transform);
            }

            if (other.CompareTag("Player"))
            {
                //collider.enabled = false;
                dropZoneManager.DrainDropZone(other.transform);
            }
        }
    }
}