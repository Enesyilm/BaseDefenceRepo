using System;

namespace Data.ValueObjects.PlayerData
{
    [Serializable]
    public class PlayerData
    {
        public int PlayerHealth;
        public int PlayerHealingRate;
        public float PlayerHealingOffset;
        public float PlayerSpeed;
        public int AttackRange;
    }
}