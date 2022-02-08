using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lobby : MonoBehaviour
{
    [SerializeField]
    InputField InputName;
    [SerializeField]
    Button JoinBtn;
    [SerializeField]
    Text ConnectingTxt;

    private void Start(){
        JoinBtn.onClick.AddListener(
            delegate(){
                OnClickJoinRoom();
            }
        );
    }

    private void OnClickJoinRoom(){
        ConnectingTxt.gameObject.SetActive(true);
        JoinBtn.gameObject.SetActive(false);
        InputName.gameObject.SetActive(false);

        string playerName = InputName.text;

        GetComponent<ConnectionController>().Connect(playerName);
    }
}