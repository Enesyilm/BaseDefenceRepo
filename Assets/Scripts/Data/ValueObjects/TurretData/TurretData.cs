using System;
using System.Collections;
using UnityEngine;

namespace Datas.ValueObject
{
    [Serializable]
    public class TurretData 
    {
        public TurretMovementData MovementDatas;
        public TurretOtoAtackData TurretOtoAtackDatas;
        public TurretShootData gattalingRotateDatas; 
    }
}

    [Serializable]
    public class TurretMovementData 
    {

        public float TurretTurnSpeed;
    }

    [Serializable]
    public class TurretOtoAtackData 
    {
        public float TimeScale;
    }
[Serializable]
    public class TurretShootData 
    {

        public float RotateSpeed;
    }
