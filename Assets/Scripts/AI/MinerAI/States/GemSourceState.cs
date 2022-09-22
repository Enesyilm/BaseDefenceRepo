using AI.MinerAI;
using Enum;
using UnityEngine;

namespace AI.States
{
    public class GemSourceState:IState
    {
        private Animator _animator;
        public bool IsMiningTimeUp=>_timer>=_minerAIBrain.GemCollectionOffset;
        private MinerAIBrain _minerAIBrain;
        private MinerItems _minerItems;
        private MinerAnimationStates _minerAnimationState;
        private float _timer;
        public GemSourceState(MinerAIBrain minerAIBrain, Animator animator,
            MinerAnimationStates minerAnimationState, MinerItems minerItems)
        {
            _minerAIBrain = minerAIBrain;
            _minerItems = minerItems;
            _animator = animator;
            _minerAnimationState = minerAnimationState;
        }

        public void Tick()
        {
            _timer += Time.deltaTime;
        }

        public void OnEnter()
        {
        _minerAIBrain.MinerAIItemController.OpenItem(_minerItems);
        _animator.SetTrigger(_minerAnimationState.ToString());
        
        }

        public void OnExit()
        {
            ResetTimer();
            _minerAIBrain.MinerAIItemController.CloseItem(_minerItems);
            _minerAIBrain.SetTargetForGemHolder();
        }

        private void ResetTimer()
        {
            _timer = 0;
        }
    }
}