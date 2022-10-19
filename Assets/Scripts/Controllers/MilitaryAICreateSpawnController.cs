using Buyablezone.Interfaces;
using Buyablezone.PurchaseParams;
using Enum;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class MilitaryAICreateSpawnController : MonoBehaviour,IBuyable
    {
        [SerializeField] private MilitaryBaseManager _militaryBaseManager;
        private BuyableZoneDataList _buyableZoneDataList=new BuyableZoneDataList(0,15);
        public BuyableZoneDataList GetBuyableData()
        {
            return _buyableZoneDataList;
        }

        public void TriggerBuyingEvent()
        {
            _militaryBaseManager.UpdateSoldierAmount();
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