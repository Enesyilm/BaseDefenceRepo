using System;
using System.Collections;
using System.Collections.Generic;
using AIBrains.EnemyBrain;
using Data.UnityObjects;
using Data.ValueObjects;
using Enum;
using FrameworkGoat;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Managers
{
    public class EnemySpawnManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        
        [SerializeField]private List<GameObject> enemies = new List<GameObject>();
        
        [SerializeField]private List<Transform> targetList = new List<Transform>();
        [SerializeField]
        private float mapLimit;

        #endregion
    
        #region Public Variables
        
        public int NumberOfEnemiesToSpawn = 50;
        
        public float SpawnDelay = 2;


        #endregion

        #region Private Variables

        
        private EnemyType enemyType;
        
        private List<EnemyAIBrain> enemyScripts = new List<EnemyAIBrain>();
        public List<Transform> spawnPoint;//Mevcut Level IDsinden cekilecek
        
        private NavMeshTriangulation triangulation;
        
        private EnemyAIBrain _EnemyAIBrain;


        

        #endregion
        #endregion
        

        private void InitEnemyPool()
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                ObjectPoolManager.Instance.AddObjectPool(()=>Instantiate(enemies[i]),TurnOnEnemyAI,TurnOffEnemyAI,((EnemyType)i).ToString(),50,true);
            }
            
        }

        private void Awake()
        {   
            InitEnemyPool();

            StartCoroutine(SpawnEnemies());
        }
        
        private void TurnOnEnemyAI(GameObject enemy)
        {
            enemy.transform.position= new Vector3(Random.Range(-mapLimit, mapLimit),enemy.transform.position.y,spawnPoint[/*Mevcut Level Ids*/0].position.z);
            enemy.SetActive(true);
        }

        private void TurnOffEnemyAI(GameObject enemy)
        {
            enemy.SetActive(false);
        }
        private void ReleaseEnemyObject(GameObject go,Type t)
        {
            ObjectPoolManager.Instance.ReturnObject(go,t.ToString());
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

            int randomType = Random.Range(0, System.Enum.GetNames(typeof(EnemyType)).Length);
            int randomPercentage = Random.Range(0, 101);
            if (randomType == (int)EnemyType.BigRed)
            {
                if (randomPercentage<30)
                {
                    randomType = (int)EnemyType.Red;
                    Debug.Log(randomType);
                }
            } 
            ObjectPoolManager.Instance.GetObject<GameObject>(((EnemyType)randomType).ToString());
           
        }
    }
}