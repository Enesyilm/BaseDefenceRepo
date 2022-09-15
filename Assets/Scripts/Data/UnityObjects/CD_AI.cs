using Data.ValueObjects;
using Data.ValueObjects.AiData;
using UnityEngine;

namespace Data.UnityObjects
{
    [CreateAssetMenu(fileName = "CD_AI", menuName = "BaseDefence/CD_AI", order = 0)]
    public class CD_AI : ScriptableObject
    {
        public AmmoWorkerAIData AmmoWorkerAIData;
        public MoneyWorkerAIData MoneyWorkerAIData;
        public SoldierAIData SoldierAIData;
        public EnemyAIData EnemyAIData;
    }
}