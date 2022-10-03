using DG.Tweening;
using Enum;
using FrameworkGoat;
using UnityEngine;

namespace Commands
{
    public class DropZoneAnimationCommand
    {
        public void DropZoneAnimation(Transform otherTransform, Vector3 targetPosition)
        {
            otherTransform.DOLocalMove(new Vector3(Random.Range(0, 3)
                , Random.Range(0, 3)
                , Random.Range(0, 3)),1f).OnComplete(() =>
            {
                otherTransform.DOLocalMove(targetPosition, 0.5f).OnComplete((() =>
                {
                    
                }));
            });
           
        }
        public void DropZoneGetAnimation(GameObject otherTransform, Vector3 targetPosition)
        {
            otherTransform.transform.DOLocalMove(new Vector3(Random.Range(0, 3)
                , Random.Range(0, 3)
                , Random.Range(0, 3)),1f).OnComplete(() =>
            {
                otherTransform.transform.DOLocalMove(targetPosition, 0.5f).OnComplete((() =>
                {
                    ObjectPoolManager.Instance.ReturnObject(otherTransform,PoolObjectType.Gem.ToString());
                }));
            });
           
        }
    }
}