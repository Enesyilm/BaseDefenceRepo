using System.Collections;
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
        
        public override void InitPool(MonoBehaviour m)
        {
            /* Ya monobehaviour constructor gonderir halledersin yada PoolHandler gonderip gereken fonksiyonu buradan cagirirsin
             Actionda gonderilebilir bunun uzerinden boylece sinyalleri buradaki fonksiyonlara baglamis oluruz*/
            base.InitPool(m);
            m.StartCoroutine(SpawnEnemies());

        }
        
        public IEnumerator SpawnEnemies()
        {
            //var List<>
            WaitForSeconds wait = new WaitForSeconds(SpawnDelay);
            int spawnedEnemies = 0;
            while (spawnedEnemies < initalAmount)
            {
                DoSpawnEnemy();
                spawnedEnemies++;
                yield return wait;
            }
        }

        private void DoSpawnEnemy()
        {
            int randomPercentage = Random.Range(1, 101);
            if (randomPercentage < Spawnpossibility)
            {
                var go = ObjectPoolManager.Instance.GetObject<GameObject>(ObjectType.name);
                CreatedObjects.Add(go);
                //ObjectPoolManager.Instance.GetObject<GameObject>(ObjectType.name);
                //GetObject(ObjectType.name);
                
            }
        }
    }
}