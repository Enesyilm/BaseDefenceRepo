using System;
using System.Threading.Tasks;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class MinePhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
            [SerializeField]
            private MineManager mineManager;
        #endregion

        #region Private Variables

        private int timer;
        

        #endregion
        #endregion
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
               
                    WaitForSeconds(4);
                    mineManager.PayGemToMine();
                    
                
            }
        }
        public async void WaitForSeconds(int _time)
        {
            Task.Delay(_time*1000).Wait();
        }
    }
}