using Buyablezone.Interfaces;
using Buyablezone.PurchaseParams;
using UnityEngine;

namespace Controllers
{
    public class BaseExtender : MonoBehaviour,IBuyable
    {
        BuyableZoneDataList _buyableZoneDataList = new BuyableZoneDataList();
        [SerializeField] private GameObject extendablePart;
        [SerializeField] private GameObject closeablePart;
                public BuyableZoneDataList GetBuyableData()
                {
                    return _buyableZoneDataList;
                }
        
                public void TriggerBuyingEvent()
                {
                    extendablePart.SetActive(true);
                    closeablePart.SetActive(false);
                }
        
                public bool MakePayment()
                {
                    return true;
                }
    }
}