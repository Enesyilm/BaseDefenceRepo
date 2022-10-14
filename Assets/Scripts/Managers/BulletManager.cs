using Controllers;
using Data.ValueObjects.WeaponData;
using Enum;
using Enums;
using Interfaces;
using Signals;
using UnityEngine;

namespace Managers
{
    public class BulletManager: MonoBehaviour, IReleasePoolObject
        {
            #region Self Variables
    
            #region Public Variables
    
            #endregion
    
            #region Serialized Variables
    
            [SerializeField] 
            private WeaponTypes weaponType;
            [SerializeField] 
            private BulletPhysicController physicsController;
    
            #endregion
    
            #region Private Variables
    
            private WeaponData _data;
    
            #endregion
    
            #endregion
            private void Awake()
            {
                _data = GetBulletData();
                SetDataToControllers();
            }
            private void OnEnable()
            {
                Invoke(nameof(SetBulletToPool),1f);
            }
            private WeaponData GetBulletData() => Resources.Load<CD_Weapon>("Data/CD_Weapon").WeaponData[(int)weaponType];
            private void SetDataToControllers() => physicsController.GetData(_data);
            public void ReleaseObject(GameObject obj, PoolObjectType poolName)=>PoolSignals.Instance.onReleaseObjectFromPool.Invoke(obj,poolName);
            public void SetBulletToPool()
            {
                var poolName = (PoolObjectType)System.Enum.Parse(typeof(PoolObjectType),weaponType.ToString());
                ReleaseObject(gameObject,poolName);
            }
        }
}