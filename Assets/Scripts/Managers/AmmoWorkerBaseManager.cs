using AIBrain;
using Controllers;
using Data.UnityObject;
using Datas.ValueObject;
using Enums;
using Signals;
using System;
using System.Collections.Generic;
using System.Linq;
using Concreate;
using Enum;
using UnityEngine;
using Interfaces;

namespace Managers
{
    public class AmmoWorkerBaseManager : MonoBehaviour,IGetPoolObject
    {
        [SerializeField] //datadan gelcek
        private List<AmmoWorkerBrain> AmmoWorkerBrainList;
        private List<AmmoDropZoneManager> _dropZoneList=new List<AmmoDropZoneManager>();
        [SerializeField]
        private Transform ammoLoadAreaPos;

        private ushort prevIndex=0;

        public bool _isAllDropzonesFull=true;
        private int _maxAmmoCapasity=15;

        internal void SendAmmoToWorker(AmmoWorkerPhysicsController ammoWorkerPhysicsController)
        {   
            GameObject _ammoInstance;
            for (int i = 0; i < _maxAmmoCapasity; i++)
            {
                if (ammoWorkerPhysicsController.ammoStackerController.PositionList.Count <= ammoWorkerPhysicsController.ammoStackerController.StackList.Count)
                {
                    return;
                }
                _ammoInstance=GetObjectType(PoolObjectType.Ammo);
                _ammoInstance.transform.position = transform.position;
                StackableAmmo stackableAmmo=_ammoInstance.GetComponentInChildren<StackableAmmo>();
                ammoWorkerPhysicsController.ammoStackerController.SetStackHolder(stackableAmmo.SendToStack().transform);
                ammoWorkerPhysicsController.ammoStackerController.GetStack(stackableAmmo.gameObject);
                _ammoInstance.transform.position = transform.position;

            }
        }


        private void OnEnable() => SubscribeEvents();
        private void SubscribeEvents()
        {
            AmmoManagerSignals.Instance.onPlayerEnterAmmoWorkerCreaterArea += OnAddAmmoWorker;
            AmmoManagerSignals.Instance.onDropzoneFull += OnDropZoneFull;
            AmmoManagerSignals.Instance.onOpenNewAmmoDropZone += OnOpenNewAmmoDropZone;
          
        }

        private void OnDropZoneFull()
        {
            _isAllDropzonesFull = true;
            for (int index = 0; index < _dropZoneList.Count; index++)
            {
                _isAllDropzonesFull &= _dropZoneList[index].IsFull;
            }
            if (_isAllDropzonesFull)
            {
                for (int index = 0; index < AmmoWorkerBrainList.Count; index++)
                {
                    AmmoWorkerBrainList[index].IsDropZonesAreFull=true;
                    AmmoWorkerBrainList[index].IsDropZonesAreNotFull=false;
                    AmmoWorkerBrainList[index].AmmoStackerController.CheckIfStopRemoveStack(true);

                }
            }
            else
            {
                for (int index = 0; index < AmmoWorkerBrainList.Count; index++)
                {
                    AmmoWorkerBrainList[index].IsDropZonesAreFull=false;
                    AmmoWorkerBrainList[index].IsDropZonesAreNotFull=true;
                    AmmoWorkerBrainList[index].AmmoStackerController.CheckIfStopRemoveStack(false);

                }
            }
        }

        private void UnsubscribeEvents()
        {
            AmmoManagerSignals.Instance.onPlayerEnterAmmoWorkerCreaterArea -= OnAddAmmoWorker;
            AmmoManagerSignals.Instance.onOpenNewAmmoDropZone -= OnOpenNewAmmoDropZone;
           
        }

        private void OnDisable() => UnsubscribeEvents();
        
        public GameObject GetObjectType(PoolObjectType poolName) => PoolSignals.Instance.onGetObjectFromPool?.Invoke(poolName);
        private void OnOpenNewAmmoDropZone(AmmoDropZoneManager newDropzoneComponent)
        {
           //_isAllDropzonesFull &=newDropzoneComponent.IsFull;
            _dropZoneList.Add(newDropzoneComponent);
            OnDropZoneFull();
        }

        public void DecideNewTarget(AmmoWorkerBrain ammoWorkerBrain)
        {
        
            ushort minAmount=0;
        
            AmmoDropZoneManager selectedDropZone = null;
        
            int dropZoneListCount=_dropZoneList.Count;
        
            for (int index = 0; index <dropZoneListCount ; index++)
            {
                if (index == 0&& !_dropZoneList[index].IsFull)
                {
                    minAmount=_dropZoneList[0].CurrentAmmoAmount;
                }
                if (minAmount >= _dropZoneList[index].CurrentAmmoAmount&&!_dropZoneList[index].IsFull&&prevIndex!=index)
                {
                    prevIndex = (ushort)index;
                    minAmount=_dropZoneList[index].CurrentAmmoAmount;
                    selectedDropZone = _dropZoneList[index];
                }
            

            }
            
            if (dropZoneListCount > 0)
            {
                if (selectedDropZone != null)
                {
                    ammoWorkerBrain.TargetAmmoDropZone = selectedDropZone.gameObject;
                    
                }
                else
                {
                    OnDropZoneFull();
                }
            }
        }
        private void OnAddAmmoWorker(Transform ammoWorkerZone)
        {
            AddAmmoWorker(ammoWorkerZone);
        }

        public void AddAmmoWorker(Transform ammoWorkerZone)
        {
            GameObject _ammoWorker = GetObjectType(PoolObjectType.AmmoWorkerAI);
            AmmoWorkerBrain _ammoWorkerBrain=_ammoWorker.GetComponent<AmmoWorkerBrain>();
            _ammoWorkerBrain.AmmoLoadArea = ammoLoadAreaPos;
            AmmoWorkerBrainList.Add(_ammoWorkerBrain);
    
            _ammoWorker.transform.position = ammoWorkerZone.position;
        }
    }
}



