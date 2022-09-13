using System;
using System.Collections.Generic;
using AI;
using AI.EnemyAI;
using AI.States;
using Data.UnityObjects;
using Data.ValueObjects.AiData;
using Enum;
using Managers;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace AIBrains.EnemyBrain
{
    public class EnemyAIBrain : MonoBehaviour
    {

        #region Self Variables

        #region Public Variables

        

        #endregion

        #region Serialized Variables

        

        #endregion

        #region Private Variables

        

        #endregion

        #endregion
        private StateMachine _stateMachine;

        [SerializeField]
        private Animator animator;
        // Player target,mayin target,taret target
        public Transform TurretTarget;
        public float attackRange;
        public int damage;
        public int health;
        public float chaseSpeed;
        public float moveSpeed;
        public float enemySpeed;
        public Transform MineTarget;
        public EnemyDataHandler enemyDataHandler;
        public Transform playerPosition;
        public EnemyTypeData EnemyTypeData;
        [SerializeField]
        public Transform PlayerTarget;
        public EnemyType EnemyType;
        //public NavMeshAgent _navMeshAgent;
        public Transform _currentTarget;
        public Transform _spawnPosition;
        //[SerializeField] public Transform PlayerTarget;

        private void Awake()
        {
            EnemyTypeData= GetEnemyData();
            SetEnemyData();
            GetStatesReferences();
           
        }

        private void SetEnemyData()
        {
            damage=EnemyTypeData.Damage;
            health=EnemyTypeData.Health;
            attackRange=EnemyTypeData.AttackRange;
            chaseSpeed=EnemyTypeData.ChaseSpeed;
            moveSpeed=EnemyTypeData.MoveSpeed;
            playerPosition=FindObjectOfType<PlayerManager>().transform;
            TurretTarget = EnemyTypeData.TargetList[Random.Range(0, EnemyTypeData.TargetList.Count)];
            _spawnPosition = EnemyTypeData.SpawnPosition;        }

        private void Start()
        {
             
        }

        private EnemyTypeData GetEnemyData()=>Resources.Load<CD_AI>("Data/CD_AI").EnemyAIData.EnemyList[(int)EnemyType];

        private void GetStatesReferences()
        {
            var navmeshAgent = GetComponent<NavMeshAgent>();
            // var animator = GetComponent<Animator>();
            var search = new SearchState(this,navmeshAgent,_spawnPosition);
            var attack = new AttackState(navmeshAgent, animator,this,attackRange);
            var move = new Move(this,navmeshAgent, animator,moveSpeed);
            var death = new DeathState(navmeshAgent, animator);
            var chase = new ChaseState(navmeshAgent, animator,this,attackRange,chaseSpeed);
            var moveToBomb = new MoveToBombState(navmeshAgent, animator);

            _stateMachine = new StateMachine();
            At(search, move, HasTurretTarget()); // player chase range
            At(move, chase, HasTarget()); // player chase range
            At(chase, attack, AmIAttackPlayer()); // remaining distance < 1f
            At(attack, chase, ()=>attack.InPlayerAttackRange()==false); // remaining distance > 1f
            At(chase, move, HasTargetNull());
            _stateMachine.AddAnyTransition(death, () => death.isDead);
            _stateMachine.AddAnyTransition(move, () => death.isDead);
            At(moveToBomb, attack, () => moveToBomb.BombIsAlive);
            
            _stateMachine.SetState(search);
            void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);

            Func<bool> HasTarget() => () => PlayerTarget != null;
            Func<bool> HasTurretTarget() => () => TurretTarget != null;
            Func<bool> HasTargetNull() => () => PlayerTarget is null;
            Func<bool> AmIAttackPlayer() => () => PlayerTarget != null && chase.isPlayerInRange();
            //Func<bool> AttackOffRange() => () => attack.InPlayerAttackRange()==false;
        }


        private void Update() => _stateMachine.Tick();

        // private bool CheckDistanceWithPlayer(Vector3 enemyDistance)
        // {
        //     //Vector3.Distance(enemyDataHandler.PlayerPos.position,enemyDistance)<chaseRange;
        // }
    }
}