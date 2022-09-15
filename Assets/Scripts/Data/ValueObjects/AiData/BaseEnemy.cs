using Enum;
using UnityEngine;

namespace Data.ValueObjects.AiData
{
    public abstract class BaseEnemy
    {
        public int Health;
        public int Damage;
        public int AttackRange;
        public int AttackSpeed;
        public float Speed;
        public float ChaseSpeed;
        public EnemyType EnemyType;

        public BaseEnemy(int health, int damage, int attackRange,
            int attackSpeed, float speed, float chaseSpeed,EnemyType enemyType)
        {
            Health = health;
            Damage = damage;
            AttackRange = attackRange;
            AttackSpeed = attackSpeed;
            Speed = speed;
            ChaseSpeed = chaseSpeed;
            EnemyType = enemyType;
        }
    }
}