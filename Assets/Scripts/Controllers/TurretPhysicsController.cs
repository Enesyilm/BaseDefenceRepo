using Managers;
using UnityEngine;

namespace Controllers
{
    public class TurretPhysicsController : MonoBehaviour
    {

        [SerializeField] private TurretManager _turretManager;
        [SerializeField] private BoxCollider boxCollider;


        private void OnTriggerEnter(Collider other)
        {


            if (other.TryGetComponent(typeof(PlayerManager), out Component getterGameObject))
            {
                Debug.Log("Player carptı");
                _turretManager.IsEnterUser();
            }

            if (other.CompareTag("Enemy"))
            {

                Debug.Log("vurdu");
                _turretManager.IsEnemyEnterTurretRange(other.gameObject);

            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(typeof(EnemyPhysicsController), out Component enemy))
            {
                _turretManager.IsEnemyExitTurretRange();
            }
        }

        private void OnTriggerStay(Collider other)
        {
           if (other.CompareTag("Enemy"))
           {
               _turretManager.IsFollowEnemyInTurretRange();
           }
        }
    }
}
