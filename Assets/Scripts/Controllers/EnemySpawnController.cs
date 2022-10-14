using System;
using System.Collections;
using Enum;
using Interfaces;
using Signals;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class EnemySpawnController : MonoBehaviour, IGetPoolObject
        {
            #region Self Variables

            #region Serialized Variables

            #endregion

            #region Public Variables

            public int NumberOfEnemiesToSpawn = 50;

            public float SpawnDelay = 2;
            private IGetPoolObject _getPoolObjectImplementation;

            #endregion

            #region Private Variables


            #endregion
            #endregion

            private void Awake()
            {
                //StartCoroutine(SpawnEnemies());
            }

            private void Start()
            {
               StartCoroutine(SpawnEnemies());
            }

            private IEnumerator SpawnEnemies()
            {
                WaitForSeconds wait = new WaitForSeconds(SpawnDelay);

                int spawnedEnemies = 0;

                while (spawnedEnemies < NumberOfEnemiesToSpawn)
                {
                    DoSpawnEnemy();

                    spawnedEnemies++;

                    yield return wait;
                }
            }

            private void DoSpawnEnemy()
            {
                int randomType;

                int randomPercentage = Random.Range(0, 101);

                if (randomPercentage<=15)
                {
                    randomType = (int)PoolObjectType.BigRedEnemy;
                }
                else if (15< randomPercentage && randomPercentage <=50)
                {
                    randomType = (int)PoolObjectType.RedEnemy;
                }
                else
                {
                    randomType = (int)PoolObjectType.OrangeEnemy;

                }

                var obj= GetObject(((PoolObjectType)randomType));
            }

            public GameObject GetObject(PoolObjectType poolName)
            {
                return PoolSignals.Instance.onGetObjectFromPool?.Invoke(poolName);
            }

            public GameObject GetObjectType(PoolObjectType poolType)
            {
                return PoolSignals.Instance.onGetObjectFromPool?.Invoke(poolType);
            }
        }

    }