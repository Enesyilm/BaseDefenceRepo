using UnityEngine;

namespace Commands
{
    public class LevelLoaderCommand : MonoBehaviour
        {
            public void InitializeLevel(int levelID, Transform levelHolder)
            {
                Instantiate(Resources.Load<GameObject>($"Prefabs/LevelPrefabs/Level{levelID}"), levelHolder);
            }
        }
}