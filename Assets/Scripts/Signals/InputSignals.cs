using Extentions;
using Keys;
using UnityEngine.Events;

namespace Signals
{
    public class InputSignals:MonoSingleton<InputSignals>
    {
        public UnityAction onEnableInput = delegate {  };
        public UnityAction onDisableInput = delegate {  };
        public UnityAction onFirstTimeTouchTaken = delegate { };
        public UnityAction<XZInputParams>  onInputTaken = delegate { };
        public UnityAction onInputReleased = delegate { };
    }
}