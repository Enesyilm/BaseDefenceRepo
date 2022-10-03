using System.Collections;
using System.Collections.Generic;
using AI;
using AIBrains.EnemyBrain;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState:IState
{
    private readonly EnemyAIBrain _enemyAIBrain;
    private readonly NavMeshAgent _navMeshAgent;
    private readonly Animator _animator;
    private readonly float _attackRange;
    private readonly float _chaseSpeed;
    private bool _attackOnPlayer; 
    private static readonly int Speed = Animator.StringToHash("Speed");
    public bool isPlayerInRange() => _attackOnPlayer;
    
    public ChaseState(NavMeshAgent navmeshAgent, Animator animator,EnemyAIBrain enemyAIBrain,float attackRange,float chaseSpeed)
    {
        _animator = animator;
        _attackRange = attackRange;
        _chaseSpeed = chaseSpeed;
        _enemyAIBrain = enemyAIBrain;
        _navMeshAgent = navmeshAgent;
    }


    public void Tick()
    {
        if (_enemyAIBrain.PlayerTarget!=null)
        {
            _navMeshAgent.destination = _enemyAIBrain.PlayerTarget.transform.position;
           
        }
        _animator.SetFloat(Speed, _navMeshAgent.velocity.magnitude);
        CheckAttackDistance();
    }

    public void OnEnter()
    {
        _animator.SetTrigger("Chase");
        _navMeshAgent.speed=6f;

        _attackOnPlayer = false;
        _navMeshAgent.speed = _chaseSpeed;
        if (_enemyAIBrain.PlayerTarget != null)
        {
            _navMeshAgent.SetDestination(_enemyAIBrain.PlayerTarget.transform.position);
        }
    }

    public void OnExit()
    {
    }

    private void CheckAttackDistance()
    {
        if (_navMeshAgent.remainingDistance <= _attackRange)
        {
            
            _attackOnPlayer = true;
        }
    }
}
