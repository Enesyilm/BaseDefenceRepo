using AI.MinerAI;
using Enum;
using Managers;
using UnityEngine;

namespace AI.States
{
    public class MinerIdleState:IState

    {
        private HostageManager _hostageManager;
        private MinerAIBrain _minerAIBrain;
        public MinerIdleState(MinerAIBrain minerAIBrain, HostageManager hostageManager)
        {
            _minerAIBrain = minerAIBrain;
            _hostageManager = hostageManager;
        }

        public void Tick()
        {
            
        }

        public void OnEnter()
        {
            _minerAIBrain.SetTargetForMine();
            _hostageManager.ChangeAnimation(MinerAnimationStates.Idle);
        }

        public void OnExit()
        {
            
        }
    }
}