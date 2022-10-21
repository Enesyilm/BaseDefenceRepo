using Enum;
using Interfaces;
using Signals;
using UnityEngine;

public class BulletFireController : IGetPoolObject
{
    private WeaponTypes _weaponType;
    public BulletFireController(WeaponTypes weaponType)
    {
        _weaponType = weaponType;
    }
    public GameObject GetObjectType(PoolObjectType poolName)
    {
        var obj =  PoolSignals.Instance.onGetObjectFromPool.Invoke(poolName);
        return obj;
    }
    public void FireBullets(Transform holderTransform)
    {
        Debug.Log("_weaponType.ToString()"+_weaponType.ToString());
        var poolType = (PoolObjectType)System.Enum.Parse(typeof(PoolObjectType),_weaponType.ToString());
        var bullet = GetObjectType(poolType);
        Debug.Log("holder"+holderTransform);
        Debug.Log("bullet"+bullet);
        bullet.transform.position = holderTransform.position;
        bullet.transform.rotation = holderTransform.rotation;
    }

   
}