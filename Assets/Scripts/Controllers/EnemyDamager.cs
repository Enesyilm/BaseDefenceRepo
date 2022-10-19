using System;
using Data.ValueObjects;
using Data.ValueObjects.AiData.EnemyData;
using Interfaces;
using UnityEngine;

namespace Controllers
{
    public class EnemyDamager : MonoBehaviour,IDamager
    {
        private EnemyTypeData _enemyTypeData;
        private void Awake()
        {
            
        }

        public void SetEnemyData(EnemyTypeData enemyAIData)
        {
            _enemyTypeData=enemyAIData;
        }

        public int GetDamage()
        {
            return _enemyTypeData.Damage;
        }
    }
}