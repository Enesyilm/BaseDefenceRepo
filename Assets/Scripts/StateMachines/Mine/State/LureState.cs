using AI;
using Managers;
using UnityEngine;

namespace StateMachines.State
{
    public class LureState:IState
    {
        private MineBrain _mineBrain;
        private float timer=0;
        public bool IsTimerDone => timer >=_mineBrain.mineManager.LureTime;
        public LureState(MineBrain mineBrain)
        {
            _mineBrain = mineBrain;
        }
        public void Tick()
        {            Debug.Log("lurestate");

            timer += Time.deltaTime;
        }

        public void OnEnter()
        {
            ResetTimer();
            _mineBrain.mineManager.LureColliderState(true);
        }

        public void OnExit()
        {
            ResetTimer();
            _mineBrain.mineManager.LureColliderState(false);
        }

        private void ResetTimer()
        {
            timer = 0;
        }
    }
}