// using System;
// using System.Collections.Generic;
// using Data.ValueObjects;
// using Data.ValueObjects.ScoreData;
// using Enum;
// using Enums;
// using Signals;
// using UnityEngine;
// using UnityEngine.SocialPlatforms.Impl;
//
// namespace Managers
// {
//     public class ScoreManager : MonoBehaviour
//     {
//         #region Self Variables
//
//         #region Private Variables
//
//         private int _totalScore;
//         private int _levelScore;
//         private List<int> _scoreVariables = new List<int>(System.Enum.GetNames(typeof(ScoreVariableType)).Length);
//         
//
//         #endregion
//
//         #region Serialized Variables
//         
//
//         #endregion
//         #endregion
//
//         private void Awake()
//         {
//             InitScoreValues();
//         }
//         
//
//         private void InitScoreValues()
//         {
//             for (int i = 0; i < System.Enum.GetNames(typeof(ScoreVariableType)).Length; i++)
//             {
//                 _scoreVariables.Add(0);
//             }
//         }
//
//         #region Event Subscription
//         
//         private void OnEnable()
//         {
//             
//             SubscribeEvents();
//         }
//
//
//         private void SubscribeEvents()
//         {
//             InitializeDataSignals.Instance.onLoadScoreData+=OnLoadScoreData;
//             ScoreSignals.Instance.onChangeScore+=OnChangeScore;
//             ScoreSignals.Instance.onGetScore+=OnGetScore;
//         }
//
//        
//
//         private void OnDisable()
//         {
//             UnSubscribeEvents();
//         }
//
//         private void UnSubscribeEvents()
//         {
//            
//             ScoreSignals.Instance.onChangeScore-=OnChangeScore;
//             ScoreSignals.Instance.onGetScore-=OnGetScore;
//
//         }
//
//         private void OnSendScoreToSave()
//         {
//             
//         }
//
//         private void InitTotalScoreData()
//         {
//             _scoreVariables[0]=a_saveData.TotalColorman;
//             
//         }
//         private void OnLoadScoreData(ScoreData arg0)
//         {
//             ScoreSignals.Instance.onUpdateScore?.Invoke(_scoreVariables);
//         }
//
//         #endregion
//         private int OnGetScore(ScoreVariableType _scoreVarType)
//         {
//             return _scoreVariables[(int)_scoreVarType];
//         }
//
//         
//         private void OnChangeScore(ScoreTypes _scoreType,ScoreVariableType _scoreVarType)
//         {
//             int _changedScoreValue = 0;
//             switch (_scoreType)
//             {
//                 case ScoreTypes.DecScore:
//                     _changedScoreValue--;
//                     break;
//                 case ScoreTypes.IncScore:
//                     _changedScoreValue++;
//                     break;
//                 
//                     
//             }
//
//             _scoreVariables[(int)_scoreVarType]+=_changedScoreValue;
//             ScoreSignals.Instance.onUpdateScore?.Invoke( _scoreVariables);
//
//         }
//         private void OnSendScoreToManagers(GameStates arg0)
//         {
//             ScoreSignals.Instance.onUpdateScore?.Invoke(_scoreVariables);
//         }
//     }
// }