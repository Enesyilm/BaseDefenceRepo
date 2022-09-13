using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Data.UnityObjects
{
    public class PoolHandler : MonoBehaviour
    {

        #region Self Varaibles

        #region Public Variables

       
        #endregion
        #region Public Variables

        [Header("EnemyPools")] [SerializeField]
        private List<CD_PoolObjectBase> PoolObjects;
        [Header("BulletPools")] [SerializeField]
        private List<CD_PoolObjectBase> BulletPoolObject;

        #endregion
        #region Serialized Variables

        

        #endregion
        

        #endregion

        private void Awake()
        {
            InitializePools();
        }

        private void InitializePools()
        {
            
            foreach (var enemyPoolObject in PoolObjects)
            {
                enemyPoolObject.InitPool(this);
            }
        }


        #region Event Subscription

        private void OnEnable()
        {
             
        }

        #endregion
    }
}