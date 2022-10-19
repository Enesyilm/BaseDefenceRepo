using System.Collections.Generic;
using Abstracts;
using Controllers.Player;
using Controllers.TurretControllers;
using Enum;
using Interfaces;
using Managers;
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
                 playerManager.CheckAreaStatus( AreaType.BaseDefense);
                 StartCoroutine(playerManager.StartHealing());
                 playerManager.EnemyTarget = null;
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
                 playerManager.CheckAreaStatus(AreaType.BattleOn);
                 playerManager.HasEnemyTarget = false;
                 playerManager.EnemyList.Clear();
             }
             if (other.TryGetComponent(out EnemyDamager enemyDamager))
             {
                 playerManager.OnUpdateHealth(ScoreTypes.DecScore,enemyDamager.GetDamage());
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
       
    }
}