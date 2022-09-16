using AI;
using UnityEngine;

namespace StateMachines.State
{
    public class ReadyState:IState
    {
        public ReadyState()
        {
            
        }

        public void Tick()
        {
            Debug.Log("reasdystate");

        }

        public void OnEnter()
        {

        }

        public void OnExit()
        {
            
        }
    }
}