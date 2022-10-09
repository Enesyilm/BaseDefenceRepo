using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Data.UnityObject;
using Data.UnityObjects;
using Data.ValueObject;
using Data.ValueObjects;
using Data.ValueObjects.LevelData;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;

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

    #endregion

    #endregion

    private CD_Level GetLevelDatas() => Resources.Load<CD_Level>("Data/CD_Level");


    private void Start()
    {
        InitData();
        CoreGameSignals.Instance.onLevelInitialize?.Invoke();
    }

    #region InıtData

    private void InitData()
    {
        cdLevel = GetLevelDatas();
        _levelID = cdLevel.LevelId;
        levelDatas = cdLevel.LevelDatas;
        if (!ES3.FileExists($"_levelData{_uniqueID}.es3"))
        {
            if (!ES3.KeyExists("_levelData"))
            {
                cdLevel = GetLevelDatas();
                _levelID = cdLevel.LevelId;
                levelDatas=cdLevel.LevelDatas;//niye 2 defa cagrildi
                Save(_uniqueID);
            }
        }

        Load(_uniqueID);
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
        InitializeDataSignals.Instance.onSaveMineBaseData += SyncMineBaseDatas;
        InitializeDataSignals.Instance.onSaveMilitaryBaseData += SyncMilitaryBaseData;
        InitializeDataSignals.Instance.onSaveBuyablesData += SyncBuyablesData;
    }

    private void UnsubscribeEvents()
    {
        InitializeDataSignals.Instance.onSaveLevelID -= OnSyncLevelID;
        CoreGameSignals.Instance.onLevelInitialize -= OnSyncLevel;
        InitializeDataSignals.Instance.onSaveBaseRoomData -= SyncBaseRoomDatas;
        InitializeDataSignals.Instance.onSaveMineBaseData -= SyncMineBaseDatas;
        InitializeDataSignals.Instance.onSaveMilitaryBaseData -= SyncMilitaryBaseData;
        InitializeDataSignals.Instance.onSaveBuyablesData -= SyncBuyablesData;
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    #endregion

    private void SendDataManagers()
    {
        InitializeDataSignals.Instance.onLoadLevelID?.Invoke(_levelID);
        InitializeDataSignals.Instance.onLoadBaseRoomData?.Invoke(_baseRoomData);
        InitializeDataSignals.Instance.onLoadMineBaseData?.Invoke(_mineBaseData);
        InitializeDataSignals.Instance.onLoadMilitaryBaseData?.Invoke(_militaryBaseData);
        InitializeDataSignals.Instance.onLoadBuyablesData?.Invoke(_buyablesData);
    }

    #region Level Save - Load

    public void Save(int uniqueId)
    {
        CD_Level cdLevel = new CD_Level(_levelID, levelDatas); //niye newleniyor
        SaveLoadSignals.Instance.onSaveGameData.Invoke(cdLevel, uniqueId);
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
        cdLevel.LevelDatas[_levelID].BaseData.BaseRoomData = baseRoomData;
    }

    private void SyncMineBaseDatas(MineBaseData mineBaseData)
    {
        cdLevel.LevelDatas[_levelID].BaseData.MineBaseData = mineBaseData;
    }

    private void SyncMilitaryBaseData(MilitaryBaseData militaryBaseData)
    {
        cdLevel.LevelDatas[_levelID].BaseData.MilitaryBaseData = militaryBaseData;
    }

    private void SyncBuyablesData(BuyablesData buyablesData)
    {
        cdLevel.LevelDatas[_levelID].BaseData.BuyablesData = buyablesData;
    }

    #endregion

    private void OnApplicationFocus(bool hasFocus)
    {
        
    }
    }