using System.Collections.Generic;
using Data.ValueObjects.AIStackData;
using UnityEngine;

namespace Data.UnityObjects
{
    [CreateAssetMenu(fileName = "CD_AIStack", menuName = "BaseDefence/CD_AIStack", order = 0)]
    public class CD_AIStack : ScriptableObject
    {
        List<AIStackData>AIStackDatas;
        public AIStackData AIStackData;
    }
}