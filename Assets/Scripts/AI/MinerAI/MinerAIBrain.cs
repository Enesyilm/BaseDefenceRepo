using System;
using System.Threading.Tasks;
using AI.States;
using Commands;
using Controllers;
using Enum;
using Signals;
using UnityEngine;
using UnityEngine.AI;

namespace AI.MinerAI
{
    public class MinerAIBrain : MonoBehaviour
    {

        #region SelfVariables
        public Vector3 ManipulatedTarget;
        #region Public Variables
        public Transform CurrentTarget;
        public GemMineType CurrentTargetType;
        public Transform GemHolder;
        public float GemCollectionOffset=5;
        

        

        #endregion

        #region Serialized Variables

        [SerializeField] private Animator animator;
        

        #endregion

        #region Private Variables

        private StateMachine _stateMachine;
        private FindRandomPointOnCircleCommand _findRandomPointOnCircleCommand;
        public MinerAIItemController MinerAIItemController;

        #endregion

        #endregion
        private void Awake()
        {
            _findRandomPointOnCircleCommand=new FindRandomPointOnCircleCommand();
            GetStatesReferences();
        }

        private void Start()
        {
            SetTargetForMine();
        }

        public void SetTargetForMine()
        {
            (CurrentTarget, CurrentTargetType)= MineBaseSignals.Instance.onGetRandomMineTarget?.Invoke();
            ManipulatedTarget= _findRandomPointOnCircleCommand.FindRandomPointOnCircle(CurrentTarget.position,3f);
        }
        public void SetTargetForGemHolder()
        {
            CurrentTarget= GemHolder;
            if (CurrentTarget.position- transform.position != Vector3.zero)
            {
                transform.forward =CurrentTarget.position- transform.position;
            }
            ManipulatedTarget= GemHolder.position+transform.TransformDirection(new Vector3(0,0,-3f));
        }

        private void GetStatesReferences()
        {
            var moveToMine = new MoveState(this,animator,MinerAnimationStates.Walk,MinerItems.None);
            var moveToGemHolder = new MoveState(this,animator,MinerAnimationStates.CarryGem,MinerItems.Gem);
            var mineGemSourceState=new GemSourceState(this,animator,MinerAnimationStates.MineGemSource,MinerItems.Pickaxe);
            var cartGemSourceState=new GemSourceState(this,animator,MinerAnimationStates.CartGemSource,MinerItems.None); 
            var dropGemState=new DropGemState(this,animator); 
            _stateMachine = new StateMachine();
            At(moveToMine,mineGemSourceState,()=>moveToMine.IsReachedToTarget&&CurrentTargetType==GemMineType.Mine);//su iki state tek move stat oldugu icin tekrar tekrar calisiyor
            At(moveToMine,cartGemSourceState,()=>moveToMine.IsReachedToTarget&&CurrentTargetType==GemMineType.Cart);//su iki state tek move stat oldugu icin tekrar tekrar calisiyor
            At(mineGemSourceState,moveToGemHolder,()=>mineGemSourceState.IsMiningTimeUp);
            At(cartGemSourceState,moveToGemHolder,()=>cartGemSourceState.IsMiningTimeUp);
            At(moveToGemHolder,dropGemState,()=>moveToGemHolder.IsReachedToTarget);
            At(dropGemState,moveToMine,()=>dropGemState.IsGemDropped);
            
            _stateMachine.SetState(moveToMine);
            void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);
        }

        private void Update() => _stateMachine.Tick();
    }
}