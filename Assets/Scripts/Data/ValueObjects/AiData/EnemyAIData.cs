using System;
using System.Collections.Generic;
using Data.ValueObjects.AiData.EnemyData;
using UnityEngine;

namespace Data.ValueObjects
{
    [Serializable]
    public class EnemyAIData
    {
        public List<EnemyTypeData> EnemyList;
        public List<Transform> TargetList = new List<Transform>();
    }
}