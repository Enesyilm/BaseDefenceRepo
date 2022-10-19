using AIBrain;
using Controllers;
using Enums;
using Extentions;
using System;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class AmmoManagerSignals : MonoSingleton<AmmoManagerSignals>
    {
        public UnityAction <Transform> onPlayerEnterAmmoWorkerCreaterArea = delegate { };
        public UnityAction <AmmoDropZoneManager> onOpenNewAmmoDropZone = delegate { };
        public UnityAction onDropzoneFull = delegate { };
        public Func<bool> onGetDropZoneStates= delegate { return true;};

    }
}