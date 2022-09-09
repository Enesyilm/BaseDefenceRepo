using System.Collections.Generic;
using Data.ValueObjects.LevelData;
using UnityEngine;

namespace Data.UnityObjects
{
    [CreateAssetMenu(fileName = "CD_Level", menuName = "BaseDefence/CD_Level", order = 0)]
    public class CD_Level : ScriptableObject
    {
        public List<LevelData> LevelDatas;
    }
}