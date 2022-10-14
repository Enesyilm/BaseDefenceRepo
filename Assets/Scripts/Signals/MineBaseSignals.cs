using System;
using AI.MinerAI;
using Enum;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class MineBaseSignals:MonoSingleton<MineBaseSignals>
    {
        public Func<Tuple<Transform,GemMineType>> onGetRandomMineTarget= delegate { return null;};
        //public UnityAction<MinerAIBrain> onNewMineWorkerAdd= delegate {};
        public  Func<MinerAIBrain,bool> onNewMineWorkerAdd= delegate { return false;};
        public Func<Transform> onGetGemHolderPos= delegate { return null;};
    }
}