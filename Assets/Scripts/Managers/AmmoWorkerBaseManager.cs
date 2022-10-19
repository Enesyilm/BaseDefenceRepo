using AIBrain;
using Controllers;
using Signals;
using System.Collections.Generic;
using Concreate;
using Enum;
using UnityEngine;
using Interfaces;

namespace Managers
{
    public class AmmoWorkerBaseManager : MonoBehaviour,IGetPoolObject
    {
        #region Self Variables

        #region Public Variables
        public bool IsAllDropzonesFull=true;
        #endregion
        
        #region Serialized Variables
        [SerializeField]
        private List<AmmoWorkerBrain> AmmoWorkerBrainList;
        [SerializeField]
        private Transform ammoLoadAreaPos;
        #endregion
        
        #region Private Variables
        private List<AmmoDropZoneManager> _dropZoneList=new List<AmmoDropZoneManager>();
        
        private short prevIndex=-1;
        private int _maxAmmoCapasity=5;
        List<AmmoDropZoneManager> tempList = new List<AmmoDropZoneManager>();
        #endregion
        

        #endregion

        #region Event Subscription
        private void OnEnable() => SubscribeEvents();
        private void SubscribeEvents()
        {
            AmmoManagerSignals.Instance.onPlayerEnterAmmoWorkerCreaterArea += OnAddAmmoWorker;
            AmmoManagerSignals.Instance.onDropzoneFull += OnDropZoneFull;
            AmmoManagerSignals.Instance.onOpenNewAmmoDropZone += OnOpenNewAmmoDropZone;
            AmmoManagerSignals.Instance.onGetDropZoneStates += OnGetDropZoneState;
            
          
        }
        private void UnsubscribeEvents()
        {
            AmmoManagerSignals.Instance.onPlayerEnterAmmoWorkerCreaterArea -= OnAddAmmoWorker;
            AmmoManagerSignals.Instance.onOpenNewAmmoDropZone -= OnOpenNewAmmoDropZone;
           
        }

        private void OnDisable() => UnsubscribeEvents();
        

        #endregion

        

        private bool OnGetDropZoneState()
        {
            return IsAllDropzonesFull;
        }

        private void OnDropZoneFull()
        {
            IsAllDropzonesFull = true;
            for (int index = 0; index < _dropZoneList.Count; index++)
            {
                IsAllDropzonesFull &= _dropZoneList[index].IsFull;
            }
            if (IsAllDropzonesFull)
            {
                for (int index = 0; index < AmmoWorkerBrainList.Count; index++)
                {
                    AmmoWorkerBrainList[index].AmmoStackerController.CheckIfStopRemoveStack(true);
                    AmmoWorkerBrainList[index].IsDropZonesAreFull=true;
                    AmmoWorkerBrainList[index].IsDropZonesAreNotFull=false;
                }
            }
            else
            {
                for (int index = 0; index < AmmoWorkerBrainList.Count; index++)
                {
                    AmmoWorkerBrainList[index].AmmoStackerController.CheckIfStopRemoveStack(false);
                    AmmoWorkerBrainList[index].IsDropZonesAreFull=false;
                    AmmoWorkerBrainList[index].IsDropZonesAreNotFull=true;
                }
            }
        }

       
        
        public GameObject GetObjectType(PoolObjectType poolName) => PoolSignals.Instance.onGetObjectFromPool?.Invoke(poolName);
        private void OnOpenNewAmmoDropZone(AmmoDropZoneManager newDropzoneComponent)
        {
            _dropZoneList.Add(newDropzoneComponent);
            tempList.Add(newDropzoneComponent);
            OnDropZoneFull();
        }
        public void SendAmmoToWorker(AmmoWorkerPhysicsController ammoWorkerPhysicsController)
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
        public void DecideNewTarget(AmmoWorkerBrain ammoWorkerBrain)
        {
            
            AmmoDropZoneManager _selectedDropZone=null;
            
            
            for (int index = 0; index < _dropZoneList.Count; index++)
            {
                Debug.Log("1");
                if (!_dropZoneList[index].IsFull)
                {
                    Debug.Log("2");
                    tempList.Add(_dropZoneList[index]);
                }
            }
            Debug.Log("_dropZoneList.Count"+_dropZoneList.Count);
            Debug.Log("_dropZoneList.Count"+_dropZoneList.Count);
            Debug.Log("templist.Count"+tempList.Count);
            if (tempList.Count > 0)
            {
                Debug.Log("3");
                int randomindex=Random.Range(0, _dropZoneList.Count-1);
                Debug.Log("4");
                Debug.Log("randomindex"+randomindex);
                _selectedDropZone = tempList[randomindex];
                Debug.Log("5");
                ammoWorkerBrain.TargetAmmoDropZone = _selectedDropZone.gameObject;
                tempList.Clear();
                tempList.TrimExcess();
                     Debug.Log("TempList a geç"+Random.Range(0, _dropZoneList.Count-1));
                
            }
            else
            {
                Debug.Log("Idle a geç");
            }
            
            // ushort minAmount=0;
            // ushort tempIndex=0;
            // AmmoDropZoneManager selectedDropZone = null;
            //
            // int dropZoneListCount=_dropZoneList.Count;
            //
            // for (int index = 0; index <dropZoneListCount ; index++)
            // {
            //     if (index == 0&& !_dropZoneList[index].IsFull)
            //     {
            //         Debug.Log("1 index:"+index);
            //         minAmount=_dropZoneList[0].CurrentAmmoAmount;
            //         selectedDropZone = _dropZoneList[index];
            //     }
            //     if (minAmount >= _dropZoneList[index].CurrentAmmoAmount&&!_dropZoneList[index].IsFull&&prevIndex!=index)
            //     {
            //         Debug.Log("2 index:"+index);
            //         tempIndex = (ushort)index;
            //         minAmount=_dropZoneList[index].CurrentAmmoAmount;
            //         selectedDropZone = _dropZoneList[index];
            //     }
            // }
            //
            // if (dropZoneListCount > 0)
            // {
            //     if (selectedDropZone != null)
            //     {
            //         prevIndex = (short)tempIndex;
            //         ammoWorkerBrain.TargetAmmoDropZone = selectedDropZone.gameObject;
            //     }
            //     else
            //     {
            //         OnDropZoneFull();
            //     }
            // }
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



