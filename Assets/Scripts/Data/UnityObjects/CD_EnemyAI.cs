using Data.ValueObjects.AiData;
using UnityEngine;
using UnityEngine.AI;
[CreateAssetMenu(fileName = "CD_EnemyAI", menuName = "BaseDefence/CD_EnemyAI")]
public class CD_EnemyAI : ScriptableObject
{
    public EnemyAIData EnemyAIData;
}