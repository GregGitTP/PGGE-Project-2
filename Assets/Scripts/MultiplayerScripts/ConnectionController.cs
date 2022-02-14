using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ConnectionController : MonoBehaviourPunCallbacks
{
    GameManager gm;
    LobbyUI lobbyUI;

    enum Intent{None, Create, Join, Random, RoomList};
    Intent intent = Intent.None;

    bool isConnecting;
    string roomName = "";

    private void Awake(){
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start(){
        gm = FindObjectOfType<GameManager>();
        lobbyUI = GetComponent<LobbyUI>();
        isConnecting = PhotonNetwork.ConnectUsingSettings();
    }

    public void ConnectCreate(string _roomName, string name){
        intent = Intent.Create;
        PhotonNetwork.NickName = name;
        PhotonNetwork.GameVersion = Application.version;
        roomName = _roomName;

        if(PhotonNetwork.IsConnected){
            PhotonNetwork.CreateRoom(
                roomName,
                new RoomOptions(){
                    MaxPlayers = 4
                }
            );
        }
        else{
            isConnecting = PhotonNetwork.ConnectUsingSettings();
        }
    }

    public void Connect(string _roomName, string name){
        intent = Intent.Join;
        PhotonNetwork.NickName = name;
        PhotonNetwork.GameVersion = Application.version;
        roomName = _roomName;

        if(PhotonNetwork.IsConnected){
            PhotonNetwork.JoinRoom(roomName);
        }
        else{
            isConnecting = PhotonNetwork.ConnectUsingSettings();
        }
    }

    public void ConnectRandom(string name){
        intent = Intent.Random;
        PhotonNetwork.NickName = name;
        PhotonNetwork.GameVersion = Application.version;

        if(PhotonNetwork.IsConnected){
            PhotonNetwork.JoinRandomRoom();
        }
        else{
            isConnecting = PhotonNetwork.ConnectUsingSettings();
        }
    }

    public void ConnectFromRoomList(string name, string _roomName){
        intent = Intent.RoomList;
        PhotonNetwork.NickName = name;
        PhotonNetwork.GameVersion = Application.version;
        roomName = _roomName;

        if(PhotonNetwork.IsConnected){
            PhotonNetwork.JoinOrCreateRoom(
                roomName,
                new RoomOptions(){
                    MaxPlayers = 4
                },
                TypedLobby.Default
            );
        }
        else{
            isConnecting = PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster(){
        if(isConnecting){
            if(intent == Intent.None){
                PhotonNetwork.JoinLobby();
            }
            else if(intent == Intent.Create){
                PhotonNetwork.CreateRoom(
                    roomName,
                    new RoomOptions(){
                        MaxPlayers = 4
                    }
                );
            }
            else if(intent == Intent.Join){
                PhotonNetwork.JoinRoom(roomName);
            }
            else if(intent == Intent.Random){
                PhotonNetwork.JoinRandomRoom();
            }
            else if(intent == Intent.RoomList){
                PhotonNetwork.JoinOrCreateRoom(
                    roomName,
                    new RoomOptions(){
                        MaxPlayers = 4
                    },
                    TypedLobby.Default
                );
            }
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList){
        lobbyUI.UpdateRoomList(roomList);
    }

    public override void OnCreateRoomFailed(short returnCode, string message){
        lobbyUI.CreateFail();
    }

    public override void OnJoinRoomFailed(short returnCode, string message){
        lobbyUI.JoinFail();
    }

    public override void OnJoinRandomFailed(short returnCode, string message){
        PhotonNetwork.CreateRoom(
            null,
            new RoomOptions(){
                MaxPlayers = 4
            }
        );
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
