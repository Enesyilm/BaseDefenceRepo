using Concreate;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class AmmoDropZonePhysicsController : MonoBehaviour
    {
        [SerializeField]
        private AmmoDropZoneStacker ammoDropZoneStacker;

        public AmmoDropZoneManager ammoDropZoneManager;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<StackableAmmo>(out StackableAmmo interactable))
            {

                if(ammoDropZoneManager.CheckIfDropzoneFull())
                {
                    Debug.Log("CheckIfDropzoneFull");
                    return;
                }
             
                ammoDropZoneStacker.GetStack(interactable.SendToStack(), interactable.SendToStack().transform);
            }
            //else if (other.TryGetComponent(out AmmoWorkerInteractable ammoWorkerInteractable))
            //{
            //    Debug.Log("other.transfrom" + other.transform.name + " " + other.transform.parent.name);
            //    ammoDropZoneStacker.OnRemoveAllStack(other.transform);
            //}

        }
  
    }
}