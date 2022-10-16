using System;
using System.Collections.Generic;
using Buyablezone.PurchaseParams;
using Data.ValueObjects.AiData;

namespace Data.ValueObjects
{
    [Serializable]
    public class BaseData
    {
        public BaseRoomData BaseRoomData;
        public AmmoWorkerAIData AmmoWorkerAIData;
        public MineBaseData MineBaseData;
        public MilitaryBaseData MilitaryBaseData;
        public BuyablesData BuyablesData;
        
    }
}