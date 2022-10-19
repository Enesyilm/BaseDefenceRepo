using System;
using Data.ValueObjects.PlayerData;
using Enum;
using Managers;
using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;

namespace Controllers.Player
{
    public class PlayerHealthController:MonoBehaviour
    {
        [SerializeField]
        private RectTransform healthBarTransform;

        [SerializeField] 
        private Image healthBarImage;
        [SerializeField]
        private TextMeshProUGUI playerHealthText;
        private int _health;
        private float _defaultWidth;
        private PlayerData _playerData;
        public int Health { get
            {
                return _health;
            }
            set
            {
                _health=Mathf.Clamp(value, 0, 100);
                if (_health <= 0)
                {
                    playerManager.PlayerDeath();
                }
            } }

        private void Awake()
        {
            
            _defaultWidth=healthBarTransform.localScale.x;
              
        }

        private void Start()
        {
            UpdateHealthBar();
        }

        [SerializeField] private PlayerManager playerManager;

        public void SetHealthData(PlayerData playerData)
        {
            _playerData = playerData;
            _health=playerData.PlayerHealth;
        }
        public void UpdateHealth(ScoreTypes scoreType,int damageAmount)
        {
            if (scoreType == ScoreTypes.DecScore)
            {
                Health-=damageAmount;
                
            }
            else
            {
                Health += damageAmount;
                
            }

            UpdateHealthBar();
        }

        private void UpdateHealthBar()
        {
            
            if ((int)PlayerHealthColorStates.Green <= _health)
            {
             healthBarImage.color=Color.green;   
            }
            else if ((int)PlayerHealthColorStates.Yellow <= _health)
            {
                healthBarImage.color=Color.yellow;   
            }
            else
            {
                healthBarImage.color=Color.red;   
            }
            //playerHealthText.text = _health.ToString();
            healthBarTransform.localScale=new Vector3(_health*(_defaultWidth / _playerData.PlayerHealth),healthBarTransform.localScale.y,healthBarTransform.localScale.z);
        }
    }
}