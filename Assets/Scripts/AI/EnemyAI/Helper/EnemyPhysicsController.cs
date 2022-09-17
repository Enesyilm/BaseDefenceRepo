using System;
using System.Collections;
using System.Collections.Generic;
using AIBrains.EnemyBrain;
using Interfaces;
using UnityEngine;
using Managers;

public class EnemyPhysicsController : MonoBehaviour
{
    private Transform _detectedMine;
    private EnemyAIBrain _enemyAIBrain;
    private bool _amIDead=false;

    public bool AmIDead() => _amIDead;

    private void Awake()
    {
        _enemyAIBrain = this.gameObject.GetComponentInParent<EnemyAIBrain>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MineLure"))
        {
            Debug.Log("EnemyLurea girdi");
            _detectedMine = other.transform;
            _enemyAIBrain.MineTarget = _detectedMine;
        }
        if (other.CompareTag("Player"))
        {
            
            _enemyAIBrain.PlayerTarget = other.transform.parent.transform;
        }
        if (other.CompareTag("Bullet"))
        {
            int damageAmount = other.GetComponent<IDamager>().GetDamage();
            _enemyAIBrain.Health -=damageAmount;
            if(_enemyAIBrain.Health<=0){
                _amIDead = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.gameObject.GetComponentInParent<EnemyAIBrain>().PlayerTarget = null;
        }
    }
}