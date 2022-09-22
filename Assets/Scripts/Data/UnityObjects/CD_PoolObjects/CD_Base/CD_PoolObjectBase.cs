using System.Collections.Generic;
using FrameworkGoat;
using UnityEngine;

namespace Data.UnityObjects
{ 
    public abstract class CD_PoolObjectBase :ScriptableObject
    {
        #region Self Variables

        #region Public Variables
            public GameObject ObjectType;
            public int initalAmount;
            private int Id;
        #endregion

        #region Serialized Variables
            [SerializeField]private bool isDynamic;
        #endregion

        #region Private Variables
            public List<GameObject> CreatedObjects;

        #endregion
        

        #endregion

        public virtual void InitPool(int index,PoolHandler m,List<GameObject> PoolObjects)
        {
            Id=index;
            ObjectPoolManager.Instance.AddObjectPool<GameObject>(FactoryMethod,TurnOnObject,TurnOffObject,ObjectType.name,initalAmount,isDynamic);
        }

        public virtual void TurnOnObject(GameObject obj)
        {
            if (obj!=null)
            { 
                obj.SetActive(true);
            }
        }
        public virtual void TurnOffObject(GameObject obj)
        {
            obj.SetActive(false);
        }
        public virtual void ReleaseObject(GameObject obj,string poolName)
        {
            ObjectPoolManager.Instance.ReturnObject(obj,poolName);
        }
        public virtual void GetObject(string poolName)
        {
            Debug.Log("CreatedObject");
            ObjectPoolManager.Instance.GetObject<GameObject>(poolName);
        }

        public virtual GameObject FactoryMethod()
        {    
            var go=Instantiate(ObjectType);
            return go;
        }
    }
}