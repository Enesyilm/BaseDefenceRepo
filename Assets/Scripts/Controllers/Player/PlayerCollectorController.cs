using Abstracts;
using Controllers.StackableControllers;
using Interfaces;
using UnityEngine;

namespace Controllers.Player
{
    public class PlayerCollectorController : MonoBehaviour
    {
        [SerializeField] private MoneyStackerController moneyStackerController;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out StackableMoney stackableMoney))
            {
                Debug.Log("Stackable ile Çarptı");
                moneyStackerController.SetStackHolder(stackableMoney.SendToStack().transform);
                moneyStackerController.GetStack(other.gameObject);
            }
            else if (other.CompareTag("GateEnter"))
            {
                moneyStackerController.OnRemoveAllStack();
            }
        }
    }
}