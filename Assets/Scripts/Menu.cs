using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]
    Button SingleplayerBtn;
    [SerializeField]
    Button MultiplayerBtn;

    private void Start(){
        SingleplayerBtn.onClick.AddListener(
            delegate{
                OnClickSingleplayer();
            }
        );
        MultiplayerBtn.onClick.AddListener(
            delegate{
                OnClickMultiplayer();
            }
        );
    }

    private void OnClickSingleplayer(){
        SceneManager.LoadScene("Singleplayer");
    }
    
    private void OnClickMultiplayer(){
        SceneManager.LoadScene("Lobby");
    }
}
