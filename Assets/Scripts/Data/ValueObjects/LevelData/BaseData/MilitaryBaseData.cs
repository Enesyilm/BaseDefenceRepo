using System;
using UnityEngine;

namespace Data.ValueObjects
{
    [Serializable]
    public class MilitaryBaseData
    {
        public int BaseCapacity;
        public int TentCapacity;
        public int CandidateAmount;
        public int CurrentSoldierAmount;
        public int SoldierUpgradeTime;
        public int SoldierSlotCost;
        public int SlotAmount;
        public Transform SlotTransform;
        public int AttackTime;
        public GameObject SlotPrefab;
        public Transform frontYardSoldierPosition;
    }
}