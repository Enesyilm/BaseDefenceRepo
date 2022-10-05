using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using Buyablezone.Interfaces;
using Commands;
using Controllers;
using Enum;
using FrameworkGoat;
using Interfaces;
using Signals;
using Unity.Mathematics;
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

        [Header("DropZone Settings")] 
        [SerializeField]
        private int maxRowSize=3;
        [SerializeField]
        private int maxColumnSize=3;
        [SerializeField]
        private int maxHeigthSize=3;
        [Header("Offset Settings")] 
        [SerializeField]
        private float heigthOffset=0;
        [SerializeField]
        private float rowOffset=0;
        [SerializeField]
        private float columnOffset=0.1f;
        private bool _isStackFull;
        private List<GameObject> Itemlist=new List<GameObject>();
        [SerializeField] private List<Vector3> positionList;
        public bool IsStackFull   // property
        {
            get { return _isStackFull; }   // get method
            set { _isStackFull = value; }  // set method
        }
        

        #endregion
        #region Private Variables

        private DropZonePlacerCommand _dropZonePlacerCommand;
        private DropZoneAnimationCommand _dropZoneAnimationCommand;

        #endregion
        #endregion
        private void Awake()
        {
            _dropZonePlacerCommand = new DropZonePlacerCommand();
            _dropZoneAnimationCommand = new DropZoneAnimationCommand();
            CurrentExpectedTag=ObjectToStacked.tag;
        }

        public void AddNewItemToStack(Transform otherTransform)
        {
            if (!IsStackFull)
            {
                otherTransform.parent = stackHolder;
                //otherTransform.localPosition=Vector3.zero;
                otherTransform.localRotation=Quaternion.Euler(0,0,90);
                
                Itemlist.Add(otherTransform.gameObject);
                Vector3 _target=_dropZonePlacerCommand.DropZonePlacer(this,stackHolder,maxRowSize,maxColumnSize,maxHeigthSize,heigthOffset,columnOffset,rowOffset);
                _dropZoneAnimationCommand.DropZoneAnimation(otherTransform,_target);
                
            }
            else
            {
                DropzoneSignals.Instance.onDropZoneFull?.Invoke(true);
            }
        }

        public void DrainDropZone(Transform playerTransform)
        {
            IsStackFull = false;
            DropzoneSignals.Instance.onDropZoneFull?.Invoke(false);
            for (int index = 0; index < Itemlist.Count; index++)
            {
                Itemlist[index].transform.parent=playerTransform;
                //Itemlist[index].GetComponent<Collider>().enabled = false;
                Itemlist[index].tag = "Collected";
                _dropZonePlacerCommand.ResetDropZone();
                _dropZoneAnimationCommand.DropZoneGetAnimation(Itemlist[index],new Vector3(0,.5f,0));
               
                
            }
            //Score arttirilacak
            Itemlist.Clear();
            
        }

        public void GetStackPositions(List<Vector3> gridPositionsData)
        {
            positionList=gridPositionsData;
        }
    }
}