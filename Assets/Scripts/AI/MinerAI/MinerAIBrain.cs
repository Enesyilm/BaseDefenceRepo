using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AI.States;
using Commands;
using Controllers;
using Data.UnityObjects;
using Data.ValueObjects;
using Data.ValueObjects.AiData;
using Enum;
using Managers;
using Signals;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions.Must;

namespace AI.MinerAI
{
    public class MinerAIBrain : MonoBehaviour
    {

        #region SelfVariables
        public Vector3 ManipulatedTarget;
        #region Public Variables
        public float CurrentAISpeed=3;
        public Transform CurrentTarget;
        public GemMineType CurrentTargetType;
        public Transform GemHolder;
        public HostageManager hostageManager;
        public float GemCollectionOffset=5;
        public bool IsDropZoneFullStatus
        {
            get => isDropZoneFull;
            set => isDropZoneFull = value;
        }

        private bool isDropZoneFull;


        

        #endregion

        #region Serialized Variables

        [SerializeField] private Animator animator;

        [SerializeField] MineWorkerData mineWorkerData;


        #endregion

        #region Private Variables

        private StateMachine _stateMachine;
        private FindRandomPointOnCircleCommand _findRandomPointOnCircleCommand;
        public MinerAIItemController MinerAIItemController;

        //public List<var>

        #endregion

        #endregion
        private void Awake()
        {
            mineWorkerData=GetMinerData();
            InitDataValues();
            _findRandomPointOnCircleCommand=new FindRandomPointOnCircleCommand();
            GetStatesReferences();
        }

        private void InitDataValues()
        {
            CurrentAISpeed = mineWorkerData.MineWorkerSpeed;
        }

        private void Start()
        {
            SetTargetForMine();
        }

        public MineWorkerData GetMinerData()
        {
            return Resources.Load<CD_AI>("Data/CD_AI").MineWorkerData;
        }
        public void SetTargetForMine()
        {
            GemHolder = MineBaseSignals.Instance.onGetGemHolderPos?.Invoke();
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
            var minerReadyState = new MinerReadyState();
            var moveToMine = new MoveState(this,hostageManager,MinerAnimationStates.Walk,MinerItems.None);
            var moveToGemHolder = new MoveState(this,hostageManager,MinerAnimationStates.CarryGem,MinerItems.Gem);
            var mineGemSourceState=new GemSourceState(this,hostageManager,MinerAnimationStates.MineGemSource,MinerItems.Pickaxe);
            var cartGemSourceState=new GemSourceState(this,hostageManager,MinerAnimationStates.CartGemSource,MinerItems.None); 
            var idleState=new MinerIdleState(this,hostageManager); 
            var dropGemState=new DropGemState(this); 
            _stateMachine = new StateMachine();
            At(minerReadyState,moveToMine,IsGameStarted());
            At(moveToMine,mineGemSourceState,()=>moveToMine.IsReachedToTarget&&CurrentTargetType==GemMineType.Mine);//su iki state tek move stat oldugu icin tekrar tekrar calisiyor
            At(moveToMine,cartGemSourceState,()=>moveToMine.IsReachedToTarget&&CurrentTargetType==GemMineType.Cart);//su iki state tek move stat oldugu icin tekrar tekrar calisiyor
            At(mineGemSourceState,moveToGemHolder,()=>mineGemSourceState.IsMiningTimeUp);
            At(cartGemSourceState,moveToGemHolder,()=>cartGemSourceState.IsMiningTimeUp);
            At(moveToGemHolder,dropGemState,()=>moveToGemHolder.IsReachedToTarget);
            At(dropGemState,moveToMine,()=>dropGemState.IsGemDropped);
            _stateMachine.AddAnyTransition(idleState,IsDropZoneFull());
            At(idleState,moveToMine,IsDropZoneNotFull());
            _stateMachine.SetState(minerReadyState);
            void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);
            Func<bool> IsDropZoneFull() => () => isDropZoneFull;
            Func<bool> IsGameStarted() => () => true;
            Func<bool> IsDropZoneNotFull() => () => isDropZoneFull==false;
        }

        private void Update() => _stateMachine.Tick();
    }
}