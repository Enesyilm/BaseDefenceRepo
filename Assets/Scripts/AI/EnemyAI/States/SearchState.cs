using AIBrains.EnemyBrain;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;

namespace AI.States
{
    public class SearchState:IState
    {
        private EnemyAIBrain _enemyAIBrain;
        private NavMeshAgent _navMeshAgent;
        private Transform _spawnPosition;
        public SearchState(EnemyAIBrain enemyAIBrain,NavMeshAgent navMeshAgent,Transform spawnPosition)
        {
            _enemyAIBrain = enemyAIBrain;
            _navMeshAgent = navMeshAgent;
            _spawnPosition = spawnPosition;
        }
        public void Tick()
        {
        }

        public void OnEnter()
        {
            _navMeshAgent.enabled = true;
            GetRandomPointOnBakedSurface();
        }

        public void OnExit()
        {
            
        }

        private void GetRandomPointOnBakedSurface()
        {
            bool RandomPoint(Vector3 center, float range, out Vector3 result)
            {
                for (int i = 0; i < 60; i++)
                {
                    Vector3 randomPoint = center + Random.insideUnitSphere * range;
                    Vector3 randomPos = new Vector3(randomPoint.x, 0, _spawnPosition.transform.position.z);
                    NavMeshHit hit;
                    if (NavMesh.SamplePosition(randomPos, out hit, 1.0f, 1))
                    {
                        result = hit.position;
                        return true;
                    }
                   
                }
                result = Vector3.zero;
                return false;

            }
         Vector3 point;
         if(!RandomPoint(_spawnPosition.position,20,out point))return;
        _navMeshAgent.Warp(point);
        }
        

       
    }
}