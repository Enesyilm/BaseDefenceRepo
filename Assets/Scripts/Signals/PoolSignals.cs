using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Extentions;
using Enums;
using System;
using Enum;

namespace Signals
{
    public class PoolSignals : MonoSingleton<PoolSignals>
    {
        public Func<PoolObjectType, GameObject> onGetObjectFromPool = delegate { return null; };
        public UnityAction<GameObject,PoolObjectType> onReleaseObjectFromPool = delegate { };
    } 
}
