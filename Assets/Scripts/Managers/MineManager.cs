using System;
using System.Collections.Generic;
using AIBrains.EnemyBrain;
using Buyablezone.Interfaces;
using Buyablezone.PurchaseParams;
using Controllers;
using Data.UnityObjects;
using Data.ValueObjects;
using Enum;
using Interfaces;
using UnityEngine;

namespace Managers
{
    public class MineManager : MonoBehaviour, IDamager,IBuyable

    {

    #region Self Variables

    #region Public Variables

    public bool IsPayedTotalAmount=false;
    public int GemAmount=9; //Sinyalle Cekilecek Score Manager Uzerinden
    public int LureTime = 5;
    [SerializeField] private int explosionDamage = 999;

    #endregion

    #region Serialized Variables

    [SerializeField] private MinePhysicsController minePhysicsController;
    [SerializeField] private int explosionRange = 10;

    [SerializeField]
    private ParticleSystem ExplosionParticle;

    #endregion

    #region Private Variables
    public List<EnemyAIBrain>  _enemyAIBrains;
    private BombData Data;
    private BuyableZoneDataList _buyableZoneList;
    
    #endregion

    #endregion
    private void Awake()
    {
        Data= GetMineData();
        _buyableZoneList=Data.BuyableZoneDataList;
    }

    private BombData GetMineData()=>Resources.Load<CD_Level>("Data/CD_Level").LevelDatas[0].FrontyardData.Bomb[0];
  
    public void OpenLureRange(bool _state)
    {
        minePhysicsController.gameObject.SetActive(_state);
    }
    public void ClearExplosionList()
    {
        ExplosionParticle.Play();
        Debug.Log("patladı");

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
    
    public int GetDamage()
    {
        return explosionDamage;
    }

    #region IBuyable

    public BuyableZoneDataList GetBuyableData()
    {
        return _buyableZoneList;
    }
    public void TriggerBuyingEvent()
    {
        IsPayedTotalAmount = true;
    }
    public bool MakePayment()
    {
        Debug.Log("Payment"+ GemAmount);
        if (GemAmount > 0)
        {
        GemAmount--;
        return true;
        }
        else
        {
            return false;
        }
        return (GemAmount < 0) ? false : true;
        //Payed Amount have to increase in here
    }
    #endregion

    }
}