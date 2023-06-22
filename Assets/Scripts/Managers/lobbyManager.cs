using System;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class lobbyManager : MonoBehaviour
    {
        [SerializeField]
        GameObject lobby;
        [SerializeField]
        Sprite openImg;
        [SerializeField]
        public SceneHandler sceneHandler;
        private int state = 0;
        [SerializeField]
        Sprite closeImg;
        [SerializeField]
        Image img;

       
        public void ChangeColor()
        {
            Debug.Log("ewew");
            SceneHandler.player.GetComponentInChildren<Material>().color = Color.gray;
        }
        public void CloseLobby()
        {
            Destroy(lobby);
        }
        public void openSound()
        {
            if (state == 0)
            {
                state = 1;
                img.sprite = closeImg;

            }
            else
            {
                state = 0;
                img.sprite = openImg;

            }

        }
    }
}