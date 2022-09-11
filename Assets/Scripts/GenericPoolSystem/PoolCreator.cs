// using System;
// using System.Collections.Generic;
// using Enum;
// using FrameworkGoat;
// using UnityEngine;
// using Enum;
// using Object = UnityEngine.Object;
//
// namespace GenericPoolSystem
// {
//     public class PoolCreator:MonoBehaviour
//     {
//         [SerializeField]
//         private int InitialEnemyStock;
//         [SerializeField]
//         private bool IsDynamic;
//         [SerializeField]
//         private GameObject PoolObjectInstance;
//         [SerializeField]
//         private List<GameObject> CreatedInstances;
//
//         private void Awake()
//         {
//             InitPool();
//         }
//
//         public void InitPool()
//         {
//             
//             ObjectPoolManager.Instance.AddObjectPool<GameObject>(FabricateGameObject,
//                 TurnOnEnemy,TurnOffEnemy,InitialEnemyStock,IsDynamic);
//         }
//         
//         public void GetFromPool()
//         {
//             CreatedInstances.Add(ObjectPoolManager.Instance.GetObject<GameObject>(pooltype.ToString()));
//         }
//         public void TurnOnEnemy(GameObject gameObject)
//         {
//             gameObject.SetActive(true);
//         }
//         public void TurnOffEnemy(GameObject gameObject)
//         {
//             gameObject.SetActive(false);
//         }
//         public GameObject FabricateGameObject()
//         {
//             return Instantiate(PoolObjectInstance,Vector3.zero,Quaternion.identity,transform);
//
//         }
//     }
// }