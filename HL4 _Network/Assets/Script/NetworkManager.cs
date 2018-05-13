using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkManager : Photon.PunBehaviour {


    private string GameVersion = "0.0.1";
    private string RoomName = "onlyRoom";
    public byte MaxPlayersInRoom = 20;

    public void Awake()
    {
        PhotonNetwork.autoJoinLobby = false;
        PhotonNetwork.automaticallySyncScene = true;
    }

    // Use this for initialization
    void Start () {
        Connect();
	}

    public void Connect()
    {
        if (PhotonNetwork.connected)
        {
            PhotonNetwork.JoinOrCreateRoom(RoomName, new RoomOptions() { MaxPlayers = MaxPlayersInRoom }, null);
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings(GameVersion);
        }
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinOrCreateRoom(RoomName, new RoomOptions() { MaxPlayers = MaxPlayersInRoom }, null);
    }

    public override void OnJoinedRoom()
    {
        SceneManager.LoadScene("main");
    }
}
