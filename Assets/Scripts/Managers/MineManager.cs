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
using Signals;
using UnityEngine;

namespace Managers
{
    public class MineManager : MonoBehaviour, IDamager,IBuyable

    {

    #region Self Variables

    #region Public Variables

    public bool IsPayedTotalAmount=false;
    public int LureTime = 5;
    

    #endregion

    #region Serialized Variables

    [SerializeField] private MinePhysicsController minePhysicsController;
    [SerializeField] private int explosionRange = 10;
    [SerializeField] private int explosionDamage = 999;

    [SerializeField]
    private ParticleSystem ExplosionParticle;

    #endregion

    #region Private Variables
    public List<EnemyAIBrain>  _enemyAIBrains;
    private BuyableZoneDataList _buyableZoneList=new BuyableZoneDataList(0,10);
    
    #endregion

    #endregion

    public void OpenLureRange(bool _state)
    {
        minePhysicsController.gameObject.SetActive(_state);
    }
    public void ClearExplosionList()
    {
        ExplosionParticle.Play();

        for (int index = 0; index < _enemyAIBrains.Count; index++)
        {
            if (InExplosionRange(_enemyAIBrains[index]))
            {
                _enemyAIBrains[index].Health=0;
            }
            _enemyAIBrains[index].MineTarget = null;
        }
        foreach (var enemyAIBrain in _enemyAIBrains)
        {
            //enemyAIBrain.AmIDead = InExplosionRange(enemyAIBrain);
            
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
        int _gemAmount=ScoreSignals.Instance.onGetScore.Invoke(ScoreVariableType.TotalGem);
        if ( _gemAmount> 0)
        {
            ScoreSignals.Instance.onUpdateGemScore?.Invoke(ScoreTypes.DecScore);
            return true;
                
        }
        else
        {
            return false;
        }
    }
    #endregion

    }
}