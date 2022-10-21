using UnityEngine;
using Signals;
using Data.ValueObject.AIDatas;
using Enums;
using Controllers;
using Data.ValueObjects;
using Data.ValueObjects.AiData.EnemyData;
using DefaultNamespace;
using Enum;

namespace Managers
{
    public class PortalManager : MonoBehaviour
    {
        #region Self variables 

        #region Public Variables



        #endregion

        #region Seriliazable Variables

        [SerializeField]
        private PortalController portalController;

        #endregion

        #region Private Variables

        #endregion

        #endregion

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            
            EnemySignals.Instance.onOpenPortal += OnOpenPortal;
            
        }

        private void UnsubscribeEvents()
        {
            
            EnemySignals.Instance.onOpenPortal -= OnOpenPortal;
            
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
        private void OnOpenPortal()
        {
            portalController.OpenPortal();
        }
    } 
}
