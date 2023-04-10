using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class SceneHandler : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI logText;
    void Start()
    {        
        PhotonNetwork.ConnectUsingSettings();
          
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Bağlantı koptu");      
    }
    public override void OnConnectedToMaster()
    {

        Debug.Log("Server'e Bağlanıldı.");
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {       

        Debug.Log("Lobiye bağlanıldı.");
        PhotonNetwork.NickName = "OLCAY";
        PhotonNetwork.JoinOrCreateRoom("oda isim", new RoomOptions { MaxPlayers = 2, IsOpen = true, IsVisible = true }, TypedLobby.Default);
    }  
    public override void OnJoinedRoom()
    {
        Debug.Log("Odaya Girildi.");
        PhotonNetwork.Instantiate("PlayerManager", Vector3.zero, Quaternion.identity);

    }
    public override void OnLeftLobby()
    {
        Debug.Log("Lobiden Çıkıldı.");

    }
    public override void OnLeftRoom()
    {
        Debug.Log("Odadan Çıkıldı.");      
    }  

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Odaya girilemedi." + message + " - " + returnCode);   
    
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Random Odaya girilemedi." + message + " - " + returnCode);

    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Oda oluşturulamadı." + message + " - " + returnCode);
    }
}
