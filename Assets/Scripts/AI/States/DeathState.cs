using UnityEngine;
using UnityEngine.AI;

namespace AI.States
{
    public class DeathState:IState
    {
        public bool isDead => IsEnemyDead != null;
        Transform IsEnemyDead; 
        public DeathState(NavMeshAgent _navmeshAgent,Animator _animator)
        {
            
        }
        public void Tick()
        {
        
        }

        public void OnEnter()
        {
        
        }

        public void OnExit()
        {
        }
    }
}