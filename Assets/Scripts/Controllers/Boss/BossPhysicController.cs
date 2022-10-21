using System;
using StateMachines.AIBrain.Enemy;
using UnityEngine;

namespace Controllers.AIControllers.BossAIControllers
{
    public class BossPhysicController : MonoBehaviour,IDamageable
    {
        [SerializeField]
        BossEnemyBrain bossEnemyBrain;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out BulletPhysicController bulletPhysicController))
            {
                bossEnemyBrain.UpdateHealth(bulletPhysicController.GetDamage());
            }
    
        }

        public bool IsTaken { get; set; }
        public bool IsDead { get; set; }
        public int TakeDamage(int damage)
        {
            throw new NotImplementedException();
        }

        public Transform GetTransform()
        {
            return transform;
        }
    }
}