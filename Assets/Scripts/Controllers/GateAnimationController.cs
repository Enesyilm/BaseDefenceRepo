using DG.Tweening;
using UnityEngine;

namespace Controllers
{
    public class GateAnimationController : MonoBehaviour
    {
        public void PlayGateAnimation(bool _state)
        {
            if (_state)
            {
                transform.DOLocalRotate(new Vector3(0, 0, 90),2).SetEase(Ease.OutBounce);
            }
            else
            {
                transform.DOLocalRotate(new Vector3(0, 0, 0),2);
            }
        }
    }
}