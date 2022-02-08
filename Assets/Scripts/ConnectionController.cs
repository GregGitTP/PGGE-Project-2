using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ConnectionController : MonoBehaviourPunCallbacks
{
    public byte MaxPlayersPerRoom = 4;

    bool isConnecting;

    private void Awake(){
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void Connect(string name){
        PhotonNetwork.NickName = name;
        PhotonNetwork.GameVersion = Application.version;

        if(PhotonNetwork.IsConnected){
            PhotonNetwork.JoinRandomRoom();
        }
        else{
            isConnecting = PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message){
        PhotonNetwork.CreateRoom(
            null,
            new RoomOptions(){
                MaxPlayers = MaxPlayersPerRoom
            }
        );
    }

    public override void OnConnectedToMaster(){
        if(isConnecting){
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnDisconnected(DisconnectCause cause){
        isConnecting = false;
    }

    public override void OnJoinedRoom(){
        if(PhotonNetwork.IsMasterClient){
            PhotonNetwork.LoadLevel("MultiplayerLevel1");
        }
    }
}
