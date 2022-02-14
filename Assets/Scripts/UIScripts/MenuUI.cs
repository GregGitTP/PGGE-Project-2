using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    [SerializeField]
    Button SingleplayerBtn;
    [SerializeField]
    Button MultiplayerBtn;

    GameManager gm;

    private void Start(){
        gm = FindObjectOfType<GameManager>();

        SingleplayerBtn.onClick.AddListener(
            delegate{
                OnClickSingleplayer();
                gm.ButtonSoundEffect();
            }
        );
        MultiplayerBtn.onClick.AddListener(
            delegate{
                OnClickMultiplayer();
                gm.ButtonSoundEffect();
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
