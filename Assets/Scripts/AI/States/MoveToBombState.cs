using UnityEngine;
using UnityEngine.AI;

namespace AI.States
{
    public class MoveToBombState:IState

    {
        private readonly Animator _animator;
        private readonly NavMeshAgent _navMeshAgent;
        public bool BombIsAlive => bomb != null;
        Transform bomb; 
        public MoveToBombState(NavMeshAgent _navMeshAgent,Animator _animator)
        {
            this._navMeshAgent = _navMeshAgent;
            this._animator = _animator;
        }
    public void Tick()
    {

    }

    public void OnEnter()
    {

    }

    public void OnExit()
    {
    }
    }
}