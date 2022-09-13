using System;
using System.Collections.Generic;
using AI;
using AI.EnemyAI;
using AI.States;
using Data.UnityObjects;
using Data.ValueObjects.AiData;
using Data.ValueObjects.FrontyardData;
using Enum;
using Managers;
using StateMachines.State;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace StateMachines
{
    public class Mine : MonoBehaviour
    {

        #region Self Variables

        #region Public Variables

        

        #endregion

        #region Serialized Variables

        [SerializeField]
        private MineManager mineManager; 

        #endregion

        #region Private Variables

        

        #endregion

        #endregion
        private StateMachine _stateMachine;

        [SerializeField]
        private Animator animator;

        private void Awake()
        {
            EnemyTypeData= GetEnemyData();
            
            GetStatesReferences();
           
        }
        
        private BombData GetEnemyData()=>Resources.Load<CD_Level>("Data/CD_Level").LevelDatas[0].FrontyardData.Bomb[0];

        private void GetStatesReferences()
        {
            var readyState = ReadyState();
            var mineCountDownState = MineCountDownState();
            _stateMachine = new StateMachine();
           At(readyState,mineCountDownState,mineManager.IsPayedTotalAmount);
            At(chase, move, HasTargetNull());
            _stateMachine.AddAnyTransition(move, () => death.isDead);
            At(moveToBomb, attack, () => moveToBomb.BombIsAlive);
            
            _stateMachine.SetState(search);
            void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);

            
            Func<bool> IsMineBought() => () => mineManager.IsPayedTotalAmount;
           
        }


        private void Update() => _stateMachine.Tick();

       
    }
}