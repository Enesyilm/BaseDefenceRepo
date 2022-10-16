using Managers;
using Signals;
using System.Collections;
using Buyablezone.Interfaces;
using Buyablezone.PurchaseParams;
using UnityEngine;

namespace Controllers
{
    public class AmmoWorkerCreaterZone : MonoBehaviour,IBuyable
    {
        [SerializeField] private BuyableZoneDataList _buyableZoneDataList=new BuyableZoneDataList();
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
        {AmmoManagerSignals.Instance.onPlayerEnterAmmoWorkerCreaterArea(transform);
        }

        public bool MakePayment()
        {
            return true;
        }
    }
}