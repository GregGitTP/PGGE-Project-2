using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using Photon.Realtime;

public class LobbyUI : MonoBehaviour
{
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

    GameManager gm;

    ConnectionController cc;

    [SerializeField]
    List<GameObject> FixedRooms;

    List<GameObject> roomListing;

    string rlRoomName;

    private void Start(){
        StartUI();

        cc = GetComponent<ConnectionController>();

        gm = FindObjectOfType<GameManager>();

        roomListing = new List<GameObject>();

        // Option page navigation buttons
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

        // Respective button actions
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

        // Back button actions
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

    public void OnClickRoomFromRoomList(){
        GameObject selectedRoom = EventSystem.current.currentSelectedGameObject;
        GameObject selectedRoomName = selectedRoom.transform.Find("Name").gameObject;
        rlRoomName = selectedRoomName.GetComponent<Text>().text;
        SelectedRoomTxt.text = "Selected Room: " + rlRoomName;
    }

    private void StartUI(){
        ResetAndOffAllUI();
        OptionsPanel.SetActive(true);
        BackToMenuBtn.gameObject.SetActive(true);
    }

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

    // Respective button action functions
    private void OnClickCreate(){
        CreateRoomName.gameObject.SetActive(false);
        CreateNickName.gameObject.SetActive(false);
        CreateBtn.gameObject.SetActive(false);
        CreateFailTxt.gameObject.SetActive(false);
        
        ConnectingTxt.gameObject.SetActive(true);

        cc.ConnectCreate(CreateRoomName.text, CreateNickName.text);
    }

    private void OnClickJoin(){
        JoinRoomName.gameObject.SetActive(false);
        JoinNickName.gameObject.SetActive(false);
        JoinBtn.gameObject.SetActive(false);
        JoinFailTxt.gameObject.SetActive(false);
        
        ConnectingTxt.gameObject.SetActive(true);

        cc.Connect(JoinRoomName.text, JoinNickName.text);
    }

    private void OnClickJoinRdm(){
        JoinRdmNickName.gameObject.SetActive(false);
        JoinRdmBtn.gameObject.SetActive(false);
        
        ConnectingTxt.gameObject.SetActive(true);

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

    // Fail sudo callback functions
    public void CreateFail(){
        OnClickCreateRoom();
        CreateFailTxt.gameObject.SetActive(true);
    }
    
    public void JoinFail(){
        OnClickJoinRoom();
        JoinFailTxt.gameObject.SetActive(true);
    }

    // Room List update fucntions
    public void UpdateRoomList(List<RoomInfo> roomList){
        if(!RoomListPanel.active){
            StartCoroutine(DelayCor(roomList));
            return;
        }
        foreach(RoomInfo info in roomList){
            if(!info.Name.Contains("Fixed Room")){
                if(info.RemovedFromList){
                    foreach(GameObject room in roomListing){
                        if(info.Name == room.GetComponent<RoomListUI>().GetName()){
                            Destroy(room);
                            roomListing.Remove(room);
                            break;
                        }
                    }
                }
                else{
                    List<string> names = new List<string>();
                    foreach(GameObject existingRoom in roomListing){
                        names.Add(existingRoom.GetComponent<RoomListUI>().GetName());
                    }
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

    private IEnumerator DelayCor(List<RoomInfo> roomList){
        yield return new WaitForSeconds(.5f);
        UpdateRoomList(roomList);
        yield break;
    }

    private void UpdateRoomSpace(List<RoomInfo> roomList){
        foreach(GameObject fixedRoom in FixedRooms){
            RoomListUI fixedRoomScript = fixedRoom.GetComponent<RoomListUI>();
            fixedRoomScript.SetPlayerCount(0);
        }
        foreach(RoomInfo info in roomList){
            if(info.Name.Contains("Fixed Room")){
                foreach(GameObject fixedRoom in FixedRooms){
                    RoomListUI fixedRoomScript = fixedRoom.GetComponent<RoomListUI>();
                    if(info.Name == fixedRoomScript.GetName()){
                        fixedRoomScript.SetPlayerCount(info.PlayerCount);
                        break;
                    }
                }
            }
            else{
                foreach(GameObject room in roomListing){
                    RoomListUI roomScript = room.GetComponent<RoomListUI>();
                    if(info.Name == roomScript.GetName()){
                        roomScript.SetPlayerCount(info.PlayerCount);
                    }
                }
            }
        }
    }
}