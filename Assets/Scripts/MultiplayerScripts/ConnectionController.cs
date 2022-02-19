using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ConnectionController : MonoBehaviourPunCallbacks
{
    GameManager gm;
    LobbyUI lobbyUI;

    // Variables to keep track of what actions to perform
    // after the connection has been made to photon servers
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

    // Function for creating a new room with the room name provided
    // and allowing the player to join the newly created room
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

    // Function for joining a room by the room name provided
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

    // Function for joining a random room in the photon room list
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

    // Function for joining a room from the room list UI
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

    // Callback function upon making connection
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

        // Calling the respective callback function in the LobbyUI script
        lobbyUI.UpdateRoomList(roomList);
    }

    public override void OnCreateRoomFailed(short returnCode, string message){

        // Calling the respective callback function in the LobbyUI script
        lobbyUI.CreateFail();
    }

    public override void OnJoinRoomFailed(short returnCode, string message){

        // Calling the respective callback function in the LobbyUI script
        lobbyUI.JoinFail();
    }

    public override void OnJoinRandomFailed(short returnCode, string message){

        // Creating a new room if there are
        // no rooms available for random joining
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
