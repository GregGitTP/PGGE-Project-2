using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomListUI : MonoBehaviour
{
    LobbyUI lobbyUI;
    Button thisBtn;

    private void Awake(){
        lobbyUI = FindObjectOfType<LobbyUI>();
        thisBtn = GetComponent<Button>();
    }

    private void Start(){
        thisBtn.onClick.AddListener(
            delegate{
                lobbyUI.OnClickRoomFromRoomList();
            }
        );
    }

    public string GetName(){
        return transform.Find("Name").gameObject.GetComponent<Text>().text;
    }

    public void SetName(string _name){
        GameObject name = transform.Find("Name").gameObject;
        name.GetComponent<Text>().text = _name;
    }

    public void SetPlayerCount(int count){
        if(count >= 4){
            thisBtn.interactable = false;
        }
        else{
            thisBtn.interactable = true;
        }
        GameObject players = transform.Find("Players").gameObject;
        players.GetComponent<Text>().text = count + " / 4  ";
    }
}
