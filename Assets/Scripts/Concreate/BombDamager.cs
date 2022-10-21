using Interfaces;
using UnityEngine;

namespace Concreate
{
    public class BombDamager : MonoBehaviour,IDamager
    {
        public int GetDamage()
        {
            return 25;
        }
    }
}