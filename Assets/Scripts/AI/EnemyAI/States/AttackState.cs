using AIBrains.EnemyBrain;
using UnityEngine;
using UnityEngine.AI;

namespace AI.States
{
    public class AttackState:IState
    {
        #region Self Variables

        #region Public Variables

        

        #endregion

        #region Serialized Variables

        

        #endregion

        #region Private Variables
        private readonly Animator _animator;
        private readonly NavMeshAgent _navMeshAgent;
        private readonly float _attackRange;
        private readonly EnemyAIBrain _enemyAIBrain;

        #endregion
        #endregion

        public bool _inAttack;
        public bool InPlayerAttackRange() => _inAttack;
        public AttackState(NavMeshAgent navMeshAgent,Animator animator,EnemyAIBrain enemyAIBrain,float attackRange)
        {
            _navMeshAgent = navMeshAgent;
            _animator = animator;
            _enemyAIBrain = enemyAIBrain;
            _attackRange = attackRange;
        }
        public void Tick()
        {
            if (_enemyAIBrain.PlayerTarget)
            {
                _navMeshAgent.destination = _enemyAIBrain.PlayerTarget.transform.position;
               
            }
            else
            {
                _inAttack = false;
            }
            CheckAttackDistance();
        }

        public void OnEnter()
        {
            _animator.SetTrigger("Attack");
            _inAttack = true;
            _navMeshAgent.SetDestination(_enemyAIBrain.PlayerTarget.transform.position);
        }

        public void OnExit()
        {
        }
        private void CheckAttackDistance()
        {
            if (_navMeshAgent.remainingDistance > _attackRange)
            {
                _inAttack = false;
            }
        }
    }
}