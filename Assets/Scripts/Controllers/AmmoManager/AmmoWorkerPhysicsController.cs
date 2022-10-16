using AIBrain;
using Managers;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace Controllers
{
    public class AmmoWorkerPhysicsController : MonoBehaviour
    {
        
        private AmmoWorkerBaseManager _ammoManager;
        
        public AmmoStackerController ammoStackerController;
        public AmmoWorkerBrain Brain;

        private float _timer = 0.4f;

        private void OnTriggerEnter(Collider other)
        {

            if (other.TryGetComponent(typeof(AmmoManagerPhysicsController), out Component ammoManagment))//it must change
            {
                 Brain.IsAmmoWorkerReachedWareHouse = true;
            }
           
             if (other.TryGetComponent(out AmmoDropZonePhysicsController ammoDropZonePhysicsController))//it must change
             {
                 //_ammoManager.WhenEnterTurretStack(transform.parent.GetComponent<AmmoWorkerBrain>());
                 //tüm zoneları kontrol edecek amma manager base fonksiyonu olsun ve 
                 Debug.Log("AmmoDropZonePhysicsController");
                 Brain.IsAmmoWorkerReachedDropzone = true;
                 if (ammoDropZonePhysicsController.ammoDropZoneManager.CheckIfDropzoneFull())
                 {
                     return;
                 }
                 ammoStackerController.OnRemoveAllStack(other.transform);
             }
        }


        private void OnTriggerExit(Collider other)
        {
           
            if (other.TryGetComponent(typeof(AmmoManagerPhysicsController), out Component ammoManagment))//it must change
            {


                Brain.IsAmmoWorkerReachedWareHouse = false;

            }
        }
        



    }
}