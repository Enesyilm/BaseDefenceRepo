using System;
using System.Threading.Tasks;
using Buyablezone.Interfaces;
using Buyablezone.PurchaseParams;
using Data.UnityObjects;
using Data.ValueObjects;
using Enum;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class BaseExtender : MonoBehaviour,IBuyable
    {
        private BaseRoomData _baseRoomData;
        private BuyableZoneDataList _buyableZoneDataList=new BuyableZoneDataList();
        [SerializeField] private GameObject extendablePart;
        [SerializeField] private GameObject closeablePart;
        [SerializeField] private GameObject buyableZone;
        public BaseRoomTypes BaseRoomType;
        

       
        #region EventSubscription

       
        private void OnEnable()
        {
            _baseRoomData=InitializeDataSignals.Instance.onGetBaseRoomData?.Invoke();
            SubscribeEvents();
            OnLoadBaseRoom(_baseRoomData);
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
            _buyableZoneDataList=baseRoomData.Rooms[(int)BaseRoomType].buyableZoneDataStages;
            if (CheckRoomStatus())
            {
                TriggerBuyingEvent();
            }

           
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

        public BuyableZoneDataList GetBuyableData()
        {
            WaitUntilSaveData();
            return _buyableZoneDataList;
        }

        private async void WaitUntilSaveData()
        {
            await Task.Delay(1000);
        }

        public  void AlreadyBuyed()
        {
            extendablePart.SetActive(true);
            closeablePart.SetActive(false);
            buyableZone.SetActive(false);
        }
        public void TriggerBuyingEvent()
        {
            _baseRoomData.Rooms[(int)BaseRoomType].IsOpened=true;
            InitializeDataSignals.Instance.onSaveBaseRoomData?.Invoke(_baseRoomData);
            extendablePart.SetActive(true);
            closeablePart.SetActive(false);
        }

        public bool MakePayment()
        {
            int _gemAmount=ScoreSignals.Instance.onGetScore.Invoke(ScoreVariableType.TotalGem);
            if ( _gemAmount> 0)
            {
                ScoreSignals.Instance.onUpdateGemScore?.Invoke(ScoreTypes.DecScore);
                return true;
                
            }
            else
            {
                return false;
            }
        }
    }
}