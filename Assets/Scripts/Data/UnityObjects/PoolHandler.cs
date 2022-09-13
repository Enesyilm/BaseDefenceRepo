using System.Collections.Generic;
using UnityEngine;

namespace Data.UnityObjects
{
    public class PoolHandler : MonoBehaviour
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

        #region Deneme

        // public void StartSpawnEnemies(int Spawnpossibility,int initalAmount,int SpawnDelay,GameObject ObjectType,List<GameObject> GoList)
        // {
        //     StartCoroutine(SpawnEnemies(Spawnpossibility, initalAmount, SpawnDelay, ObjectType, GoList));
        // }
        //
        // #region Event Subscription
        //
        // private void OnEnable()
        // {
        //      
        // }
        //
        // #endregion
        // public IEnumerator SpawnEnemies(int Spawnpossibility,int initalAmount,int SpawnDelay,GameObject ObjectType,List<GameObject> GoList)
        // {
        //     
        //     WaitForSeconds wait = new WaitForSeconds(SpawnDelay);
        //     int spawnedEnemies = 0;
        //     while (spawnedEnemies < initalAmount)
        //     {
        //         DoSpawnEnemy(Spawnpossibility,initalAmount,SpawnDelay,ObjectType,GoList);
        //         spawnedEnemies++;
        //         yield return wait;
        //     }
        // }
        //
        // private void DoSpawnEnemy(int Spawnpossibility,int initalAmount,int SpawnDelay,GameObject ObjectType,List<GameObject> GoList)
        // {
        //     int randomPercentage = Random.Range(1, 101);
        //     if (randomPercentage < Spawnpossibility)
        //     {
        //         var go = ObjectPoolManager.Instance.GetObject<GameObject>(ObjectType.name);
        //         GoList.Add(go);
        //         //ObjectPoolManager.Instance.GetObject<GameObject>(ObjectType.name);
        //         //GetObject(ObjectType.name);
        //         
        //     }
        // }

        #endregion
    }
}