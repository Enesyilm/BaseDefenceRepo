using States;
using System;
using System.Collections.Generic;
using System.Linq;
using AI;
using Controllers;
using Data.UnityObjects;
using Data.ValueObjects.AiData;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrain
{
    public class AmmoWorkerBrain : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        public GameObject TargetAmmoDropZone;
        #endregion
        #region SerializeField Variables

        [SerializeField]
        private NavMeshAgent _agent;
        [SerializeField]
        private Animator _animator;
        #endregion

        #region Private Variables 

        private bool _IsAmmoWorkerReachedWareHouse;


        #endregion

        #region State Field

        private MoveToWareHouse _moveToWareHouse;
        private TakeAmmo _takeAmmo;
        private MoveToAvaliableContainer _moveToAvaliableAmmoDropZone;
        private LoadContainer _loadTurret;
        private IdleState _idleState;
        private Create _createWorker;
        private AmmoWorkerAIData _ammoWorkerAIData;
        public AmmoStackerController AmmoStackerController;
        public Transform AmmoLoadArea;
        private StateMachine _statemachine;
        public bool IsAmmoWorkerReachedWareHouse;
        public bool IsAmmoWorkerReachedDropzone;
        public bool IsDropZonesAreFull=false;
        public bool IsDropZonesAreNotFull=false;



        #endregion
        #endregion

        #region GetReferans
        private void Awake()
        {
            _ammoWorkerAIData = GetAmmoWorkerData();
          
        }

        private void Start()
        {
             InitBrain();
        }

        private AmmoWorkerAIData GetAmmoWorkerData()=>Resources.Load<CD_Level>("Data/CD_Level").LevelDatas[0].BaseData.AmmoWorkerAIData;
        public void InitBrain()
        {
            GetStatesReferences();
            TransitionofState();
        }
        
        public  void GetStatesReferences()
        {
            _statemachine = new StateMachine();
            _createWorker = new Create();
            _moveToWareHouse = new MoveToWareHouse(_agent, _animator, _ammoWorkerAIData.MovementSpeed,AmmoLoadArea,this);
            _takeAmmo = new TakeAmmo(_agent,_animator);
            _idleState = new IdleState(_agent,_animator,this);
            _moveToAvaliableAmmoDropZone = new MoveToAvaliableContainer(_agent, _animator, _ammoWorkerAIData.MovementSpeed,this);
            _loadTurret = new LoadContainer(_agent, _animator, _ammoWorkerAIData.MovementSpeed, AmmoLoadArea);

        }



        #endregion

        #region StateEngine

        public void TransitionofState()
        {
   

            #region Transtion

            At(_createWorker, _moveToWareHouse,()=> true);

            At(_moveToWareHouse, _takeAmmo, WhenAmmoWorkerInAmmoWareHouse());

            At(_takeAmmo, _moveToAvaliableAmmoDropZone,WhenAmmoWorkerStackFull());

            At(_moveToAvaliableAmmoDropZone, _loadTurret, IsAmmoWorkerInDropzone());

            At(_loadTurret, _moveToWareHouse, ()=> true );
            _statemachine.AddAnyTransition(_idleState,()=>IsDropZonesAreFull);
            At(_idleState, _moveToWareHouse, ()=>IsDropZonesAreNotFull);

            _statemachine.SetState(_createWorker);

            void At(IState to, IState from, Func<bool> condition) => _statemachine.AddTransition(to, from, condition);

            #endregion

            #region Conditions

            Func<bool> WhenAmmoWorkerInAmmoWareHouse() => () => IsAmmoWorkerReachedWareHouse;

            Func<bool> WhenAmmoWorkerStackFull() => () => TargetAmmoDropZone != null;

            Func<bool> IsAmmoWorkerInDropzone() => () => IsAmmoWorkerReachedDropzone;

            #endregion
        }

        public void Update()
        {

            _statemachine.Tick();
        }
        #endregion



    }
}
