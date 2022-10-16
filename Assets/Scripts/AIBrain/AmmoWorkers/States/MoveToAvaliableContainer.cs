using AI;
using AIBrain;
using UnityEngine;
using UnityEngine.AI;

namespace States
{
    public class MoveToAvaliableContainer :  IState
    {
        #region Constructor

        private NavMeshAgent _agent;
        private Animator _animator;
        private float _movementSpeed;
        private AmmoWorkerBrain _workerBrain;
        public MoveToAvaliableContainer(NavMeshAgent agent, Animator animator, float movementSpeed,AmmoWorkerBrain ammmoWorkerBrain)
        {
            _agent = agent;
            _animator = animator;
            _movementSpeed = movementSpeed;
            _workerBrain = ammmoWorkerBrain;
        }

        #endregion

        #region State
        public void OnEnter()
        {   

            _agent.speed = _movementSpeed;
            _animator.SetTrigger("Walk");
            _agent.SetDestination(_workerBrain.TargetAmmoDropZone.transform.position);
        }

        public void OnExit()
        {
            _workerBrain.IsAmmoWorkerReachedWareHouse = false;
        }

        public void Tick()
        {

        } 
        #endregion
    }
}