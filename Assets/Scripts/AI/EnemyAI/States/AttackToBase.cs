using AIBrains.EnemyBrain;
using UnityEngine;
using UnityEngine.AI;

namespace AI.EnemyAI.States
{
    public class AttackToBase:IState
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
                public AttackToBase(NavMeshAgent navMeshAgent,Animator animator,EnemyAIBrain enemyAIBrain,float attackRange)
                {
                    _navMeshAgent = navMeshAgent;
                    _animator = animator;
                    _enemyAIBrain = enemyAIBrain;
                    _attackRange = attackRange;
                }
                public void Tick()
                {
                    CheckAttackDistance();
                }
        
                public void OnEnter()
                {
                    _animator.SetTrigger("Attack");
                    _inAttack = true;
                    _enemyAIBrain.transform.rotation = new Quaternion(0, 0, .5f,0);
                }
        
                public void OnExit()
                {
                    
                }
                private void CheckAttackDistance()
                {
                    if (_navMeshAgent.remainingDistance < _attackRange)
                    {
                        _inAttack = false;
                    }
                }
    }
}