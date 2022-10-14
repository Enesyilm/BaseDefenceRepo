using Abstracts;
using Controllers.Player;
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
                 var playerIsGoingToFrontYard = other.transform.position.z > transform.position.z;
                 gameObject.layer = LayerMask.NameToLayer("Base");
                 gameObject.transform.parent.gameObject.layer = LayerMask.NameToLayer("Base");
                 playerManager.CheckAreaStatus( AreaType.BaseDefense);
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
             }
        }
        // private void OnTriggerExit(Collider other)
        // {
        //     if (other.TryGetComponent(out GatePhysicsController physicsController))
        //     {
        //         GateExit(other);
        //     }
        // }
       
    }
}