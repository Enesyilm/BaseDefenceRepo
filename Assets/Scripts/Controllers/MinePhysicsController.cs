using System;
using System.Threading.Tasks;
using AIBrains.EnemyBrain;
using DG.Tweening;
using Enum;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class MinePhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private MineManager mineManager;
        [SerializeField] private SphereCollider lureCollider;
        [SerializeField] private SphereCollider explosionCollider;

        #endregion

        #region Private Variables
        private float timer;
        private float payOffset = 0.1f;
        #endregion

        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                mineManager._enemyAIBrains.Add(other.GetComponentInParent<EnemyAIBrain>());

            }
        }
    }
}