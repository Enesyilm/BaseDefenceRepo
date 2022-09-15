using Data.ValueObjects.AiData;
using Data.ValueObjects.AiData.EnemyData;
using UnityEngine;
using UnityEngine.AI;
[CreateAssetMenu(fileName = "CD_EnemyAI", menuName = "BaseDefence/CD_EnemyAI")]
public class CD_EnemyAI : ScriptableObject
{
    public EnemyTypeData EnemyAIData;
}