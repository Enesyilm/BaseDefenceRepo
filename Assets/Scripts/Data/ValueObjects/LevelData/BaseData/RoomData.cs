using System;

namespace Data.ValueObjects
{[Serializable]
    public class RoomData
    {
        public int RoomCost;
        public int RoomPayedAmount;
        public TurretData TurretData;
    }
}