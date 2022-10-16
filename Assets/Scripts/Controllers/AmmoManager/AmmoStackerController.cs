using DG.Tweening;
using Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abstracts;
using UnityEngine;

namespace Controllers
{
    public class AmmoStackerController : AStacker
    {

        [SerializeField] private List<Vector3> positionList;

        [SerializeField] private float radiusAround;

        private float stackDelay = 0.5f;

        private Sequence GetStackSequence;

        private int stackListOrder = 0;
        private bool _isDropZoneFull;

        private int stackListConstCount;

        private bool canRemove = true;

        private void Awake()
        {
            DOTween.Init(true, true, LogBehaviour.Verbose).SetCapacity(500, 125);
        }
        private new List<GameObject> StackList
        {
            get => base.StackList;
            set => base.StackList = value;
        }
        public List<Vector3> PositionList { get => positionList; set => positionList = value; }

        public override void SetStackHolder(Transform otherTransform)
        {
            otherTransform.SetParent(transform);
        }
        public override void GetStack(GameObject stackableObj)
        {
            GetStackSequence = DOTween.Sequence();
            var randomBouncePosition = CalculateRandomAddStackPosition();
            var randomRotation = CalculateRandomStackRotation();

            GetStackSequence.Append(stackableObj.transform.DOLocalMove(randomBouncePosition, .5f));
            GetStackSequence.Join(stackableObj.transform.DOLocalRotate(randomRotation, .5f)).OnComplete(() =>
            {

                stackableObj.transform.rotation = Quaternion.LookRotation(transform.forward);

                    StackList.Add(stackableObj);
                  
                              
                stackableObj.transform.DOLocalMove(PositionList[StackList.Count - 1], 0.3f);
            });
            
            //GetStackSequence.Play().OnComplete()

        }
        public void OnRemoveAllStack(Transform zoneLocation)
        {
            if (!canRemove)
                return;
            canRemove = false;
            stackListConstCount = StackList.Count;
            RemoveAllStack(zoneLocation);
        }

        private async void RemoveAllStack(Transform zoneLocation)
        {
            if (StackList.Count == 0)
            {
                canRemove = true;
                return;
            }
            if (StackList.Count >= stackListConstCount - 6)
            {
                //Remove'a devam etmeli mi bunu kontrol etmeli bool kontrol etmeli
                if(_isDropZoneFull){
                     return;
                }
                RemoveStackAnimation(StackList[StackList.Count - 1],zoneLocation);
                StackList.TrimExcess();
                await Task.Delay(201);
                RemoveAllStack(zoneLocation);
            }
            else
            {
                for (int i = 0; i < StackList.Count; i++)
                {
                    //Remove'a devam etmeli mi bunu kontrol etmeli bool kontrol etmeli
                    if(_isDropZoneFull){
                        break;
                    }
                    RemoveStackAnimation(StackList[i],zoneLocation);
                    StackList.TrimExcess();
                }
                canRemove = true;
            }
        }

        private void RemoveStackAnimation(GameObject removedStack,Transform zoneLocation)
        {
            GetStackSequence = DOTween.Sequence();
            var randomRemovedStackPosition = CalculateRandomRemoveStackPosition();
            var randomRemovedStackRotation = CalculateRandomStackRotation();

            GetStackSequence.Append(removedStack.transform.DOLocalMove(randomRemovedStackPosition, .2f));
            GetStackSequence.Join(removedStack.transform.DOLocalRotate(randomRemovedStackRotation, .2f)).OnComplete(() =>
            {
                removedStack.transform.rotation = Quaternion.LookRotation(transform.forward);
               
                StackList.Remove(removedStack);
                removedStack.layer = LayerMask.NameToLayer("Ammo/AmmoOnStaticStack");
                removedStack.transform.SetParent(zoneLocation);
                removedStack.transform.DOLocalMove(Vector3.zero, .1f).OnComplete(() =>
                {
                    

                });
            });
        }

        public void  CheckIfStopRemoveStack(bool dropZoneState)//Alıp alamayacağını return eder
        {
            _isDropZoneFull = dropZoneState;
        }
        public override void ResetStack(IStackable stackable)
        {

        }
        public void GetStackPositions(List<Vector3> stackPositions)
        {
            PositionList = stackPositions;
        }
        private Vector3 CalculateRandomAddStackPosition()
        {
            var randomHeight = Random.Range(0.1f, 3f);
            var randomAngle = Random.Range(230, 310);
            var rad = randomAngle * Mathf.Deg2Rad;
            return new Vector3(radiusAround * Mathf.Cos(rad),
                transform.parent.position.y + randomHeight, -radiusAround * Mathf.Sin(rad));
        }
        private Vector3 CalculateRandomRemoveStackPosition()
        {
            var randomHeight = Random.Range(0.1f, 3f);
            var randomAngle = Random.Range(1, 179);
            var rad = randomAngle * Mathf.Deg2Rad;
            return new Vector3(radiusAround * Mathf.Cos(rad),
                transform.parent.position.y + randomHeight, radiusAround * Mathf.Sin(rad));
        }
        private Vector3 CalculateRandomStackRotation()
        {
            var randomRotationX = Random.Range(-90, 90);
            var randomRotationY = Random.Range(-90, 90);
            var randomRotationZ = Random.Range(-90, 90);
            return new Vector3(randomRotationX, randomRotationY, randomRotationZ);
        }
    }
}