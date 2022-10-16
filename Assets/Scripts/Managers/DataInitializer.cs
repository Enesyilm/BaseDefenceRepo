using UnityEngine;

using System.Collections.Generic;
using Data;
using Data.UnityObjects;
using Data.ValueObjects;
using Data.ValueObjects.LevelData;
using Signals;
public class DataInitializer : MonoBehaviour
{
    #region Self Variables

    #region Public Variables

    #endregion

    #region Serialized Variables

    [SerializeField] private List<LevelData> levelDatas = new List<LevelData>();

    [SerializeField] private CD_Level cdLevel;

    #endregion

    #region Private Variables

    private int _levelID;
    private int _uniqueID = 12123;

    private LevelData _levelData;
    private BaseRoomData _baseRoomData;
    private MineBaseData _mineBaseData;
    private MilitaryBaseData _militaryBaseData;
    private BuyablesData _buyablesData;
    private ScoreData _scoreData;

    #endregion

    #endregion

    private CD_Level GetLevelDatas() => Resources.Load<CD_Level>("Data/CD_Level");
    private void Awake()
    {
        cdLevel=GetLevelDatas();
        _levelID = cdLevel.LevelId;
        levelDatas=cdLevel.LevelDatas;
        _scoreData = cdLevel.ScoreData;
    }

    private void Start()
    {
        InitData();
        CoreGameSignals.Instance.onLevelInitialize?.Invoke();
    }

    #region InıtData

    private void InitData()
    {
        if (!ES3.FileExists($"LevelData{_uniqueID}.es3"))
        {
            if (!ES3.KeyExists("_levelData"))
            {
                cdLevel = GetLevelDatas();
                _levelID = cdLevel.LevelId;
                levelDatas=cdLevel.LevelDatas;//niye 2 defa cagrildi
                _scoreData = cdLevel.ScoreData;
                Debug.Log("Dosya yok");
                Save(_uniqueID);
            }
        }

        Load(_uniqueID);
    }
    public void Load(int uniqueId)
    {
        CD_Level cdLevel = SaveLoadSignals.Instance.onLoadGameData.Invoke(this.cdLevel.Key, uniqueId);
        _levelID = cdLevel.LevelId;
        levelDatas = cdLevel.LevelDatas;
        _baseRoomData = cdLevel.LevelDatas[_levelID].BaseData.BaseRoomData;
        _mineBaseData = cdLevel.LevelDatas[_levelID].BaseData.MineBaseData;
        _militaryBaseData = cdLevel.LevelDatas[_levelID].BaseData.MilitaryBaseData;
        _buyablesData = cdLevel.LevelDatas[_levelID].BaseData.BuyablesData;
        _scoreData = cdLevel.ScoreData;

    }

    #endregion

