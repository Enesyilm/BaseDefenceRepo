using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using FrameworkGoat;
using UnityEngine;

namespace Data.UnityObjects
{
    public class CD_EnemyPoolBase : CD_PoolObjectBase
    {
        #region Self Variables

        #region Public Variables
        public int SpawnDelay = 1;
        [Range(0,100)]
        public int Spawnpossibility=100;
        #endregion

        #region Serialized Variables
        
        #endregion

        #region Private Variables

        #endregion
        

        #endregion
        
        public override void InitPool(int index,PoolManager m,List<GameObject> PoolObjects)
        {
            /* Ya monobehaviour constructor gonderir halledersin yada PoolHandler gonderip gereken fonksiyonu buradan cagirirsin
             Actionda gonderilebilir bunun uzerinden boylece sinyalleri buradaki fonksiyonlara baglamis oluruz*/
            base.InitPool(index,m,PoolObjects);
            m.StartCoroutine(SpawnEnemies(PoolObjects));
          
        }
        
        public IEnumerator SpawnEnemies(List<GameObject> PoolObjects)
        {
            //var List<GameObject>
            WaitForSeconds wait = new WaitForSeconds(SpawnDelay);
            int spawnedEnemies = 0;
            while (spawnedEnemies < initalAmount)
            {
                DoSpawnEnemy(PoolObjects);
                spawnedEnemies++;
                yield return wait;
            }
        }

        private void DoSpawnEnemy(List<GameObject> PoolObjects)
        {
            int randomPercentage = Random.Range(1, 101);
            if (randomPercentage < Spawnpossibility)
            {
                var go = ObjectPoolManager.Instance.GetObject<GameObject>(ObjectType.name);
                PoolObjects.Add(go);
                //ObjectPoolManager.Instance.GetObject<GameObject>(ObjectType.name);
                //GetObject(ObjectType.name);
                
            }
        }
    }
}