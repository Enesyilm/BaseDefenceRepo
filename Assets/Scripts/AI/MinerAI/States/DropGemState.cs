using AI.MinerAI;
using UnityEngine;

namespace AI.States
{
    public class DropGemState:IState
    {
        private readonly MinerAIBrain _minerAIBrain;
        public bool IsGemDropped=>isGemDropped;
        private readonly Animator _animator;
        private bool isGemDropped;

        public DropGemState(MinerAIBrain minerAIBrain, Animator animator)
        {
            _minerAIBrain = minerAIBrain;
            _animator = animator;
        }

        public void Tick()
        {
        
        }

        public void OnEnter()
        {
            isGemDropped = true;
        }

        public void OnExit()
        {
            _minerAIBrain.SetTargetForMine();
        }
    }
}