using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data.ValueObjects.AiData
{
    [Serializable]
    public class EnemyTypeData
    {
        public int Health=100;
        public Color EnemyColor;
        public int Damage=10;
        public float AttackRange=2;
        public float AttackSpeed=5;
        public float MoveSpeed=5;
        public float ChaseSpeed=10;
        public List<Transform> TargetList=new List<Transform>();
        public Transform SpawnPosition;
    }
}