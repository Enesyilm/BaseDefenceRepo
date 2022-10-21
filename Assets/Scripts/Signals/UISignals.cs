using System.Collections;
using System.Collections.Generic;
using Enum;
using UnityEngine;
using UnityEngine.Events;
using Enums;
using Extentions;

namespace Signals
{
    public class UISignals : MonoSingleton<UISignals>
    {
        public UnityAction<UIPanels> onOpenPanel = delegate { };
        public UnityAction<UIPanels> onClosePanel = delegate { };
        public UnityAction<WeaponTypes> onChangeWeapon = delegate{ WeaponTypes arg0; };
        public  UnityAction<bool> onHealthBarVisibility=delegate(bool arg0) {  };
        protected override void Awake()
        {
            base.Awake();
        }
    }

}