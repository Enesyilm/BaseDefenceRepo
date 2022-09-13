using System.Collections;
using System.Collections.Generic;
using AIBrains.EnemyBrain;
using UnityEngine;
using Managers;

public class EnemyPhysicsController : MonoBehaviour
{
    private Transform _detectedPlayer;
    private Transform _detectedMine;
    private EnemyAIBrain _enemyAIBrain;
    public bool IsPlayerInRange() => _detectedPlayer != null;
    public bool IsBombInRange() => _detectedMine != null;
    private void Awake()
    {
        _enemyAIBrain = this.gameObject.GetComponentInParent<EnemyAIBrain>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _detectedPlayer = other.GetComponentInParent<PlayerManager>().transform;

            //sinyalle çakmayı dene
            _enemyAIBrain.PlayerTarget = other.transform.parent.transform;
        }

        /*if (other.GetComponent<Mine>())
        {
            _detectedMine = other.GetComponent<Mine>();
        }*/
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _detectedPlayer = null;
            this.gameObject.GetComponentInParent<EnemyAIBrain>().PlayerTarget = null;
        }

        /*if (other.GetComponent<Mine>())
        {

        }*/
    }
}