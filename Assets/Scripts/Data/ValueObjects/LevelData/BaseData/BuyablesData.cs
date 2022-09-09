using System;

namespace Data.ValueObjects
{
    [Serializable]
    public class BuyablesData
    {
        public int MoneyWorkerCost;
        public int MoneyWorkerPayed;
        public int AmmoWorkerCost;
        public int AmmoWorkerPayedAmount;
        public int BoughtMoneyWorkerAmount;
        public int BoughtAmmoWorkerAmount;
        public bool UpgradeButtonLocked;
        public int MoneyWorkerLevel;
        public int AmmoWorkerLevel;
    }
}