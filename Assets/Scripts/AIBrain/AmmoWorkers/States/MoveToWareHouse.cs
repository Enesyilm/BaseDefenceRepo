using UnityEngine;
using UnityEngine.AI;
using AI;
using AIBrain;

namespace States
{
    public class MoveToWareHouse :IState
    {
        #region Constructor

        private NavMeshAgent _agent;
        private Animator _animator;
        private float _movementSpeed;
        private Transform _ammoWareHousePos;
        private AmmoWorkerBrain _workerBrain;

        public MoveToWareHouse(NavMeshAgent agent, Animator animator, float movementSpeed, Transform ammoWareHousePos,
            AmmoWorkerBrain ammoWorkerBrain)
        {
            _agent = agent;
            _animator = animator;
            _movementSpeed = movementSpeed;
            _ammoWareHousePos = ammoWareHousePos;
            _workerBrain = ammoWorkerBrain;
        }

        #endregion

        #region State
        public void OnEnter()
        {

            _agent.speed = _movementSpeed;
            _agent.SetDestination(_ammoWareHousePos.position);

            _animator.SetTrigger("Walk");
        }

        public void OnExit()
        {
            _workerBrain.IsAmmoWorkerReachedDropzone = false;
        }

        public void Tick()
        {
            
        } 
        #endregion


    }
}