using System.Collections.Generic;
using Buyablezone.Interfaces;
using Buyablezone.PurchaseParams;
using Data.ValueObjects;
using DG.Tweening;
using Enum;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class FrontYardWallController : MonoBehaviour,IBuyable
    {
        public List<BuyableZoneData> BuyableZoneStages=
                new List<BuyableZoneData>()
                {
                    new BuyableZoneData()
                };

        private BuyableZoneDataList _buyableZoneDataList = new BuyableZoneDataList(0,25);
        public int CurrentStageID;
        public FrontyardData FrontyardData;
        [SerializeField]private Material wallShader;
        int Gem = 100;

        public BuyableZoneDataList GetBuyableData()
        {
            return _buyableZoneDataList;
        }

        public void AlreadyOpened()
        {
            gameObject.SetActive(false);
        }
        public void TriggerBuyingEvent()
        {
            FrontyardData.Stages[CurrentStageID].IsOpened = true;
            StartCloseWallAnimation();
            InitializeDataSignals.Instance.onSaveFrontyardData?.Invoke(FrontyardData);
            //
        }

        private void StartCloseWallAnimation()
        {
                float fresnelValue=0;
                DOVirtual.Float(0,2,2, fresnelValue=>
                {
                    Debug.Log(fresnelValue);
                    wallShader.SetFloat("_FresnelPower",fresnelValue);
                }).OnComplete(()=>{
                    gameObject.SetActive(false);
                    DOVirtual.Float(2,0,2, fresnelValue=>
                    {
                        Debug.Log(fresnelValue);
                        wallShader.SetFloat("_FresnelPower",fresnelValue);
                    });
                });
            
        }

        public bool MakePayment()
        {
            int _gemAmount=ScoreSignals.Instance.onGetScore.Invoke(ScoreVariableType.TotalGem);
            if ( _gemAmount> 0)
            {
                ScoreSignals.Instance.onUpdateGemScore?.Invoke(ScoreTypes.DecScore);
                return true;
                
            }
            else
            {
                return false;
            }
        }
    }
}