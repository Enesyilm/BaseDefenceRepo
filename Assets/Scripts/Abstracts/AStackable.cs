using Controllers.StackableControllers;
using DG.Tweening;
using Interfaces;
using Signals;
using UnityEngine;

namespace Abstracts
{
    public abstract class AStackable:MonoBehaviour,IStackable
    {
        public virtual bool IsSelected { get; set; }
        public virtual bool IsCollected { get; set; }

        public virtual void SetInit(Transform initTransform, Vector3 position)
        {

        }

        public virtual void SetVibration(bool isVibrate)
        {

        }

        public virtual void SetSound()
        {

        }

        public virtual void EmitParticle()
        {

        }

        public virtual void PlayAnimation()
        {

        }

        public GameObject SendToStack()
        {
            return this.gameObject;
        }

        public abstract GameObject SendToStack(Transform transform1);
        public virtual void SendPosition(StackableMoney stackableMoney)
                {
                    DOVirtual.DelayedCall(0.1f, () => MoneyWorkerSignals.Instance.onSetStackable?.Invoke(stackableMoney));
                }
    }
}