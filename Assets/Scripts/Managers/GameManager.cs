using Signals;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        

        #endregion

        #region Private Variables


        #endregion

        #region Serialized Variables

        

        #endregion

        #endregion


        #region Event Subscription

        private void Awake()
        {
            Application.targetFrameRate = 60;
        }
        

        #endregion
        

        private void OnApplicationPause(bool isPauseStatus)
        {
            if (isPauseStatus)
            {
                CoreGameSignals.Instance.onApplicationPause?.Invoke();
            }
            
        }

        private void OnApplicationQuit()
        {
            CoreGameSignals.Instance.onApplicationPause?.Invoke();
        }


       

    }
}