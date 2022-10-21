using System.Collections.Generic;
using Buyablezone.PurchaseParams;
using Controllers;
using Data.ValueObjects;
using Signals;
using UnityEngine;

namespace Managers
{
    public class FrontyardManager : MonoBehaviour
    {
        private FrontyardData _frontyardData;
        [SerializeField] private List<FrontYardWallController> wallList;

        private void Awake()
        {
            
        }
        #region EventSubscription
        private void OnEnable()
        {
            _frontyardData=InitializeDataSignals.Instance.onGetFrontyardData.Invoke();
            
            OnLoadFrontyardData(_frontyardData);
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InitializeDataSignals.Instance.onLoadFrontyardData+=OnLoadFrontyardData;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void UnSubscribeEvents()
        {
            InitializeDataSignals.Instance.onLoadFrontyardData-=OnLoadFrontyardData; 

        }
        #endregion
        private void OnLoadFrontyardData(FrontyardData frontyardData)
        {
            _frontyardData = frontyardData;
            for (int index = 0; index < wallList.Count; index++)
            {
                wallList[index].FrontyardData = _frontyardData;
                wallList[index].CurrentStageID = index;
                if (_frontyardData.Stages[index].IsOpened)
                {
                 
                    wallList[index].AlreadyOpened();
                   
                }
                
            }
            
        }
    }
}