using System;
using System.Collections.Generic;
using Data.UnityObject;
using UnityEngine;
using Data.UnityObjects;
using Data.ValueObject;
using Data.ValueObjects;
using Enum;
using Enums;
using UnityEngine.Rendering;
using Sirenix.OdinInspector;
using Signals;

namespace Managers
{

    public class PoolManager : MonoBehaviour
    {

        #region Self Variables

        #region Public Variables


        #endregion

        #region Serialized Variables

        #endregion

        #region Private Variables
        [ShowInInspector]
        private SerializedDictionary<PoolObjectType,PoolData> _data;
        private int _listCountCache;
        #endregion


        #endregion

        private void Awake()
        {
            _data = GetData();
            InitializePools();
        }

        #region Event Subscription


        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            PoolSignals.Instance.onGetObjectFromPool += OnGetObjectFromPoolType;
            PoolSignals.Instance.onReleaseObjectFromPool += OnReleaseObjectFromPool;

        }
        private void UnsubscribeEvents()
        {
            PoolSignals.Instance.onGetObjectFromPool -= OnGetObjectFromPoolType;
            PoolSignals.Instance.onReleaseObjectFromPool -= OnReleaseObjectFromPool;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private GameObject OnGetObjectFromPoolType(PoolObjectType poolType)
        {
            _listCountCache = (int)poolType;
           return ObjectPoolManager.Instance.GetObject<GameObject>(poolType.ToString());
        }
        private void OnReleaseObjectFromPool( GameObject obj,PoolObjectType poolType)
        {
            _listCountCache = (int)poolType;
            ObjectPoolManager.Instance.ReturnObject<GameObject>(obj, poolType.ToString());
        }

        private SerializedDictionary<PoolObjectType, PoolData> GetData()
        {
            return Resources.Load<CD_Pool>("Data/CD_Pool").PoolDataDic;
        }

        private void InitializePools()
        {
            for (int index = 0; index < _data.Count; index++)
            {
                _listCountCache = index;
                InitPool(((PoolObjectType)index), _data[((PoolObjectType)index)].initalAmount, _data[((PoolObjectType)index)].isDynamic);
            }

        }

        public void InitPool(PoolObjectType poolType, int initalAmount, bool isDynamic)
        {
            ObjectPoolManager.Instance.AddObjectPool<GameObject>(FactoryMethod, TurnOnObject, TurnOffObject, poolType.ToString(), initalAmount, isDynamic);
        }


        public void TurnOnObject(GameObject obj)
        {
            if (obj != null)
            {
                obj.SetActive(true);
            }
        }
        public void TurnOffObject(GameObject obj)
        {
            obj.SetActive(false);
        }

        public GameObject FactoryMethod()
        {
            var go = Instantiate(_data[((PoolObjectType)_listCountCache)].ObjectType,this.transform);
            return go;
        }
    }

}