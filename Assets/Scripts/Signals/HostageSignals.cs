using AI.MinerAI;
using Extentions;
using Managers;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class HostageSignals:MonoSingleton<HostageSignals>
    {
        public UnityAction<HostageManager> onAddHostageStack=delegate{};
        public UnityAction<Vector3> onSendHostageToMineBase=delegate {};
        public UnityAction onSendHostageStackToMilitaryBase=delegate {};
    }
}