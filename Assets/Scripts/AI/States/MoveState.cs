using AI;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrains.EnemyBrain
{
    public class Move : IState
    {
        private readonly EnemyAIBrain _enemyAIBrain;
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Animator _animator;
        private readonly float _moveSpeed;
        private static readonly int Speed = Animator.StringToHash("Speed");
        public Move(EnemyAIBrain enemyAIBrain,NavMeshAgent agent, Animator animator,float moveSpeed)
        {
            _navMeshAgent = agent;
            _moveSpeed = moveSpeed;
            _enemyAIBrain = enemyAIBrain;
            _animator = animator;
        }

        public void Tick()
        {
        }

        public void OnEnter()
        {
            _navMeshAgent.enabled = true;
            _navMeshAgent.speed = _moveSpeed;
            _animator.SetBool("Walk", _navMeshAgent.velocity.magnitude > 0.01f);
            _navMeshAgent.SetDestination(_enemyAIBrain.TurretTarget.position);
            //_animator.SetFloat(Speed, 1f);
        }

        public void OnExit()
        {
           // _navMeshAgent.enabled = false;
            //_animator.SetFloat(Speed, 0f);
        }
    }
}