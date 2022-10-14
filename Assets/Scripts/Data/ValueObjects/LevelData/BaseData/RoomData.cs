using System;
using Buyablezone.PurchaseParams;

namespace Data.ValueObjects
{[Serializable]
    public class RoomData
    {
        public bool IsOpened=false;
        public TurretData TurretData;
        public BuyableZoneDataList buyableZoneDataStages;
    }
}