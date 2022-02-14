using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    // Back Buttons
    [SerializeField]
    Button BackToMenuBtn;
    [SerializeField]
    Button BackToOptionsBtn;

    [SerializeField]
    Text ConnectingTxt;

    GameManager gm;

    ConnectionController cc;

    private void Start(){
        StartUI();

        cc = GetComponent<ConnectionController>();

        gm = FindObjectOfType<GameManager>();

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

        BackToMenuBtn.gameObject.SetActive(false);
        BackToOptionsBtn.gameObject.SetActive(false);

        ConnectingTxt.gameObject.SetActive(false);    

        OptionsPanel.SetActive(false);
        CreateRoomPanel.SetActive(false);
        JoinRoomPanel.SetActive(false);
        JoinRdmRoomPanel.SetActive(false);
        RoomListPanel.SetActive(false);
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
        Debug.Log("Clicked Join Random");
        JoinRdmNickName.gameObject.SetActive(false);
        JoinRdmBtn.gameObject.SetActive(false);
        
        ConnectingTxt.gameObject.SetActive(true);

        cc.ConnectRandom(JoinRdmNickName.text);
    }

    public void OnClickJoinFromRoomList(){
        
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
}