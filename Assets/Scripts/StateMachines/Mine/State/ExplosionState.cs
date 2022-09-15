using System.Threading;
using AI;
using Managers;
using UnityEngine;

namespace StateMachines.State
{
    public class ExplosionState:IState
    {
        private MineBrain _mineBrain;
        private float _timer;

        private bool isExplosionHappened=false;
        public bool IsExplosionHappened => isExplosionHappened;
        public ExplosionState(MineBrain mineBrain)
        {
            _mineBrain = mineBrain;
        }
        public void Tick()
        {
        }

        public void OnEnter()
        {
            _mineBrain.mineManager.ExplosionColliderState(true);
            isExplosionHappened=true;
        }

        public void OnExit()
        {
            Debug.Log("Onexit explosion");
            _mineBrain.mineManager.ExplosionColliderState(false);
            isExplosionHappened=false;
            
        }
    }
}