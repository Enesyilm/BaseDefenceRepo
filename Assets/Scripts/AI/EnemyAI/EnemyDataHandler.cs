using System;
using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace AI.EnemyAI
{
    public class EnemyDataHandler:MonoBehaviour
    {
        public List<Transform> turretPosition;
        public Transform PlayerPos;

        private void Awake()
        {
            
            //PlayerPos = FindObjectOfType<PlayerManager>().transform;
            //Turret Konumlari sinyalle gelecek
            
        }
       
    }
}