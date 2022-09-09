using System;
using UnityEngine;

namespace Data.ValueObjects
{
    [Serializable]
    public class TurretData
    {
        public bool IsActive;
        public bool HasTurretSoldier;
        public int SoldierCost;
        public int SoldierPayedAmount;
        public int TurretCapacity;
        public int TurretDamage;
        public ParticleSystem TurretParticle;
    }
}