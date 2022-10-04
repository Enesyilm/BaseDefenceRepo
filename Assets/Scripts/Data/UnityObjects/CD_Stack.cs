using UnityEngine;

namespace Data.UnityObjects
{
    [CreateAssetMenu(fileName = "CD_Stack", menuName = "BaseDefence/CD_Stack", order = 0)]
    public class CD_Stack : ScriptableObject
    {
        public StackData[] StackDatas = new StackData[2];
    }
}