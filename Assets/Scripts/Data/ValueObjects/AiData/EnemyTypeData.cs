using System;

namespace Data.ValueObjects.AiData
{
    [Serializable]
    public class EnemyTypeData
    {
        public int Health;
        public int Damage;
        public float AttackRange;
        public float AttackSpeed;
        public float MoveSpeed;
        public float ChaseSpeed;
    }
}