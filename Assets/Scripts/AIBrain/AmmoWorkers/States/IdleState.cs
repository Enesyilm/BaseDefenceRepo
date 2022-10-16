using AI;
using AIBrain;
using UnityEngine;
using UnityEngine.AI;

namespace States
{
    public class IdleState:IState
    {

        private NavMeshAgent _agent;
        private Animator _animator;
        private AmmoWorkerBrain _ammoWorkerBrain;

        public IdleState(NavMeshAgent agent, Animator animator,AmmoWorkerBrain ammoWorkerBrain )
        {
            _agent = agent;
            _animator = animator;
            _ammoWorkerBrain = ammoWorkerBrain;
        }

        public void Tick()
        {
            Debug.Log("Idle");
        }

        public void OnEnter()
        {
            _agent.SetDestination(_ammoWorkerBrain.transform.position);
            _animator.SetTrigger("Idle");
        }

        public void OnExit()
        {
          
        }
    }
}