using System;
using System.Collections;
using System.Collections.Generic;
using AIBrains.EnemyBrain;
using Interfaces;
using UnityEngine;
using Managers;

public class EnemyPhysicsController : MonoBehaviour
{
    private Transform _detectedPlayer;
    private Transform _detectedMine;
    private EnemyAIBrain _enemyAIBrain;
    private bool _amIDead=false;
    public bool IsPlayerInRange() => _detectedPlayer != null;
    public bool AmIDead() => _amIDead;
    public bool IsBombInRange() => _detectedMine != null;
    private void Awake()
    {
        _enemyAIBrain = this.gameObject.GetComponentInParent<EnemyAIBrain>();
    }

   

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MineLure"))
        {
            _detectedMine = other.transform;
            _enemyAIBrain.MineTarget = _detectedMine;//ActiveInHierarchy de var mi bunu check etsin
        }
        if (other.CompareTag("Player"))
        {
            _detectedPlayer = other.GetComponentInParent<PlayerManager>().transform;
            //sinyalle çakmayı dene
            _enemyAIBrain.PlayerTarget = other.transform.parent.transform;
        }

        if (other.CompareTag("Bullet"))
        {
            int damageAmount = other.GetComponent<IDamageable>().GetDamage();
            _enemyAIBrain.health =_enemyAIBrain.health-damageAmount;
            if(_enemyAIBrain.health<=0){
                _amIDead = true;
            }
        }
        if (other.CompareTag("MineExplosion"))
        {
            int damageAmount = other.transform.parent.GetComponentInParent<IDamageable>().GetDamage();
            _enemyAIBrain.health =_enemyAIBrain.health-damageAmount;
            if(_enemyAIBrain.health<=0){
                _amIDead = true;
                //AmIDead();
            }

        }

       

        /*if (other.GetComponent<Mine>())
        {
            _detectedMine = other.GetComponent<Mine>();
        }*/
    }

    private void OnTriggerExit(Collider other)
    {
       //On TriggerExite gerek yok bunun yerine state degistirirsem
        if (other.CompareTag("Player"))
        {
            _detectedPlayer = null;
            this.gameObject.GetComponentInParent<EnemyAIBrain>().PlayerTarget = null;
        }

        if (other.CompareTag("MineLure"))
        {
            _detectedMine = null;
            _enemyAIBrain.MineTarget = _detectedMine;//ActiveInHierarchy de var mi bunu check etsin
            _enemyAIBrain.MineTarget = null;
        }
        

        /*if (other.GetComponent<Mine>())
        {

        }*/
    }
}