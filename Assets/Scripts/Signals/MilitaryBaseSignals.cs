using System;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class MilitaryBaseSignals:MonoSingleton<MilitaryBaseSignals>
    {
        public Func<Transform> onGetWaitingPosition= delegate { return null;};
    }
}