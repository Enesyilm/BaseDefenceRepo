using System;
using UnityEngine;

namespace Data.ValueObjects.AiData
{
    [Serializable]
    public class SoldierAIData
    {
        //public SoldierType SoldierType;
        public GameObject SoldierPrefab;
        public int Damage;
        public float SoldierSpeed;
        public float AttackRadius;
        public Coroutine AttackCoroutine;
        public float AttackDelay;
        public int Health;
        public Transform SpawnPoint;
    }
}