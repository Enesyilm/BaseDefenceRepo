using Abstracts;
using Interfaces;
using UnityEngine;

namespace Concreate
{
    public class StackableGem:AStackable
    {
        public override GameObject SendToStack(Transform transform1)
        {
            return transform.gameObject;
        }
    }
}