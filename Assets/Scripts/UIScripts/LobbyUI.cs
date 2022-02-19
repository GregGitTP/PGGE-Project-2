using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using Photon.Realtime;

public class LobbyUI : MonoBehaviour
{
    // Variables referencing UI objects
    [SerializeField]
    GameObject OptionsPanel;

    [SerializeField]
    GameObject CreateRoomPanel;
    [SerializeField]
    GameObject JoinRoomPanel;
    [SerializeField]
    GameObject JoinRdmRoomPanel;
    [SerializeField]
    GameObject RoomListPanel;

    // Options UI
    [SerializeField]
    Button CreateRoomBtn;
    [SerializeField]
    Button JoinRoomBtn;
    [SerializeField]
    Button JoinRdmRoomBtn;
    [SerializeField]
    Button RoomListBtn;

    // Create Room UI
    [SerializeField]
    InputField CreateRoomName;
    [SerializeField]
    InputField CreateNickName;
    [SerializeField]
    Button CreateBtn;
    [SerializeField]
    Text CreateFailTxt;
    
    // Join Room UI
    [SerializeField]
    InputField JoinRoomName;
    [SerializeField]
    InputField JoinNickName;
    [SerializeField]
    Button JoinBtn;
    [SerializeField]
    Text JoinFailTxt;

    // Join Random Room UI
    [SerializeField]
    InputField JoinRdmNickName;
    [SerializeField]
    Button JoinRdmBtn;

    // Room List UI
    [SerializeField]
    GameObject RoomListing;
    [SerializeField]
    Text SelectedRoomTxt;
    [SerializeField]
    Text RoomListErrorTxt;
    [SerializeField]
    InputField RoomListNickName;
    [SerializeField]
    Button JoinRoomListBtn;

    [SerializeField]
    Transform RoomListingContent;
    [SerializeField]
    GameObject RoomPrefab;

    // Back Buttons
    [SerializeField]
    Button BackToMenuBtn;
    [SerializeField]
    Button BackToOptionsBtn;

    [SerializeField]
    Text ConnectingTxt;

    [SerializeField]
    List<GameObject> FixedRooms;

    List<GameObject> roomListing;

    GameManager gm;
    ConnectionController cc;

    string rlRoomName;

    private void Start(){
        StartUI();

        cc = GetComponent<ConnectionController>();

        gm = FindObjectOfType<GameManager>();

        roomListing = new List<GameObject>();

        // Setting the button on click functions for
        // option page navigation buttons
        CreateRoomBtn.onClick.AddListener(
            delegate{
                gm.ButtonSoundEffect();
                OnClickCreateRoom();
            }
        );
        JoinRoomBtn.onClick.AddListener(
            delegate{
                gm.ButtonSoundEffect();
                OnClickJoinRoom();
            }
        );
        JoinRdmRoomBtn.onClick.AddListener(
            delegate{
                gm.ButtonSoundEffect();
                OnClickJoinRdmRoom();
            }
        );
        RoomListBtn.onClick.AddListener(
            delegate{
                gm.ButtonSoundEffect();
                OnClickRoomList();
            }
        );

        // Setting the button on click functions for
        // Respective page button actions
        CreateBtn.onClick.AddListener(
            delegate{
                gm.ButtonSoundEffect();
                OnClickCreate();
            }
        );
        JoinBtn.onClick.AddListener(
            delegate{
                gm.ButtonSoundEffect();
                OnClickJoin();
            }
        );
        JoinRdmBtn.onClick.AddListener(
            delegate{
                gm.ButtonSoundEffect();
                OnClickJoinRdm();
            }
        );
        JoinRoomListBtn.onClick.AddListener(
            delegate{
                gm.ButtonSoundEffect();
                OnClickJoinFromRoomList();
            }
        );

        // Setting the button on click functions for
        // back button actions
        BackToMenuBtn.onClick.AddListener(
            delegate{
                gm.ButtonSoundEffect();
                OnClickBackToMenu();
            }
        );
        BackToOptionsBtn.onClick.AddListener(
            delegate{
                gm.ButtonSoundEffect();
                OnClickBackToOptions();
            }
        );
    }

    // Function thats called when a player selects
    // a room from the room list
    public void OnClickRoomFromRoomList(){
        GameObject selectedRoom = EventSystem.current.currentSelectedGameObject;
        GameObject selectedRoomName = selectedRoom.transform.Find("Name").gameObject;
        rlRoomName = selectedRoomName.GetComponent<Text>().text;
        SelectedRoomTxt.text = "Selected Room: " + rlRoomName;
    }

