using UnityEngine;
using Enums;
using System;
using Enum;

namespace Interfaces
{
    public interface IGetPoolObject
    {
        GameObject GetObjectType(PoolObjectType poolType);
    }
}
