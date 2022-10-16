using System;
using System.Collections.Generic;
using UnityEngine;

namespace Buyablezone.PurchaseParams
{
    [Serializable]
    public class BuyableZoneDataList
    {
        [Header("New Buyable Item")] public List<BuyableZoneData> BuyableZoneStages=
        new List<BuyableZoneData>()
        {
            new BuyableZoneData()
        };
        public int BuyableLevel=0;
    }
    [Serializable]
    public class BuyableZoneData
    {
        public int PayedAmount;
        public int RequiredAmount;
    }
}