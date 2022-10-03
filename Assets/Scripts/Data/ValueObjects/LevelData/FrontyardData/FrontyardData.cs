using System;
using System.Collections.Generic;
using Data.ValueObjects.LevelData;

namespace Data.ValueObjects
{
    [Serializable]
    public class FrontyardData
    {
        public HostageData HostageData;
        public List<StageData> Stages;
        public List<BombData> Bomb;
    }
}