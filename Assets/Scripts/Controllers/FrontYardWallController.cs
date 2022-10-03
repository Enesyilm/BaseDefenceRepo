using Buyablezone.Interfaces;
using Buyablezone.PurchaseParams;
using Enum;
using UnityEngine;

namespace Controllers
{
    public class FrontYardWallController : MonoBehaviour,IBuyable
    {
        BuyableZoneDataList _buyableZoneDataList = new BuyableZoneDataList();
        int Gem = 100;

        public BuyableZoneDataList GetBuyableData()
        {
            return _buyableZoneDataList;
        }

        public void TriggerBuyingEvent()
        {
            gameObject.SetActive(false);
        }

        public bool MakePayment()
        {
            if (Gem > 0)
            {
                Gem--;
                return true;
                
            }
            else
            {
                return false;
            }
        }
    }
}