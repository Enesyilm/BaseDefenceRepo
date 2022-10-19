using System.Collections.Generic;
using Controllers.Player;
using Controllers.StackableControllers;
using UnityEngine;
using StateMachines.AIBrain.Workers;
using Signals;
using Interfaces;

namespace Controllers
{
    public class MoneyWorkerPhysicController : MonoBehaviour
    {
        [SerializeField]
        private MoneyWorkerAIBrain moneyWorkerBrain;
        [SerializeField]
        private MoneyStackerController moneyStackerController;

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent<StackableMoney>(out StackableMoney stackable))
            {
                if (moneyWorkerBrain.IsAvailable())
                {
                    stackable.IsCollected = true;
                    MoneyWorkerSignals.Instance.onThisMoneyTaken?.Invoke();
                    moneyStackerController.SetStackHolder(stackable.SendToStack().transform);
                    moneyStackerController.GetStack(other.gameObject);
                    moneyWorkerBrain.SetCurrentStock();
                }
            }
            if (other.CompareTag("Gate"))
            {
                moneyStackerController.OnRemoveAllStack();
                moneyWorkerBrain.RemoveAllStock();
            }
        }
    } 
}
