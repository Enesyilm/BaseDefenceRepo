using Datas.ValueObject;
using System.Collections;
using UnityEngine;

namespace Datas.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Turret", menuName = "Basedefence/CD_Turret")]
    public class CD_Turret : ScriptableObject
    {
        public TurretData turretDatas;
    }
}