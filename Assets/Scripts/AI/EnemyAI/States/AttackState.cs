using AIBrains.EnemyBrain;
using Commands;
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
        private FindRandomPointOnCircleCommand _findRandomPointOnCircleCommand=new FindRandomPointOnCircleCommand();

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
                _navMeshAgent.destination =_enemyAIBrain.PlayerTarget.position+_enemyAIBrain.transform.TransformDirection(new Vector3(0,0,-1.5f));
                // _enemyAIBrain.transform.forward =
                //     _enemyAIBrain.PlayerTarget.position - _enemyAIBrain.transform.position;

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
           // Vector3 target=_findRandomPointOnCircleCommand.FindRandomPointOnCircle(_enemyAIBrain.PlayerTarget.position,1.5f);
            //_navMeshAgent.SetDestination(target);
            //Stoping distance
            
            //_navMeshAgent.SetDestination((_enemyAIBrain.PlayerTarget.transform.position));
            _navMeshAgent.SetDestination(_enemyAIBrain.PlayerTarget.position+_enemyAIBrain.transform.TransformDirection(new Vector3(0,0,-1.5f)));
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