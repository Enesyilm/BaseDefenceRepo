using System;
using Buyablezone.Interfaces;
using Controllers;
using Interfaces;
using UnityEngine;

namespace Managers
{
    public class DropZoneManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        public string CurrentExpectedTag;
        
        #endregion

        #region Serialized Variables

        [SerializeField]
        private GameObject ObjectToStacked;
        [SerializeField]
        private Transform stackHolder;


        #endregion
        #region Private Variables


        #endregion
        #endregion
        private void Awake()
        {
            CurrentExpectedTag=ObjectToStacked.tag;
        }

        public void AddNewItemToStack(GameObject otherGameObject)
        {
            otherGameObject.transform.parent = stackHolder;
            
        }
    }
}