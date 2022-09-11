using System;
using Enums;
using UnityEngine;

namespace Data.ValueObjects
{
    [Serializable]
    public class PlayerStackData
    {
        public PlayerStackType PlayerStackType;
        public Vector2 Capacity;
        public Vector3 Offset;
        
    }
}