    // Function for getting all elements ready at the start
    private void StartUI(){
        ResetAndOffAllUI();
        OptionsPanel.SetActive(true);
        BackToMenuBtn.gameObject.SetActive(true);
    }

    // Function for resetting all of the UI
    // and turning all of them off
    private void ResetAndOffAllUI(){
        CreateRoomBtn.gameObject.SetActive(true);
        JoinRoomBtn.gameObject.SetActive(true);
        JoinRdmRoomBtn.gameObject.SetActive(true);
        RoomListBtn.gameObject.SetActive(true);

        CreateRoomName.gameObject.SetActive(true);
        CreateNickName.gameObject.SetActive(true);
        CreateBtn.gameObject.SetActive(true);
        CreateFailTxt.gameObject.SetActive(false);

        CreateRoomName.text = "";
        CreateNickName.text = "";

        JoinRoomName.gameObject.SetActive(true);
        JoinNickName.gameObject.SetActive(true);
        JoinBtn.gameObject.SetActive(true);
        JoinFailTxt.gameObject.SetActive(false);

        JoinRoomName.text = "";
        JoinNickName.text = "";
        
        JoinRdmNickName.gameObject.SetActive(true);
        JoinRdmBtn.gameObject.SetActive(true);

        JoinRdmNickName.text = "";

        RoomListing.SetActive(true);
        SelectedRoomTxt.gameObject.SetActive(true);
        RoomListErrorTxt.gameObject.SetActive(false);
        RoomListNickName.gameObject.SetActive(true);
        JoinRoomListBtn.gameObject.SetActive(true);

        SelectedRoomTxt.text = "Selected Room: ";
        RoomListNickName.text = "";

        BackToMenuBtn.gameObject.SetActive(false);
        BackToOptionsBtn.gameObject.SetActive(false);

        ConnectingTxt.gameObject.SetActive(false);    

        OptionsPanel.SetActive(false);
        CreateRoomPanel.SetActive(false);
        JoinRoomPanel.SetActive(false);
        JoinRdmRoomPanel.SetActive(false);
        RoomListPanel.SetActive(false);
        
        rlRoomName = null;
    }

    // Option page navigation button functions
    private void OnClickCreateRoom(){
        ResetAndOffAllUI();
        BackToOptionsBtn.gameObject.SetActive(true);
        CreateRoomPanel.SetActive(true);
    }

    private void OnClickJoinRoom(){
        ResetAndOffAllUI();
        BackToOptionsBtn.gameObject.SetActive(true);
        JoinRoomPanel.SetActive(true);
    }

    private void OnClickJoinRdmRoom(){
        ResetAndOffAllUI();
        BackToOptionsBtn.gameObject.SetActive(true);
        JoinRdmRoomPanel.SetActive(true);
    }

    private void OnClickRoomList(){
        ResetAndOffAllUI();
        BackToOptionsBtn.gameObject.SetActive(true);
        RoomListPanel.SetActive(true);
    }

    // Respective pages button action functions
    private void OnClickCreate(){
        CreateRoomName.gameObject.SetActive(false);
        CreateNickName.gameObject.SetActive(false);
        CreateBtn.gameObject.SetActive(false);
        CreateFailTxt.gameObject.SetActive(false);
        
        ConnectingTxt.gameObject.SetActive(true);
        
        // Calling the respective ConnectionController function
        // that corresponds to its buttons
        cc.ConnectCreate(CreateRoomName.text, CreateNickName.text);
    }

    private void OnClickJoin(){
        JoinRoomName.gameObject.SetActive(false);
        JoinNickName.gameObject.SetActive(false);
        JoinBtn.gameObject.SetActive(false);
        JoinFailTxt.gameObject.SetActive(false);
        
        ConnectingTxt.gameObject.SetActive(true);

        // Calling the respective ConnectionController function
        // that corresponds to its buttons
        cc.Connect(JoinRoomName.text, JoinNickName.text);
    }

    private void OnClickJoinRdm(){
        JoinRdmNickName.gameObject.SetActive(false);
        JoinRdmBtn.gameObject.SetActive(false);
        
        ConnectingTxt.gameObject.SetActive(true);

        // Calling the respective ConnectionController function
        // that corresponds to its buttons
        cc.ConnectRandom(JoinRdmNickName.text);
    }

