using UnityEngine;

namespace Managers
{
    public class MineManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        public bool IsPayedTotalAmount=>_payedGemAmount>=_requiredGemAmount;
        public int GemAmount;//Sinyalle Cekilecek Score Manager Uzerinden

        #endregion

        #region Serialized Variables

        #endregion

        #region Private Variables

        private int _payedGemAmount;
        private int _requiredGemAmount;


        #endregion

        #endregion

        private void ShowGemAmountText()
        {
            
        }
        public void PayGemToMine()
        {
            GemAmount--;
            _payedGemAmount++;
        }
    }
}