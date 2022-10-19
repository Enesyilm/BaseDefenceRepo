using System;
using System.Collections.Generic;
using UnityEngine;

namespace Buyablezone.PurchaseParams
{
    [Serializable]
    public class BuyableZoneDataList
    {
        [Header("New Buyable Item")] public List<BuyableZoneData> BuyableZoneStages;

        public BuyableZoneDataList()
        {
            BuyableZoneStages=new List<BuyableZoneData>()
            {
                new BuyableZoneData(),
            };
        }

        public BuyableZoneDataList(int payedAmount,int requiredAmount)
        {
           BuyableZoneStages=new List<BuyableZoneData>()
           {
               new BuyableZoneData(payedAmount,requiredAmount),
           };
        }
        
        public int BuyableLevel=0;
    }
    [Serializable]
    public class BuyableZoneData
    {
        public BuyableZoneData()
        {
            
        }
        public BuyableZoneData(int payedAmount,int requiredAmount)
        {
            PayedAmount=payedAmount;
            RequiredAmount = requiredAmount;
        }
        public int PayedAmount;
        public int RequiredAmount;
    }
}