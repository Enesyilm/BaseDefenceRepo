using UnityEngine;

namespace AI.EnemyAI
{
    public class MeshController : MonoBehaviour
    {
        [SerializeField]
        private SkinnedMeshRenderer _renderer;
        public void ChangeColor(Color enemyColor)
        {
            _renderer.material.color=enemyColor;

        }
    }
}