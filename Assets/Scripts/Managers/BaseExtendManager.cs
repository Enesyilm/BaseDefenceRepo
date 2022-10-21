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
            _baseRoomData = InitializeDataSignals.Instance.onGetBaseRoomData.Invoke();
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
             InitializeDataSignals.Instance.onLoadBaseRoomData-=OnLoadBaseRoom;
        }

        #endregion
        private void OnLoadBaseRoom(BaseRoomData baseRoomData)
        {
            _baseRoomData = baseRoomData;
            for (int index = 0; index < baseParts.Count; index++)
            {
             if (baseRoomData.Rooms[(int)baseParts[index].BaseRoomType].IsOpened)
             {
                 baseParts[index].AlreadyBuyed();
             }
            
            }
            
        }

        
    }
}