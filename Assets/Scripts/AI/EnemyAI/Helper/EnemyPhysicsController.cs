using System;
using System.Collections;
using System.Collections.Generic;
using AIBrains.EnemyBrain;
using Interfaces;
using UnityEngine;
using Managers;

public class EnemyPhysicsController : MonoBehaviour,IDamageable
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
            _detectedMine = other.transform;
            _enemyAIBrain.MineTarget = _detectedMine;
        }
        // if (other.CompareTag("Player"))
        // {
        //    
        //     _enemyAIBrain.PlayerTarget = other.transform.parent.transform;
        // }
        if(other.TryGetComponent( out IDamager iDamager))
        {
           
            if (_enemyAIBrain.Health <= 0) return;
            var damage = iDamager.GetDamage();
            _enemyAIBrain.Health -= damage;
            if (_enemyAIBrain.Health == 0)
            {
                IsDead = true;
                Debug.Log("IsDead = "+IsDead);
            }
            //TakeDamage(damage);
        }
        
    }
    // private void OnTriggerExit(Collider other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //         this.gameObject.GetComponentInParent<EnemyAIBrain>().PlayerTarget = null;
    //     }
    // }

    public bool IsTaken { get; set; }
    public bool IsDead { get; set; }
    public int TakeDamage(int damage)
    {
        
        if (_enemyAIBrain.Health > 0)
        {
            _enemyAIBrain.Health -= damage;
           
            if (_enemyAIBrain.Health == 0)
            {
                IsDead = true;
                return _enemyAIBrain.Health;
            }
            return _enemyAIBrain.Health;
        }
        return 0;
    }

    public Transform GetTransform()
    {
        return transform;
    }
}