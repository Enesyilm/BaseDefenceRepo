using AIBrains.EnemyBrain;
using UnityEngine;

namespace Controllers
{
    public class EnemyDetectionController : MonoBehaviour
    {
        #region Self Variables
        
                #region Public Variables
        
                #endregion
        
                #region Serialized Variables
                [SerializeField]
                private EnemyAIBrain enemyAIBrain;
        
                #endregion
        
                #region Private Variables
        
                private Transform _detectedMine;
        
                #endregion
        
                #endregion
                private void OnTriggerEnter(Collider other)
                { 
                    // if (other.TryGetComponent(out PlayerPhysicsController physicsController))
                    // {
                    //     PickOneTarget(other);
                    //     enemyAIBrain.CachePlayer(physicsController);
                    //     enemyAIBrain.CacheSoldier(null);
                    // }
                    // // if (other.TryGetComponent(out SoldierHealthController soldierHealthController))
                    // // {
                    // //     enemyAIBrain.CachePlayer(null);
                    // //     PickOneTarget(other);
                    // //     enemyAIBrain.CacheSoldier(soldierHealthController);
                    // // }
                    if (other.CompareTag("Player"))
                    {
                       
                        enemyAIBrain.PlayerTarget = other.transform.parent.transform;
                    }
                }
                private void OnTriggerExit(Collider other)
                { 
                    // if (other.TryGetComponent(out PlayerPhysicsController physicsController) )
                    // {
                    //     enemyAIBrain.SetTarget(null);
                    //     enemyAIBrain.CachePlayer(null);
                    // }
                    // if (other.TryGetComponent(out IDamageable Idamageable))
                    // {
                    //     enemyAIBrain.SetTarget(null);
                    //     enemyAIBrain.CacheSoldier(null);
                    // }
                    if (other.CompareTag("Player"))
                    {
                       
                        enemyAIBrain.PlayerTarget = null;
                    }
                }
                // private void PickOneTarget(Collider other)
                // {
                //     if (enemyAIBrain.CurrentTarget == enemyAIBrain.TurretTarget)
                //     {
                //         enemyAIBrain.SetTarget(other.transform);
                //     }
                // }
    }
}