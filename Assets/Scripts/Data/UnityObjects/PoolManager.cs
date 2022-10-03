using System.Collections.Generic;
using UnityEngine;

namespace Data.UnityObjects
{
    public class PoolManager : MonoBehaviour
    {

        #region Self Varaibles

        #region Public Variables

       
        #endregion
        #region Public Variables
        [Header("EnemyPools")] [SerializeField]
        private List<CD_PoolObjectBase> PoolScriptableObjects;

        [Header("Enemy Pools Lists")] [SerializeField]
        private List<List<GameObject>> PoolObjectsLists=new List<List<GameObject>>();
        
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
            for (int index = 0; index < PoolScriptableObjects.Count; index++)
            {
                PoolObjectsLists.Add(new List<GameObject>());
                PoolScriptableObjects[index].InitPool(index,this,PoolObjectsLists[index]);
            }
           
        }
    }
}