using System;
using Enums;
using UnityEngine;

namespace Data.ValueObjects.AIStackData
{
    [Serializable]
    public class AIStackData
    {
        public AIStackDataType AIStackDataType;
        public Vector2 MoneyCapacity;
        public Vector3 MoneyOffset;
    }
}