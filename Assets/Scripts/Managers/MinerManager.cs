using System;
using AI.MinerAI;
using Controllers;
using Data.UnityObjects;
using Data.ValueObjects;
using Enum;
using UnityEngine;

namespace Managers
{
    public class MinerManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        

        #endregion

        #region Serialized Variables

        [SerializeField]
        private MineBaseManager mineBaseManager;

        [SerializeField] private MinerAIItemController minerAIItemController;
        [SerializeField]
        private MinerAIBrain minerAIBrain;


        #endregion

        #region Private Variables

        private int _currentLevel; //LevelManager uzerinden cekilecek
        private Transform _currentMinePlace;


        #endregion

        #endregion
        private void Awake()
        {
            //_currentMinePlace=mineBaseManager.GetRandomMineTarget();
        }
    }
}