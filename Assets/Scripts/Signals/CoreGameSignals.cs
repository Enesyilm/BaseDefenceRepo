using System;
using Enum;
using Enums;
using Extentions;
using Keys;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class CoreGameSignals : MonoSingleton<CoreGameSignals>
    {
        public UnityAction<GameStates> onChangeGameState = delegate { };
        public UnityAction onLevelInitialize = delegate { };
        public UnityAction onClearActiveLevel = delegate { };
        public UnityAction onLevelFailed = delegate { };
        public UnityAction onLevelSuccessful = delegate { };
        public UnityAction onNextLevel = delegate { };
        public UnityAction onRestartLevel = delegate { };
        public UnityAction onPlay = delegate { };
        public UnityAction onReset = delegate { };
        public UnityAction onEnterTurret = delegate { };
        public UnityAction onLevel = delegate { };
        public UnityAction<TurretLocationType,GameObject> onSetCurrentTurret = delegate(TurretLocationType arg0, GameObject o) {  };

        public UnityAction onSetCameraTarget = delegate { };
        public UnityAction onStageAreaReached = delegate { };
        public UnityAction onStageSuccessful = delegate { };
        public UnityAction onApplicationPause = delegate { };

        public Func<int> onGetLevelID = delegate { return 0; };
    }
}