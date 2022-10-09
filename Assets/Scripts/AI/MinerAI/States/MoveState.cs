using AI.MinerAI;
using Enum;
using Managers;
using UnityEngine;
using UnityEngine.AI;

namespace AI.States
{
    public class MoveState:IState
    {
        public bool IsReachedToTarget=>isReachedToTarget;
        private bool isReachedToTarget=false;
        private MinerAnimationStates _minerAnimationStates;
        private MinerItems _minerItems;
        private HostageManager _hostageManager;
        private MinerAIBrain _minerAIBrain;
        public MoveState(MinerAIBrain minerAIBrain, HostageManager hostageManager, MinerAnimationStates minerAnimationStates,
            MinerItems minerItems)
        {
            _minerAIBrain = minerAIBrain;
            _minerItems = minerItems;
            _minerAnimationStates = minerAnimationStates;
            _hostageManager = hostageManager;

        }

        public void Tick()
        {
             if (_minerAIBrain.CurrentTarget != null)
             {
                 MoveToTarget();
                RotateToTarget();
                if (_minerAIBrain.transform.position == _minerAIBrain.ManipulatedTarget)
                {
                    isReachedToTarget = true;
                }
                else
                {
                    isReachedToTarget = false;
                }
             }
        }
       
        private void MoveToTarget()
        {
            
            _minerAIBrain.transform.position = Vector3.MoveTowards(_minerAIBrain.transform.position,_minerAIBrain.ManipulatedTarget,_minerAIBrain.CurrentAISpeed/10);
        }

        private void RotateToTarget()
        {
            if (_minerAIBrain.CurrentTarget.position- _minerAIBrain.transform.position != Vector3.zero)
            {
                _minerAIBrain.transform.forward =_minerAIBrain.CurrentTarget.position- _minerAIBrain.transform.position;
            }
        }

        public void OnEnter()
        {
            _minerAIBrain.MinerAIItemController.OpenItem(_minerItems);
            _hostageManager.ChangeAnimation(_minerAnimationStates);
           
        }
      
        public void OnExit()
        {
           
            isReachedToTarget = false;
        }
    }
}