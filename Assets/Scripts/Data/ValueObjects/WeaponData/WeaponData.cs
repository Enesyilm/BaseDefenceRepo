using System;
using UnityEngine;

namespace Data.ValueObjects.WeaponData
{
    [Serializable]
    public class WeaponData
    {
        public int Damage;
        public float AttackRate;
        //public ParticleSystem WeaponParticle;
        public Mesh WeaponMesh;
        public int WeaponLevel;
        public bool HasSideMesh;
        public Mesh SideMesh;
    }
}