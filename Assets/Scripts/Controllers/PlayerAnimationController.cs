using Enums;
using UnityEngine;

namespace Controllers
{
    public class PlayerAnimationController1 : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        private PlayerAnimationTypes _currentAnimType=PlayerAnimationTypes.Idle;

        public void PlayAnimation(PlayerAnimationTypes _playerAnimationTypes)
        {
            if (_currentAnimType!=_playerAnimationTypes)
            {
                _currentAnimType = _playerAnimationTypes;
                animator.SetTrigger(_playerAnimationTypes.ToString());
                
            }
        }
    }
}