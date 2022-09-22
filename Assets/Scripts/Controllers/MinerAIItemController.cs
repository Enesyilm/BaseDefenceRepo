using System;
using System.Collections.Generic;
using System.Linq;
using Enum;
using UnityEngine;

namespace Controllers
{
    public class MinerAIItemController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public Dictionary<MinerItems, GameObject> ItemList = new Dictionary<MinerItems, GameObject>();
       
        

        #endregion
        #region Serialized Variables

        [SerializeField] private GameObject pickaxe;
        [SerializeField] private GameObject gem;

        #endregion
        #endregion

        private void Awake()
        {
            AddToDictionary();
            CloseAllObject();
            
        }

        private void CloseAllObject()
        {
            for (int index = 0; index < ItemList.Count; index++)
            {
                ItemList.ElementAt(index).Value.SetActive(false);
            }
        }

        private void AddToDictionary()
        {
            ItemList.Add(MinerItems.Gem, gem);
            ItemList.Add(MinerItems.Pickaxe, pickaxe);
        }

        public void OpenItem(MinerItems currentItem)
        {
            if (MinerItems.None != currentItem)
            {
                ItemList[currentItem].SetActive(true);
            }
        }
        public void CloseItem(MinerItems currentItem)
        {
            if (MinerItems.None != currentItem)
            {
               ItemList[currentItem].SetActive(false);
            }
        }
    }
}