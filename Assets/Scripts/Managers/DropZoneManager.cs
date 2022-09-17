using UnityEngine;

namespace Managers
{
    public class DropZoneManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        internal float timer = 0;
        internal float PayOffset = 0.1f;



        #endregion

        #region Serialized Variables

        [SerializeField] private int requiredAmount = 100;
        [SerializeField] private int payedAmount = 100;


        #endregion


        #endregion

        public void CalculateTimer()
        {
            if (timer > PayOffset)
            {
                PayAmountToDropzone();
                timer= 0;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }

        private void PayAmountToDropzone()
        {
            if (CheckCanBuy())
            {
                UpdateRatio();
                payedAmount++;

            }
            //ScoreManagerden kontrol ed gem /money degerini --

        }

        private bool CheckCanBuy()
        {
            if (payedAmount >= requiredAmount /*&& ScoreAmount>0*/)
            {
                return true;
            }

            return false;

        }

        private void UpdateRatio()
        {
            //payedAmount % requiredAmount;
        }
    }
}