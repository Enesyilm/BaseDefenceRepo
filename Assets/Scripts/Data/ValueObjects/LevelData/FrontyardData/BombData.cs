using System;
using Buyablezone.PurchaseParams;

namespace Data.ValueObjects
{
    [Serializable]
    public class BombData
    {
        public int BombCost;
        public int BombWaitTime;
        public BuyableZoneDataList BuyableZoneDataList;
    }
}