using System;
using AIBrains.EnemyBrain;
using Enum;
using Enums;
using Interfaces;
using Managers;
using Signals;
using UnityEngine;
using UnityEngine.AI;

namespace AI.States
{
    public class DeathState:IState,IReleasePoolObject
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
            ReleaseObject(_enemyAIBrain.gameObject,_enemyAIBrain.EnemyType);
        }

        public void OnEnter()
        {
        }

        public void OnExit()
        {
        }

        public void ReleaseObject(GameObject obj, PoolObjectType poolType)
        {
            PoolSignals.Instance.onReleaseObjectFromPool?.Invoke(obj,poolType);
            ObjectPoolManager.Instance.ReturnObject(_enemyAIBrain.gameObject,_enemyAIBrain.EnemyType.ToString());
        }
    }
}