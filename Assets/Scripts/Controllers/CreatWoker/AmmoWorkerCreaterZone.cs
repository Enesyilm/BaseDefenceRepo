using Managers;
using Signals;
using System.Collections;
using Buyablezone.Interfaces;
using Buyablezone.PurchaseParams;
using Enum;
using UnityEngine;

namespace Controllers
{
    public class AmmoWorkerCreaterZone : MonoBehaviour,IBuyable
    {
        private BuyableZoneDataList _buyableZoneDataList=new BuyableZoneDataList(0,20);
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent( out PlayerPhysicsController playerPhysicsController))
            {
                
            }


        }


        public BuyableZoneDataList GetBuyableData()
        {
            return _buyableZoneDataList;
        }

        public void TriggerBuyingEvent()
        {
            AmmoManagerSignals.Instance.onPlayerEnterAmmoWorkerCreaterArea(transform);
        }

        public bool MakePayment()
        {
            int _moneyAmount=ScoreSignals.Instance.onGetScore.Invoke(ScoreVariableType.TotalMoney);
            if ( _moneyAmount> 0)
            {
                ScoreSignals.Instance.onUpdateMoneyScore?.Invoke(ScoreTypes.DecScore);
                return true;
                
            }
            else
            {
                return false;
            }
        }
    }
}