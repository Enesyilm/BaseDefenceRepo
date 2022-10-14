using System;
using System.Collections.Generic;
using AI.MinerAI;
using Data.UnityObjects;
using Data.ValueObjects;
using Data.ValueObjects.LevelData;
using DG.Tweening;
using Enum;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class HostageBaseManager : MonoBehaviour
    {
        public List<HostageManager> StackedHostageList=new List<HostageManager>();
        private MinerAnimationStates _currentAnimType=MinerAnimationStates.Idle;
        [SerializeField] private GameObject hostageInstance;

        [SerializeField]
        private Transform militaryWaitTarget;
        [SerializeField] private HostageStackController hostageStackController;
        private HostageData _data;
        private int _maxHostileCount;
        private MineBaseData _mineBaseData;
        private List<Transform> _hostagePositionList;
        

        [SerializeField]
        //private HostageStackManager hostageStackManager;
        private void Awake()
        {
            _data = GetHostageData();
            _mineBaseData=GetMineBaseData();
            ChangeHostageAnimation(MinerAnimationStates.Crouch);

        }

        #region EventSubscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            HostageSignals.Instance.onSendHostageToMineBase += OnSendHostageToMineBase;
            HostageSignals.Instance.onSendHostageStackToMilitaryBase += OnSendHostageStackToMilitaryBase;
            InputSignals.Instance.onInputTakenActive += OnInputTaken;
            HostageSignals.Instance.onAddHostageStack += OnAddHostageStack;
        }
        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void UnSubscribeEvents()
        {
            HostageSignals.Instance.onSendHostageStackToMilitaryBase -= OnSendHostageStackToMilitaryBase;
            InputSignals.Instance.onInputTakenActive -= OnInputTaken;
            HostageSignals.Instance.onSendHostageToMineBase -= OnSendHostageToMineBase;
            HostageSignals.Instance.onAddHostageStack -= OnAddHostageStack;
        }

        private void OnSendHostageStackToMilitaryBase()
        {
            hostageStackController.SendToMilitaryGate(militaryWaitTarget);
           ClearStack();
        }

        private void ClearStack()
        {
            
            hostageStackController.ClearStack();
        }
        private void GetHostageManagerComponent()
        {
            foreach (var hostage in StackedHostageList)
            {
                hostage.minerAIBrain.enabled=true;

            }
        }

        private void OnInputTaken(bool arg0)
        {
            if (arg0)
            {
                ChangeHostageAnimation(MinerAnimationStates.Walk);
            }
            else
            {
                ChangeHostageAnimation(MinerAnimationStates.Idle);
            }
        }

        private void OnAddHostageStack(HostageManager mineManager)
        {
            hostageStackController.AddHostageStack(mineManager);
        }

        #endregion
        private HostageData GetHostageData()=>Resources.Load<CD_Level>("Data/CD_Level").LevelDatas[0/*Take from levelManager*/].FrontyardData.HostageData;
        private MineBaseData GetMineBaseData()=>Resources.Load<CD_Level>("Data/CD_Level").LevelDatas[0/*Take from levelManager*/].BaseData.MineBaseData;
        private void Start()
        {
            AssignHostileValuesToDictionary();
            InstantiateHostage();
        }
        private void InstantiateHostage()
        {
            int _remainingHostageAmount=_mineBaseData.MaxWorkerAmount - _mineBaseData.CurrentWorkerAmount;
            for (int index = 0; index <_remainingHostageAmount ; index++)
            {
                //GameObject hostage=ObjectPoolManager.Instance.GetObject<GameObject>(PoolObjectType.MinerAI.ToString());
                GameObject hostage=Instantiate(hostageInstance);
                hostage.GetComponent<HostageManager>();
                hostageInstance.transform.position = _hostagePositionList[index].position;
               
            }
        }
        private void AssignHostileValuesToDictionary()
        {
            _hostagePositionList=_data.HostagePlaces;
        }

        private void OnSendHostageToMineBase(Vector3 centerOfGatePos)
        {
            // if (hostageStackController.SendToGate(centerOfGatePos))
            // {
            //     GetHostageManagerComponent();
            //     ClearStack();
            // };
            //int index = 0;
            for (int index = 0; index < StackedHostageList.Count; index++)
            {
                if (MineBaseSignals.Instance.onNewMineWorkerAdd.Invoke(StackedHostageList[index].minerAIBrain))
                {
                    StackedHostageList[index].ChangeAnimation(MinerAnimationStates.Walk);
                    StackedHostageList[index].transform.DOMove(centerOfGatePos, 1f + 2 * index / 10f);
                    ActivateMinerAI(StackedHostageList[index]);
                    StackedHostageList.RemoveAt(index);
                    StackedHostageList.TrimExcess();
                    index++;
                    
                };
            }
            // foreach (var hostage in StackedHostageList)
            // {
            //     if (MineBaseSignals.Instance.onNewMineWorkerAdd.Invoke(hostage.minerAIBrain))
            //     {
            //         hostage.ChangeAnimation(MinerAnimationStates.Walk);
            //         hostage.transform.DOMove(centerOfGatePos, 1f + 2 * index / 10f);
            //         ActivateMinerAI(hostage);
            //         StackedHostageList.RemoveAt(index);
            //         StackedHostageList.TrimExcess();
            //         index++;
            //         
            //     };
            // }
        }

        private void ActivateMinerAI(HostageManager hostage)
        {
            hostage.minerAIBrain.enabled = true;
        }

        public void ChangeHostageAnimation(MinerAnimationStates hostageAnimationType)
        {
            if (_currentAnimType!=hostageAnimationType)
            {
                
                _currentAnimType = hostageAnimationType;
                foreach (var stackedHostage in StackedHostageList)
                {
                    stackedHostage.ChangeAnimation(hostageAnimationType);
                }
                
            }
          
        }

        public void AddHostageToList(HostageManager hostage)
        {
            StackedHostageList.Add(hostage);
            _currentAnimType = MinerAnimationStates.Crouch;
            ChangeHostageAnimation(MinerAnimationStates.Walk);
        }
    }
}