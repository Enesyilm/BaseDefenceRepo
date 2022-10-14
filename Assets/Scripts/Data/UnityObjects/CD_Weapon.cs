using System.Collections.Generic;
using Data.ValueObjects.WeaponData;
using UnityEngine;

[CreateAssetMenu(fileName = "CD_Weapon", menuName = "BaseDefence/CD_Weapon", order = 0)]
    public class CD_Weapon : ScriptableObject
    {
        public List<WeaponData> WeaponData;


    }