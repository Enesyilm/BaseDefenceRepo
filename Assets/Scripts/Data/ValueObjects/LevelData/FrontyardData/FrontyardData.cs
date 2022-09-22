using System;
using System.Collections.Generic;

namespace Data.ValueObjects
{
    [Serializable]
    public class FrontyardData
    {
        public List<StageData> Stages;
        public List<BombData> Bomb;
    }
}