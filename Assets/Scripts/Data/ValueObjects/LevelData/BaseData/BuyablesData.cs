using System;
using Buyablezone.PurchaseParams;

namespace Data.ValueObjects
{
    [Serializable]
    public class BuyablesData
    {
        public BuyableZoneDataList LandMine;
        public BuyableZoneDataList BaseRight;
        public BuyableZoneDataList BaseRight2;
        public BuyableZoneDataList BaseLeft;
        public BuyableZoneDataList BaseLeft2;
       
    }
}