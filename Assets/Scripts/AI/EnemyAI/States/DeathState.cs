using AIBrains.EnemyBrain;
using FrameworkGoat;
using UnityEngine;
using UnityEngine.AI;

namespace AI.States
{
    public class DeathState:IState
    {
        public bool isDead => IsEnemyDead != null;
        Transform IsEnemyDead;
        private EnemyAIBrain _enemyAIBrain;
        public DeathState(EnemyAIBrain enemyAIBrain,NavMeshAgent _navmeshAgent,Animator _animator)
        {
            _enemyAIBrain = enemyAIBrain;
        }
        public void Tick()
        {
            ObjectPoolManager.Instance.ReturnObject(_enemyAIBrain.gameObject,_enemyAIBrain.EnemyType.ToString());
        }

        public void OnEnter()
        {
            Debug.LogWarning("IsEnemyDead "+IsEnemyDead);
        }

        public void OnExit()
        {
        }
    }
}