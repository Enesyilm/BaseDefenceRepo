using Abstracts;
using Controllers.StackableControllers;
using Interfaces;
using Signals;
using UnityEngine;

namespace Controllers.Player
{
    public class PlayerCollectorController : MonoBehaviour
    {
        [SerializeField]
        public Collider collider;
        [SerializeField] private MoneyStackerController moneyStackerController;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out StackableMoney stackableMoney))
            {
                moneyStackerController.SetStackHolder(stackableMoney.SendToStack().transform);
                moneyStackerController.GetStack(other.gameObject);
                stackableMoney.IsCollected = true;
                MoneyWorkerSignals.Instance.onThisMoneyTaken.Invoke();
            }
            else if (other.CompareTag("GateEnter"))
            {
                moneyStackerController.OnRemoveAllStack();
            }
        }
    }
}