using System.Collections.Generic;
using Data.ValueObjects;
using UnityEngine;

namespace Data.UnityObjects
{
    [CreateAssetMenu(fileName = "CD_PlayerStack", menuName = "BaseDefence/CD_PlayerStack", order = 0)]
    public class CD_PlayerStack : ScriptableObject
    {
        public List<PlayerStackData> PlayerStackData;
    }
}