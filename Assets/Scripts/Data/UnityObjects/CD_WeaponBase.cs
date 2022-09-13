using System.Collections.Generic;
using Data.ValueObjects.WeaponData;
using UnityEngine;

namespace Data.UnityObjects
{
    // [CreateAssetMenu(fileName = "CD_Weapon", menuName = "BaseDefence/CD_Weapon", order = 0)]
    public abstract class CD_WeaponBase : ScriptableObject
    {
        public int Damage=5;
        public float AttackRate=2;
        public ParticleSystem WeaponParticle;

        public abstract void Shoot();

    }
    public abstract class PlayerManagerr : ScriptableObject
    {
        public int Damage=5;
        public float AttackRate=2;
        public ParticleSystem WeaponParticle;

        public abstract void Shoot();

    }
}