using Enum;
using UnityEngine;
using Enums;

namespace Interfaces
{
    public interface IReleasePoolObject
    {
        void ReleaseObject(GameObject obj, PoolObjectType poolType);

    } 
}
