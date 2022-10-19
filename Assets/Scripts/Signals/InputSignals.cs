using Enum;
using Extentions;
using Keys;
using UnityEngine.Events;

namespace Signals
{
    public class InputSignals:MonoSingleton<InputSignals>
    {
        // public UnityAction onEnableInput = delegate {  };
        // public UnityAction onDisableInput = delegate {  };
        public UnityAction onFirstTimeTouchTaken = delegate { };
        public UnityAction<XZInputParams>  onInputTaken = delegate { };
        public UnityAction  onCharacterInputRelease = delegate { };
        public UnityAction<XZInputParams>  onJoystickInputDraggedforTurret = delegate { };
        public UnityAction<bool> onInputTakenActive = delegate { };
        public UnityAction<InputHandlers> onInputHandlerChange = delegate { };
    }
}