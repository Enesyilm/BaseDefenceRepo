using Data.ValueObjects.ScoreData;
using UnityEngine;

namespace Data.UnityObjects
{
    [CreateAssetMenu(fileName = "CD_Score", menuName = "BaseDefence/CD_Score", order = 0)]
    public class CD_Score : ScriptableObject
    {
        public ScoreData ScoreData;
    }
}