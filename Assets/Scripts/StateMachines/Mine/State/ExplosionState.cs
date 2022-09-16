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
        public bool IsExplosionHappened => _timer>=0.3f;
        public ExplosionState(MineBrain mineBrain)
        {
            _mineBrain = mineBrain;
        }
        public void Tick()
        {Debug.Log("explosionstate");
            _timer += Time.deltaTime;
        }

        public void OnEnter()
        {
            

            _mineBrain.mineManager.ExplosionColliderState(true);
            //isExplosionHappened=true;
            ResetTimer();
        }

        public void OnExit()
        {
            _mineBrain.mineManager.ExplosionColliderState(false);
            isExplosionHappened=false;
            ResetTimer();
            
        }
        private void ResetTimer()
        {
            _timer = 0;
        }
    }
}