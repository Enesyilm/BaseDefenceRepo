using System;
using Data;
using Data.ValueObject;
using Data.ValueObjects;
using Extentions;
using UnityEngine.Events;

namespace Signals
{
    public class InitializeDataSignals : MonoSingleton<InitializeDataSignals>
    {
        public UnityAction<BaseRoomData> onSaveBaseRoomData = delegate(BaseRoomData arg0) {  };
        public UnityAction<MineBaseData> onSaveMineBaseData = delegate(MineBaseData arg0) {  };
        public UnityAction<MilitaryBaseData> onSaveMilitaryBaseData = delegate(MilitaryBaseData arg0) {  };
        public UnityAction<BuyablesData> onSaveBuyablesData = delegate(BuyablesData arg0) {  };
        public UnityAction<ScoreData> onSaveScoreData = delegate(ScoreData arg0) {  };
        public UnityAction<int> onSaveLevelID = delegate(int arg0) {  };


        public UnityAction<BaseRoomData> onLoadBaseRoomData = delegate(BaseRoomData arg0) {  };
        public Func<BaseRoomData> onGetBaseRoomData = delegate() { return null;};
        public Func<MilitaryBaseData> onGetMilitaryBaseData = delegate() { return null;};
        public UnityAction<MineBaseData> onLoadMineBaseData = delegate(MineBaseData arg0) {  };
        public UnityAction<MilitaryBaseData> onLoadMilitaryBaseData = delegate(MilitaryBaseData arg0) {  };
        public UnityAction<BuyablesData> onLoadBuyablesData = delegate(BuyablesData arg0) {  };
        public UnityAction<ScoreData> onLoadScoreData = delegate(ScoreData arg0) {  };
         public UnityAction<int> onLoadLevelID = delegate(int arg0) {  };
        // public Func<MilitaryBaseData> onLoadMilitaryBaseData = delegate { return null; };
        // public Func<BaseRoomData> onLoadBaseRoomData = delegate { return null; };
        // public Func<BuyablesData> onLoadBuyablesData = delegate { return null; };
        // public Func<MineBaseData> onLoadMineBaseData = delegate { return null; };
        // public Func<ScoreData>    onLoadScoreData = delegate { return default;};
    }
}