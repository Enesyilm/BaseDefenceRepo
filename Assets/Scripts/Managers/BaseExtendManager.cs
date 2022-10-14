using System;
using System.Collections.Generic;
using Buyablezone.PurchaseParams;
using Controllers;
using Data.ValueObjects;
using Signals;
using UnityEngine;

namespace Managers
{
    public class BaseExtendManager : MonoBehaviour
    {
        private BaseRoomData _baseRoomData;
        private BuyableZoneDataList _buyableZoneDataList;
        [SerializeField] private List<BaseExtender> baseParts;
        private void Awake()
        {
            
        }
        #region EventSubscription
        private void OnEnable()
        {
            _baseRoomData=InitializeDataSignals.Instance.onGetBaseRoomData.Invoke();
            Debug.Log(_baseRoomData);
            OnLoadBaseRoom(_baseRoomData);
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InitializeDataSignals.Instance.onLoadBaseRoomData+=OnLoadBaseRoom;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void UnSubscribeEvents()
        {
            throw new NotImplementedException();
        }


        private bool CheckRoomStatus()
        {
            BuyableZoneData _currentBuyableZoneData=_buyableZoneDataList.BuyableZoneStages[_buyableZoneDataList.BuyableLevel];
            if (_currentBuyableZoneData.PayedAmount>=_currentBuyableZoneData.RequiredAmount)
            {
                return true;
            }

            return false;
        }
        
        

        #endregion
        private void OnLoadBaseRoom(BaseRoomData baseRoomData)
               {
                   // _baseRoomData = baseRoomData;
                   // for (int index = 0; index < baseParts.Count; index++)
                   // {
                   //     Debug.Log("_baseRoomData"+baseRoomData.Rooms[(int)baseParts[index].BaseRoomType].IsOpened);
                   //     if (baseRoomData.Rooms[(int)baseParts[index].BaseRoomType].IsOpened)
                   //     {
                   //         baseParts[index].TriggerBuyingEvent();
                   //     }
                   //    
                   // }
                   
               }

        
    }
}