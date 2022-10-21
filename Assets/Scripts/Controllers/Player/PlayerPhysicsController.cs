using System.Collections.Generic;
using Abstracts;
using Concreate;
using Controllers.Player;
using Controllers.TurretControllers;
using Enum;
using Interfaces;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class PlayerPhysicsController : Interactable
    {
        #region Self Variables

        #region Public Variables
        
        #endregion

        #region Serialized Variables,
        
        [SerializeField] private PlayerManager playerManager;
        
        #endregion

        #region Private Variables

        #endregion
        
        #endregion
        private void OnTriggerEnter(Collider other)
        {
            // if (other.TryGetComponent(out GatePhysicsController physicsController))
            // {
            //     GateEnter(other);
            // }
            if (other.CompareTag("GateEnter"))
            {
                int enemy = playerManager.EnemyList.Count;
                 var playerIsGoingToFrontYard = other.transform.position.z > transform.position.z;
                 gameObject.layer = LayerMask.NameToLayer("Base");
                 gameObject.transform.parent.gameObject.layer = LayerMask.NameToLayer("Base");
                 playerManager.OnUpdateHealth(ScoreTypes.DecScore,0);
                 playerManager.CheckAreaStatus( AreaType.BaseDefense);
                 StartCoroutine(playerManager.StartHealing());
                 playerManager.EnemyTarget = null;
                 int enemyListCount = playerManager.EnemyList.Count;
                 for (int i = 0; i < enemyListCount; i++)
                 {
                     playerManager.EnemyList[i].IsTaken = false;
                 }
                 playerManager.EnemyList.Clear();
                 playerManager.EnemyTarget = null;
                 return;
                 // for (int index = 0; index < enemy; index++)
                 // {
                 //     playerManager.EnemyList[index].IsTaken=false;
                 // }
             }
             if (other.CompareTag("MineEntrance"))
             {
                 playerManager.SendHostageToMineBase(other.transform.position);
             }
             if (other.CompareTag("MilitaryEntrance"))
             {
                 playerManager.SendHostageToMilitaryBase();
             }
             if (other.CompareTag("GateExit"))
             {
                 var playerIsGoingToFrontYard = other.transform.position.z < transform.position.z;
                 gameObject.layer = LayerMask.NameToLayer("Frontyard");
                 gameObject.transform.parent.gameObject.layer = LayerMask.NameToLayer("Frontyard");
                 playerManager.OnUpdateHealth(ScoreTypes.DecScore,0);
                 playerManager.CheckAreaStatus(AreaType.BattleOn);
                 playerManager.EnemyTarget = null;
                 playerManager.EnemyList.Clear();
             }
             if (other.TryGetComponent(out EnemyDamager enemyDamager))
             {
                 Debug.Log("Enemy Damager");
                 playerManager.OnUpdateHealth(ScoreTypes.DecScore,enemyDamager.GetDamage());
             }
             if (other.TryGetComponent(out BombDamager bombDamager))
             {
                 Debug.Log("Enemy Damager");
                 playerManager.OnUpdateHealth(ScoreTypes.DecScore,bombDamager.GetDamage());
             }
             if (other.TryGetComponent(out TurretPhysicsController turretPhysicsController))
             {
                 playerManager.SetTurretAnimation(true);
             }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out TurretPhysicsController turretPhysicsController))
            {
                playerManager.SetTurretAnimation(false);
            }
        }

        public void ResetPlayerLayer()
        {
            playerManager.gameObject.layer = LayerMask.NameToLayer("Base");
            gameObject.layer = LayerMask.NameToLayer("Base");
        }
    }
}