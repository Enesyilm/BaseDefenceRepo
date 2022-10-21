using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Abstracts
{
    public abstract class AStacker:MonoBehaviour
    {
        public  List<GameObject> StackList = new List<GameObject>();

        public virtual void SetStackHolder(Transform otherTransform)
        {
            otherTransform.SetParent(transform);
        }

        public virtual void GetStack(GameObject stackableObj)
        {

        }
        public virtual void GetStack(GameObject stackableObj,Transform otherTransform)
        {

        }


        public virtual void GetAllStack(IStackbase stack)
        {

        }

        public virtual void RemoveStack(IStackable stackable)
        {

        }


        public virtual void ResetStack(IStackable stackable)
        {

        }
    }
}