    #region Event Subscriptions

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        
        InitializeDataSignals.Instance.onSaveLevelID += OnSyncLevelID;
        CoreGameSignals.Instance.onLevelInitialize += OnSyncLevel;
        InitializeDataSignals.Instance.onSaveBaseRoomData += SyncBaseRoomDatas;
        InitializeDataSignals.Instance.onGetBaseRoomData += OnGetBaseRoomData;
        InitializeDataSignals.Instance.onGetMilitaryBaseData += OnLoadMilitaryBaseData;
        InitializeDataSignals.Instance.onSaveMineBaseData += SyncMineBaseDatas;
        InitializeDataSignals.Instance.onSaveMilitaryBaseData += SyncMilitaryBaseData;
        InitializeDataSignals.Instance.onSaveBuyablesData += SyncBuyablesData;
        InitializeDataSignals.Instance.onSaveScoreData += SyncScoreData;
        CoreGameSignals.Instance.onApplicationPause += OnApplicationPauseSave;
        // InitializeDataSignals.Instance.onLoadMilitaryBaseData += OnLoadMilitaryBaseData;
        // InitializeDataSignals.Instance.onLoadBaseRoomData += OnLoadBaseRoomData;
        // InitializeDataSignals.Instance.onLoadBuyablesData += OnLoadBuyablesData;
        // InitializeDataSignals.Instance.onLoadMineBaseData += OnLoadMineBaseData;
        // InitializeDataSignals.Instance.onLoadScoreData += OnLoadScoreData;
    }

    private BaseRoomData OnGetBaseRoomData()
    {
        return _baseRoomData;
    }


    private void UnsubscribeEvents()
    {
        InitializeDataSignals.Instance.onSaveLevelID -= OnSyncLevelID;
        CoreGameSignals.Instance.onLevelInitialize -= OnSyncLevel;
        InitializeDataSignals.Instance.onSaveBaseRoomData -= SyncBaseRoomDatas;
        InitializeDataSignals.Instance.onSaveMineBaseData -= SyncMineBaseDatas;
        InitializeDataSignals.Instance.onSaveMilitaryBaseData -= SyncMilitaryBaseData;
        InitializeDataSignals.Instance.onSaveBuyablesData -= SyncBuyablesData;
        InitializeDataSignals.Instance.onSaveScoreData -= SyncScoreData;
        CoreGameSignals.Instance.onApplicationPause -= OnApplicationPauseSave;
        // InitializeDataSignals.Instance.onLoadMilitaryBaseData -= OnLoadMilitaryBaseData;
        // InitializeDataSignals.Instance.onLoadBaseRoomData -= OnLoadBaseRoomData;
        // InitializeDataSignals.Instance.onLoadBuyablesData -= OnLoadBuyablesData;
        // InitializeDataSignals.Instance.onLoadMineBaseData -= OnLoadMineBaseData;
        // InitializeDataSignals.Instance.onLoadScoreData -= OnLoadScoreData;

    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    #endregion

    
    #region ManagersData
    private void SendDataManagers()
    { 
        InitializeDataSignals.Instance.onLoadLevelID?.Invoke(_levelID);
        InitializeDataSignals.Instance.onLoadBaseRoomData?.Invoke(_baseRoomData);
        InitializeDataSignals.Instance.onLoadScoreData?.Invoke(_scoreData);
        InitializeDataSignals.Instance.onLoadMilitaryBaseData?.Invoke(_militaryBaseData);
        InitializeDataSignals.Instance.onLoadBuyablesData?.Invoke(_buyablesData);
        InitializeDataSignals.Instance.onLoadMineBaseData?.Invoke(_mineBaseData);
    }
    private MilitaryBaseData OnLoadMilitaryBaseData()
    {
          
        return _militaryBaseData;
    }
    private BaseRoomData OnLoadBaseRoomData()
    { 
        return _baseRoomData;
    }
    private MineBaseData OnLoadMineBaseData()
    {
        return _mineBaseData;
    }
    private BuyablesData OnLoadBuyablesData()
    {
        return _buyablesData;
    }

    private ScoreData OnLoadScoreData()
    {
        return _scoreData;
    }
    #endregion

    #region Level Save - Load

    public void Save(int uniqueId)
    {
        CD_Level cdLevel1 = new CD_Level(_levelID, levelDatas,_scoreData);
        SaveLoadSignals.Instance.onSaveGameData.Invoke(cdLevel1, uniqueId);
    }

    

    #endregion

    private void OnSyncLevel()
    {
        SendDataManagers();
    }

    #region Data Sync

    private void OnSyncLevelID(int levelID)
    {
        cdLevel.LevelId = levelID;
    }

    private void SyncBaseRoomDatas(BaseRoomData baseRoomData)
    {
        _baseRoomData = baseRoomData;
    }

    private void SyncMineBaseDatas(MineBaseData mineBaseData)
    {
       _mineBaseData = mineBaseData;
    }

    private void SyncMilitaryBaseData(MilitaryBaseData militaryBaseData)
    {
        _militaryBaseData = militaryBaseData;
    }
    private void SyncScoreData(ScoreData scoreData)
    {
        _scoreData = scoreData;
    }

    private void SyncBuyablesData(BuyablesData buyablesData)
    {
       _buyablesData = buyablesData;
    }

    #endregion

    private void OnApplicationPauseSave()
    {
        Save(_uniqueID);
    }
    }