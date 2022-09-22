using System;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class DropZonePhysicController : MonoBehaviour
    {
        [SerializeField]
        private DropZoneManager dropZoneManager;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(dropZoneManager.CurrentExpectedTag))
            {
                dropZoneManager.AddNewItemToStack(other.gameObject);
            }
        }
    }
}