using System;
using AIBrains.EnemyBrain;
using DG.Tweening;
using Enum;
using Enums;
using Interfaces;
using Managers;
using Signals;
using UnityEngine;
using UnityEngine.AI;

namespace AI.States
{
    public class DeathState:IState,IReleasePoolObject,IGetPoolObject
    {
        public bool isDead => IsEnemyDead != null;
        Transform IsEnemyDead;
        private EnemyAIBrain _enemyAIBrain;
        private Animator _animator;
        private NavMeshAgent _navmeshAgent;
        private float _timer=0;
        private SkinnedMeshRenderer _skinnedMeshRenderer;
        public DeathState(EnemyAIBrain enemyAIBrain, NavMeshAgent navmeshAgent, Animator animator,
            SkinnedMeshRenderer skinnedMeshRenderer)
        {
            _enemyAIBrain = enemyAIBrain;
            _animator = animator;
            _navmeshAgent = navmeshAgent;
            _skinnedMeshRenderer=skinnedMeshRenderer;
        }
        public void Tick()
        {
            //ObjectPoolManager.Instance.ReturnObject(_enemyAIBrain.gameObject,_enemyAIBrain.EnemyType.ToString());
            _timer+=Time.deltaTime;
            if (_timer >= 2)
            {
                for (int index = 0; index < 3; index++)
                {
                    GetObjectType(PoolObjectType.Money);
                    
                }
                ReleaseObject(_enemyAIBrain.gameObject,_enemyAIBrain.EnemyType);
               
                _timer = 0;
            }
        }

        public void OnEnter()
        {
            _animator.SetTrigger("Die");
            _navmeshAgent.enabled=false;
            _skinnedMeshRenderer.material.DOColor(Color.gray, 0.5f);
            Debug.Log("Death State");
        }

        public void OnExit()
        {
        }

        public void ReleaseObject(GameObject obj, PoolObjectType poolType)
        {
            PoolSignals.Instance.onReleaseObjectFromPool?.Invoke(obj,poolType);
            ObjectPoolManager.Instance.ReturnObject(_enemyAIBrain.gameObject,_enemyAIBrain.EnemyType.ToString());
        }

        public GameObject GetObjectType(PoolObjectType poolType)
        {
            GameObject _money = PoolSignals.Instance.onGetObjectFromPool.Invoke(PoolObjectType.Money);
            _money.transform.position = _enemyAIBrain.transform.position;
            return _money;
        }
    }
}