using UnityEngine;

namespace Interfaces
{
    public interface IStackbase
    {
        void SetStackHolder(Transform otherTransform);
        void GetStack(GameObject stackableObj);
        void GetAllStack(IStackbase stack);
        void RemoveStack(IStackable stackable);

        void ResetStack(IStackable stackable);
    }
}