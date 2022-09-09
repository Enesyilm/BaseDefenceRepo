using Data.ValueObjects.PlayerData;
using UnityEngine;

namespace Data.UnityObjects
{
    [CreateAssetMenu(fileName = "CD_Player", menuName = "BaseDefence/CD_Player", order = 0)]
    public class CD_Player : ScriptableObject
    {
        public PlayerData PlayerData;
    }
}