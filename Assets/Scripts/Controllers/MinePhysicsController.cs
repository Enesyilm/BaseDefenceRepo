using System;
using System.Threading.Tasks;
using DG.Tweening;
using Enum;
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
            [SerializeField]
            private SphereCollider lureCollider;
            [SerializeField]
            private SphereCollider explosionCollider;
        #endregion

        #region Private Variables
        private int initalLureSphereSize = 40;
        private int initalExplosionSphereSize = 10;

        private float timer;
        private float payOffset=0.1f;
        

        #endregion
        #endregion

        private void Awake()
        {
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (timer>payOffset)
                {
                    //Revize edecegim button modulunu yapinca
                    mineManager.PayGemToMine();
                    timer = 0;
                }
                else
                {
                    timer += Time.deltaTime;
                }
                
            }
        }

        public async void ChangeColliderState(LandMineState _state)
        {
            switch (_state)
            {
                case LandMineState.Explosion:
                    // DOTween.To(x => lureCollider.radius = x
                    //     , lureCollider.radius
                    //     , initalExplosionSphereSize
                    //     , 0.3f);
                    lureCollider.radius = initalExplosionSphereSize;
                    lureCollider.tag = "MineExplosion";
                    //lureCollider.enabled = true;
                    
                    break;
                case LandMineState.Idle: 
                    await Task.Delay(1000);
                    lureCollider.radius = .1f;
                    lureCollider.enabled = false;
                    break;
                case LandMineState.Lure:
                    // DOTween.To(x => lureCollider.radius = x
                    //     , lureCollider.radius
                    //     , initalLureSphereSize
                    //     , 0.3f);
                    lureCollider.radius = initalLureSphereSize;
                    lureCollider.tag = "MineLure";
                    lureCollider.enabled = true;
                    break;
                
            }
        }
    }
}