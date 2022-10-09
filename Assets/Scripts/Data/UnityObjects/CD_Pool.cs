using Data.ValueObject;
using Enum;
using UnityEngine;
using UnityEngine.Rendering;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Pool", menuName = "BaseDefence/CD_Pool",
        order = 0)]
    public class CD_Pool : ScriptableObject
    {
        public SerializedDictionary<PoolObjectType,PoolData> PoolDataDic = new SerializedDictionary<PoolObjectType,PoolData>();
    }
}
