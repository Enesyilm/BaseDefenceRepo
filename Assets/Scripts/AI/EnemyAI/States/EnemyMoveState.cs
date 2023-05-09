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
        private bool _attackedToBase=false;
        private readonly float _moveSpeed;
        public bool IsTurretInRange() => _attackedToBase;
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
            CheckAttackDistance();
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
        private void CheckAttackDistance()
        {
            if (_navMeshAgent.remainingDistance<=5)
            {
                _attackedToBase = true;
                _animator.Play("Attack");
                //_enemyAIBrain.transform.rotation = new Quaternion(0, -1f,0,0);
                _navMeshAgent.updateRotation=false;
                // _enemyAIBrain.transform.Rotate(new Vector3(0,180,0));
            }
            else
            {
                _navMeshAgent.updateRotation=true;
            }
            
        }
    }
}