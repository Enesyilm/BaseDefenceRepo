using System.Threading;
using AI;
using Enum;
using Managers;
using UnityEngine;

namespace StateMachines.State
{
    public class ExplosionState:IState
    {
        private MineBrain _mineBrain;
        private float _timer;

        private bool isExplosionHappened=false;
        public bool IsExplosionHappened => _timer>=0.5f;
        public ExplosionState(MineBrain mineBrain)
        {
            _mineBrain = mineBrain;
        }
        public void Tick()
        {
            _timer += Time.deltaTime;
        }

        public void OnEnter()
        {

            _mineBrain.mineManager.ClearExplosionList();
            
            //isExplosionHappened=true;
            ResetTimer();
        }

        public void OnExit()
        {
           
            isExplosionHappened=false;
            ResetTimer();
            
        }
        private void ResetTimer()
        {
            _timer = 0;
        }
    }
}