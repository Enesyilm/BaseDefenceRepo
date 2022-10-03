using AI;
using Commands;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrains.EnemyBrain
{
    public class EnemyMoveState : IState
    {
        private readonly EnemyAIBrain _enemyAIBrain;
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Animator _animator;
        
        private readonly float _moveSpeed;
        private static readonly int Speed = Animator.StringToHash("Speed");
        public EnemyMoveState(EnemyAIBrain enemyAIBrain,NavMeshAgent agent, Animator animator,float moveSpeed)
        {
            _navMeshAgent = agent;
            _moveSpeed = moveSpeed;
            _enemyAIBrain = enemyAIBrain;
            _animator = animator;
        }

        public void Tick()
        {
            _animator.SetFloat(Speed, _navMeshAgent.velocity.magnitude);
        }

        public void OnEnter()
        {
            _navMeshAgent.enabled = true;
            _navMeshAgent.speed = _moveSpeed;
            _animator.SetTrigger("Chase");
            //_animator.SetBool("Walk", _navMeshAgent.velocity.magnitude > 0.01f);
            _navMeshAgent.SetDestination(_enemyAIBrain.TurretTarget.position);
            _navMeshAgent.speed=3f;
        }

        public void OnExit()
        {
           // _navMeshAgent.enabled = false;
        }
    }
}