    private void OnClickJoinFromRoomList(){
        if(rlRoomName == null){
            RoomListErrorTxt.gameObject.SetActive(true);
            return;
        }

        RoomListing.SetActive(false);
        SelectedRoomTxt.gameObject.SetActive(false);
        RoomListErrorTxt.gameObject.SetActive(false);
        RoomListNickName.gameObject.SetActive(false);
        JoinRoomListBtn.gameObject.SetActive(false);

        ConnectingTxt.gameObject.SetActive(true);
        
        // Calling the respective ConnectionController function
        // that corresponds to its buttons
        cc.ConnectFromRoomList(RoomListNickName.text, rlRoomName);
    }

    // Back button action functions
    private void OnClickBackToMenu(){
        SceneManager.LoadScene("Menu");
    }

    private void OnClickBackToOptions(){
        ResetAndOffAllUI();
        BackToMenuBtn.gameObject.SetActive(true);
        OptionsPanel.SetActive(true);
    }

    // Functions that call upon callbacks
    // from Photon from the connection controller script
    // in the case of a fail in the action
    public void CreateFail(){
        OnClickCreateRoom();
        CreateFailTxt.gameObject.SetActive(true);
    }
    
    public void JoinFail(){
        OnClickJoinRoom();
        JoinFailTxt.gameObject.SetActive(true);
    }

    // Function that calls when the Photon room list
    // is updated and updates the displayed room list
    // for the user to see
    public void UpdateRoomList(List<RoomInfo> roomList){

        // Checking if the player has opened the room list panel
        // from the options menu

        // Prevents null object reference when room list is updated
        // while the player is in the options menu and the room list
        // UI is turned off
        if(!RoomListPanel.active){

            // Calls a coroutine that checks every half a second
            StartCoroutine(DelayCor(roomList));
            return;
        }

        // Looping through all the rooms
        foreach(RoomInfo info in roomList){

            // Checking if the room is a pre made room or a custom made room
            if(!info.Name.Contains("Fixed Room")){
                
                // Checking if the room was added or removed
                if(info.RemovedFromList){

                    // Looping through all the UI objects in the
                    // room list that is displayed to the player
                    foreach(GameObject room in roomListing){

                        // Removing the object from the list if it
                        // is found to be removed from photon's room list
                        if(info.Name == room.GetComponent<RoomListUI>().GetName()){
                            Destroy(room);
                            roomListing.Remove(room);
                            break;
                        }
                    }
                }
                else{

                    // Getting all the names of the rooms that have
                    // already been in the UI room list
                    List<string> names = new List<string>();
                    foreach(GameObject existingRoom in roomListing){
                        names.Add(existingRoom.GetComponent<RoomListUI>().GetName());
                    }

                    // Adding the newly created rooms to the UI room list
                    if(!names.Contains(info.Name)){
                        GameObject room = Instantiate(RoomPrefab, RoomListingContent);
                        roomListing.Add(room);
                        room.GetComponent<RoomListUI>().SetName(info.Name);
                    }
                }
            }
        }
        UpdateRoomSpace(roomList);
    }

    // Coroutine for regularly checking
    // if the rooms were updated while the
    // room list UI was turned off
    private IEnumerator DelayCor(List<RoomInfo> roomList){
        yield return new WaitForSeconds(.5f);
        UpdateRoomList(roomList);
        yield break;
    }

    // Function to update the number of players in the rooms
    private void UpdateRoomSpace(List<RoomInfo> roomList){

        // Resetting the player count of the pre-made rooms
        // in the case of the pre made rooms not having
        // any players and are removed from the photon room list
        // thus not getting updated
        foreach(GameObject fixedRoom in FixedRooms){
            fixedRoom.GetComponent<RoomListUI>().SetPlayerCount(0);
        }

        // Looping through ever room in the Photon room list
        foreach(RoomInfo info in roomList){

            // Checking if the room is a pre-made or a custom made room
            if(info.Name.Contains("Fixed Room")){

                // Finding the specific room by its name
                foreach(GameObject fixedRoom in FixedRooms){
                    RoomListUI fixedRoomScript = fixedRoom.GetComponent<RoomListUI>();
                    if(info.Name == fixedRoomScript.GetName()){
                        
                        // Calling the function for setting the player count
                        fixedRoomScript.SetPlayerCount(info.PlayerCount);
                        break;
                    }
                }
            }
            else{

                // Finding the specific room by its name
                foreach(GameObject room in roomListing){
                    RoomListUI roomScript = room.GetComponent<RoomListUI>();
                    if(info.Name == roomScript.GetName()){

                        // Calling the function for setting the player count
                        roomScript.SetPlayerCount(info.PlayerCount);
                    }
                }
            }
        }
    }
}