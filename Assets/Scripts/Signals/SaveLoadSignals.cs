using System;
using Data.UnityObjects;
using Extentions;
using UnityEngine.Events;

namespace Signals
{
    public class SaveLoadSignals:MonoSingleton<SaveLoadSignals>
    {
        public UnityAction<CD_Level, int> onSaveGameData = delegate { };

        public Func<string, int, CD_Level> onLoadGameData;
        
    }
}