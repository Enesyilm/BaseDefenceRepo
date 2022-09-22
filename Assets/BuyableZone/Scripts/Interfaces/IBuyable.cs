using Buyablezone.PurchaseParams;

namespace Buyablezone.Interfaces
{
    public interface IBuyable
        {
            public BuyableZoneDataList GetBuyableData();//Required Amount have to return in here
            public void TriggerBuyingEvent();//This function Will trigger on buyablezone completed
            public bool MakePayment();//Payment have to Handle in interface instance func ,it returns Pay statements succes or not legible states as a bool
//<summary>
            // public bool CheckNextBuyableLevel();//checks if new BuyableZoneData exists returns true if exists
        }
}