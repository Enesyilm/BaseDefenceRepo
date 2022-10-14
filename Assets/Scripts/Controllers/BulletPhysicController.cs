using Data.ValueObjects.WeaponData;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class BulletPhysicController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField] 
        private BulletManager bulletManager;

        #endregion

        #region Private Variables

        private int _damage;

        #endregion

        #endregion

        public void GetData(WeaponData data)
        {
            _damage = data.Damage;
        }
        public int Damage()
        {
            return _damage;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamageable idDamagable))
            {
                bulletManager.SetBulletToPool();
            }
        }
    }
}