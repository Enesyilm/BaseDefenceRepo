// using System;
// using System.Collections;
// using System.Collections.Generic;
// using FrameworkGoat;
// using UnityEngine;
// using UnityEngine.AI;
// using Random = UnityEngine.Random;
//
//     public class EnemySpawner:MonoBehaviour
//     {
//         public Transform Player;
//         public int NumberOfEnemiesToSpawn=5;
//         private NavMeshTriangulation triangulation;
//         Transform stockPile;
//         public float SpawnDelay = 1f;
//         //public EnemySpawnTypes;
//         public List<Enemy> EnemyPrefabs =new List<Enemy>();
//         public List<GameObject> EnemyInstances =new List<GameObject>();
//         public SpawnMethod EnemySpawnMethod = SpawnMethod.RoundRobin;
//
//         public enum SpawnMethod
//         {
//             RoundRobin,
//             Random
//         }
//
//         //private Dictionary<int, ObjectPool> EnemyObjectPools = new Dictionary<int, ObjectPool>();
//
//         ///
//         private void Awake()
//         {
//             for (int index = 0; index < EnemyPrefabs.Count; index++)
//             {
//                  //EnemyObjectPools.Add(index,ObjectPool.CreateInstance(EnemyPrefabs[i],NumberOfEnemiesToSpawn));
//                  
//                 //Pool Initialization
//             }
//             ObjectPoolManager.Instance.AddObjectPool<GameObject>(CreateRedEnemy(),TurnOnCallBack,TurnOffCallback,"EnemyAI",NumberOfEnemiesToSpawn,false);
//             // ObjectPoolManager.Instance.AddObjectPool<GameObject>(CreateGreenEnemy,TurnOnCallBack,TurnOffCallback,"EnemyAI",NumberOfEnemiesToSpawn,false);
//         }
//
//         private void TurnOffCallback(GameObject obj)
//         {
//             obj.SetActive(false);
//         }
//
//         private void TurnOnCallBack(GameObject obj)
//         {
//             obj.SetActive(true);
//         }
//
//         private Func<GameObject> CreateRedEnemy1() => () => Instantiate(EnemyPrefabs[0].EnemyPrefab);
//         private Func<GameObject> CreateRedEnemy()
//         {
//              GameObject CreateRedEnemyInstantiate()
//             {
//                 return Instantiate(EnemyPrefabs[0].EnemyPrefab);
//             }
//              return CreateRedEnemyInstantiate;
//         }
//         private Func<bool> CreateGreenEnemy()
//         {
//             bool CreateRedEnemyInstantiate()
//             {
//                 return stockPile!=null&&Vector3.Distance(transform.position,stockPile.position)<1f;
//             }
//             return CreateRedEnemyInstantiate;
//         }
//
//         private void Start()
//         {
//             triangulation=NavMesh.CalculateTriangulation();
//             StartCoroutine(SpawnEnemies());
//         }
//
//         private IEnumerator SpawnEnemies()
//         {
//             WaitForSeconds Wait = new WaitForSeconds(SpawnDelay);
//             int SpawnedEnemies = 0;
//             while (SpawnedEnemies < NumberOfEnemiesToSpawn)
//             {
//                 if (EnemySpawnMethod == SpawnMethod.RoundRobin)
//                 {
//                     SpawnRoundRobinEnemy(SpawnedEnemies);
//                 }
//                 else
//                 {
//                     SpawnRandomEnemy();
//                     
//                 }
//
//                 SpawnedEnemies++;
//                 yield return Wait;
//             }
//         }
//
//         private void SpawnRoundRobinEnemy(int SpawnedEnemies)
//         {
//             int SpawnIndex = SpawnedEnemies % EnemyPrefabs.Count;
//             DoSpawnEnemy(SpawnIndex);
//         }
//
//         private void SpawnRandomEnemy()
//         {
//             DoSpawnEnemy(Random.Range(0, EnemyPrefabs.Count));
//         }
//
//         private void DoSpawnEnemy(int SpawnIndex)
//         {
//
//               //poolableObject = EnemyObjectPools[SpawnIndex].GetObject();
//               GameObject poolObject = ObjectPoolManager.Instance.GetObject<GameObject>("EnemyAI");
//               EnemyInstances.Add(poolObject);
//             if (poolObject != null)
//             {
//                 Enemy enemy = poolObject.GetComponent<Enemy>();
//                 Debug.Log("poolObject");
//                 int VertexIndex = Random.Range(0, triangulation.vertices.Length);
//                 NavMeshHit hit;
//                 if (NavMesh.SamplePosition(triangulation.vertices[VertexIndex], out hit, 2f, -1))
//                 {
//                     Debug.Log("NavMesh if");
//                     enemy.Agent.Warp(hit.position);
//                     enemy.Movement.Player=Player;
//                     enemy.Agent.enabled = true;
//                     enemy.Movement.StartChasing();
//                 }
//                 else
//                 {
//                     
//                 }
//
//             }
//             else
//             {
//                 
//             }
//         }
//      
//     }