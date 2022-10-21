using System;
using Data.ValueObjects.PlayerData;
using Enum;
using Managers;
using Signals;
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

        [SerializeField]
        GameObject frame;
        [SerializeField] private PlayerManager playerManager;

        private Camera _camera;
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
                if (_health >= 100&&playerManager.gameObject.layer==LayerMask.NameToLayer("Base"))
                {
                    OnHealthBarVisibility(false);
                }
                else{OnHealthBarVisibility(true);}
               
            } }

        private void Awake()
        {
            
            _defaultWidth=healthBarTransform.localScale.x;
            _camera=Camera.main;
              
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvent();
        }

        private void SubscribeEvent()
        {
            UISignals.Instance.onHealthBarVisibility += OnHealthBarVisibility;
        }

        private void OnHealthBarVisibility(bool arg0)
        {
            frame.SetActive((arg0));
        }

        private void OnDisable()
        {
            UnSubscribeEvent();
        }

        private void UnSubscribeEvent()
        {
            UISignals.Instance.onHealthBarVisibility -= OnHealthBarVisibility;
        }
        #endregion
        private void Start()
        {
            UpdateHealthBar();
        }

        

        public void SetHealthData(PlayerData playerData)
        {
            _playerData = playerData;
            _health=playerData.PlayerHealth;
        }
        public void UpdateHealthAmount(ScoreTypes scoreType,int damageAmount)
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

        private void Update()=>transform.LookAt(transform.position+_camera.transform.rotation*Vector3.forward,_camera.transform.rotation*Vector3.up);
        

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