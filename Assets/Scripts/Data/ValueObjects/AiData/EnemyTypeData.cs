using System;
using System.Collections.Generic;
using Enum;
using UnityEngine;

namespace Data.ValueObjects.AiData.EnemyData
{
    [Serializable]
    public class EnemyTypeData:BaseEnemy
    {
        
        public Transform SpawnPosition;
        public EnemyTypeData(int health, int damage, int attackRange, int attackSpeed, float speed, float chaseSpeed, EnemyType enemyType) : base(health, damage, attackRange, attackSpeed, speed, chaseSpeed, enemyType)
        {
        }
    }
}