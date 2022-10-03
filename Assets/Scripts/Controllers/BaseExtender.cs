using Buyablezone.Interfaces;
using Buyablezone.PurchaseParams;
using UnityEngine;

namespace Controllers
{
    public class BaseExtender : MonoBehaviour,IBuyable
    {
        BuyableZoneDataList _buyableZoneDataList = new BuyableZoneDataList();
        [SerializeField] private GameObject extendablePart;
                public BuyableZoneDataList GetBuyableData()
                {
                    return _buyableZoneDataList;
                }
        
                public void TriggerBuyingEvent()
                {
                    extendablePart.SetActive(true);
                }
        
                public bool MakePayment()
                {
                    return true;
                }
    }
}