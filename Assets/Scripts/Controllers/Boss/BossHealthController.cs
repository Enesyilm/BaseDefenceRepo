using Data.ValueObjects.AiData.EnemyData;
using DG.Tweening;
using Enum;
using Interfaces;
using Signals;
using StateMachines.AIBrain.Enemy;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers.AIControllers.BossAIControllers
{
    public class BossHealthController: MonoBehaviour
    {
       [SerializeField]
        private RectTransform healthBarTransform;

        [SerializeField] 
        private Image healthBarImage;
        [SerializeField]
        private TextMeshProUGUI playerHealthText;

        [SerializeField]
        GameObject frame;
        [SerializeField] private BossEnemyBrain bossEnemyBrain;

        private Camera _camera;
        private int _health;
        private float _defaultWidth;
        private bool isAlreadyDead;
        private int _maxHealth =500;
        public EnemyTypeData EnemyTypeData;
        public int Health { get
            {
                return _health;
            }
            set
            {
                _health=Mathf.Clamp(value, 0,_maxHealth );
                bossEnemyBrain.Health = _health;
                if (_health <= 0&&!isAlreadyDead)
                {
                    OnHealthBarVisibility(false);
                    
                    bossEnemyBrain.Health = _health;
                    bossEnemyBrain.BossDeath();
                }
            } }

        private void Awake()
        {
            
            _defaultWidth=1;
            Debug.Log("_defaultWidth"+healthBarTransform.localScale.x);
            _camera=Camera.main;
              
        }

        private void OnHealthBarVisibility(bool arg0)
        {
            frame.SetActive((arg0));
        }
        
        private void Start()
        {
            UpdateHealthBar();
        }

        

        public void SetHealthData(EnemyTypeData enemyTypeData)
        {
            EnemyTypeData = enemyTypeData;
            _health=enemyTypeData.Health;
            UpdateHealthBar();
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

        private void Update()=>transform.LookAt(-transform.position+_camera.transform.rotation*Vector3.forward,_camera.transform.rotation*Vector3.up);
        

        private void UpdateHealthBar()
        {
            
            if ((_maxHealth / 3)*2 <= _health)
            {
             healthBarImage.color=Color.green;   
            }
            else if ((_maxHealth / 3) <= _health)
            {
                healthBarImage.color=Color.yellow;   
            }
            else
            {
                healthBarImage.color=Color.red;   
            }
            playerHealthText.text = _health.ToString();
            healthBarTransform.localScale=new Vector3(_health*(_defaultWidth / EnemyTypeData.Health),healthBarTransform.localScale.y,healthBarTransform.localScale.z);
        }
    }
}