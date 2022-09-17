using System;
using System.Collections.Generic;
using AIBrains.EnemyBrain;
using Controllers;
using Data.ValueObjects.FrontyardData;
using Enum;
using Interfaces;
using UnityEngine;

namespace Managers
{
    public class MineManager : MonoBehaviour, IDamager

    {

    #region Self Variables

    #region Public Variables

    public bool IsPayedTotalAmount => (_payedGemAmount >= requiredGemAmount);
    public int GemAmount; //Sinyalle Cekilecek Score Manager Uzerinden
    public int LureTime = 5;
    public int MineCountDownTime = 60;
    [SerializeField] private int explosionDamage = 999;

    #endregion

    #region Serialized Variables

    [SerializeField] private MinePhysicsController minePhysicsController;
    [SerializeField] private int requiredGemAmount;
    [SerializeField]
    private int explosionRange = 10;

    #endregion

    #region Private Variables
    public List<EnemyAIBrain>  _enemyAIBrains;
    private int _payedGemAmount = 1000;
    private BombData Data;



    #endregion

    #endregion
    private void Awake()
    {
        //Data= GetMineData();
    }
        
    //private BombData GetMineData()=>Resources.Load<CD_Level>("Data/CD_Level").LevelDatas[0].FrontyardData.Bomb[0];
    public void ShowGemAmountText()
    {

    }

    public void ResetPayedAmount()
    {
        _payedGemAmount = 0;
    }

    public void OpenLureRange(bool _state)
    {
        minePhysicsController.gameObject.SetActive(_state);
    }
    public void ClearExplosionList()
    {
        foreach (var enemyAIBrain in _enemyAIBrains)
        {
            enemyAIBrain.AmIDead = InExplosionRange(enemyAIBrain);
            enemyAIBrain.MineTarget = null;
        }
        _enemyAIBrains.Clear();
    }

    private bool InExplosionRange(EnemyAIBrain enemyAIBrain)
    {
        return (Vector3.Distance(transform.position, enemyAIBrain.transform.position)<explosionRange)
            ?true
            :false;
    }

    public void PayGemToMine()
    {
        GemAmount--;
        _payedGemAmount++;

    }

    public int GetDamage()
    {
        return explosionDamage;
    }
    }
}