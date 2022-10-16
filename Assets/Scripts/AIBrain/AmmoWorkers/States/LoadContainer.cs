
using AI;
using UnityEngine;
using UnityEngine.AI;

namespace States
{
    public class LoadContainer : IState
    {   
        private NavMeshAgent agent;
        public LoadContainer(NavMeshAgent agent, Animator animator, float movementSpeed, Transform ammoWareHouse)
        {
            this.agent = agent;
        }

        public void OnEnter()
        {
            agent.speed = 0;
        }

        public void OnExit()
        {
            
        }

        public void Tick()
        {
            
        }
    }
}