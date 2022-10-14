using System;
using Data;
using Enum;
using Signals;
using TMPro;
using UnityEngine;

namespace Controllers
{
    public class ScoreController:MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI gemText;
        [SerializeField]
        private TextMeshProUGUI moneyText;

        #region Private Variables

        private ScoreData _scoreData;
        #endregion

        private void Awake()
        {
            //_scoreData=InitializeDataSignals.Instance.onLoadScoreData.Invoke();
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void OnInitScoreData(ScoreData scoreData)
        {
            _scoreData=scoreData;
            UpdateMoneyScoreText();
            UpdateGemScoreText();
        }
        private void SubscribeEvents()
        {
            ScoreSignals.Instance.onUpdateMoneyScore += OnUpdateMoneyScore;
            ScoreSignals.Instance.onUpdateGemScore += OnUpdateGemScore;
            InitializeDataSignals.Instance.onLoadScoreData += OnInitScoreData;
        }
        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void UnSubscribeEvents()
        {
            ScoreSignals.Instance.onUpdateMoneyScore -= OnUpdateMoneyScore;
            ScoreSignals.Instance.onUpdateGemScore -= OnUpdateGemScore;
            InitializeDataSignals.Instance.onLoadScoreData -= OnInitScoreData;
        }

        #endregion
        private void OnUpdateGemScore()
        {
            _scoreData.GemScore ++;
            InitializeDataSignals.Instance.onSaveScoreData?.Invoke(_scoreData);
            UpdateGemScoreText();
        }
        

        private void UpdateGemScoreText()
        {
            gemText.text = _scoreData.GemScore.ToString();
        }
        private void UpdateMoneyScoreText()
        {
            moneyText.text = _scoreData.MoneyScore.ToString();
        }

        private void OnUpdateMoneyScore()
        {
            _scoreData.MoneyScore++;
            InitializeDataSignals.Instance.onSaveScoreData?.Invoke(_scoreData);
            UpdateMoneyScoreText();
        }
        
     
    }
}