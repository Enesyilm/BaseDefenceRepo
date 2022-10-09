using System;
using System.Collections.Generic;
using System.Linq;
using AI.MinerAI;
using Controllers;
using Data.UnityObjects;
using Data.ValueObjects;
using Enum;
using Interfaces;
using Signals;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class MineBaseManager : MonoBehaviour,IGetPoolObject
    {
        #region Self Variables

        #region Public Variables



        #endregion

        #region Serialized Variables

        [SerializeField] private TextMeshPro gemWorkerText;
        [SerializeField] private MineBaseTextController mineBaseTextController;
        [SerializeField]
        private List<Transform> mineLocations;
        [SerializeField]
        private List<Transform> cartLocations;

        [SerializeField]
        private Transform gemHolderPosition;
        [SerializeField]
        private Transform instantiatePosition;



        #endregion

        #region Private Variables

        private int _currentLevel; //LevelManager uzerinden cekilecek
        private int _currentWorkerAmount;
        private int _maxWorkerAmount;
        private Dictionary<MinerAIBrain, GameObject> _mineWorkers = new Dictionary<MinerAIBrain, GameObject>();
        private MineBaseData _mineBaseData;
        
        

        #endregion

        #endregion
        private void Awake()
        {
            _mineBaseData=GetMineBaseData();
            AssignDataValues();
          
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            MineBaseSignals.Instance.onGetRandomMineTarget += GetRandomMineTarget;
            MineBaseSignals.Instance.onGetGemHolderPos += OnGetGemHolderPos;
            MineBaseSignals.Instance.onNewMineWorkerAdd += OnNewMineWorkerAdd;
            InitializeDataSignals.Instance.onLoadMineBaseData += OnLoadMineBaseData;
        }

        private void OnLoadMineBaseData(MineBaseData MineBaseData)
        {
            _mineBaseData=MineBaseData;
            InstantiateAllMiners();
             UpdateMinerText();
             AssignMinerValuesToDictionary();
        }
        
        private void OnNewMineWorkerAdd(MinerAIBrain minerBrainAi)
        {
            _currentWorkerAmount++;
            _mineBaseData.CurrentWorkerAmount = _currentWorkerAmount;
            InitializeDataSignals.Instance.onSaveMineBaseData?.Invoke(_mineBaseData);
            _mineWorkers.Add(minerBrainAi,minerBrainAi.gameObject);
            UpdateMinerText();
        }


        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void UnSubscribeEvents()
        {
            MineBaseSignals.Instance.onGetRandomMineTarget -= GetRandomMineTarget;
            MineBaseSignals.Instance.onGetGemHolderPos -= OnGetGemHolderPos;
            MineBaseSignals.Instance.onNewMineWorkerAdd -= OnNewMineWorkerAdd;
            
        }

        private Transform OnGetGemHolderPos()
        {
            return gemHolderPosition;
        }

        #endregion


        private void Start()
        {
            //InstantiateAllMiners();
           
          
        }
       

        private void InstantiateAllMiners()
        {
            for (int index = 0; index < _currentWorkerAmount; index++)
            {
               
               GameObject _currentObject=GetObjectType(PoolObjectType.MinerAI);
                MinerAIBrain _currentMinerAIBrain=_currentObject.GetComponent<MinerAIBrain>();
                _currentObject.transform.position = instantiatePosition.position;
                _mineWorkers.Add(_currentMinerAIBrain,_currentObject);
            }
        }

        private void UpdateMinerText()
        {
            mineBaseTextController.UpdateMineWorkerAmountText(gemWorkerText,_currentWorkerAmount,_maxWorkerAmount);
        }

        private void AssignMinerValuesToDictionary()
        {
            for (int index = 0; index < _mineWorkers.Count; index++)
            {
                _mineWorkers.ElementAt(index).Key.GemHolder= gemHolderPosition;
            }
            
        }

        private void AssignDataValues()
        {
            _currentWorkerAmount =_mineBaseData.CurrentWorkerAmount;
            _maxWorkerAmount = _mineBaseData.MaxWorkerAmount;
        }

       
        public Tuple<Transform,GemMineType> GetRandomMineTarget()
        {
            int randomMineTargetIndex=Random.Range(0, mineLocations.Count + cartLocations.Count);
            return randomMineTargetIndex>= mineLocations.Count
                ? Tuple.Create(cartLocations[randomMineTargetIndex % cartLocations.Count],GemMineType.Cart)
                :Tuple.Create(mineLocations[randomMineTargetIndex],GemMineType.Mine);//Tuple ile enum donecek maden tipine gore animasyon degisecek stateler uzerinden
        }


        public MineBaseData GetMineBaseData() => Resources.Load<CD_Level>("Data/CD_Level").LevelDatas[_currentLevel].BaseData.MineBaseData;
        public GameObject GetObjectType(PoolObjectType poolType)
        {
            return PoolSignals.Instance.onGetObjectFromPool?.Invoke(poolType);
        }
    }
}