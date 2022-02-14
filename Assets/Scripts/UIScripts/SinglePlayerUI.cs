using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SinglePlayerUI : MonoBehaviour
{
    [SerializeField]
    Button BackBtn;

    GameManager gm;

    private void Start(){
        gm = FindObjectOfType<GameManager>();

        BackBtn.onClick.AddListener(
            delegate{
                OnClickBack();
                gm.ButtonSoundEffect();
            }
        );
    }

    private void OnClickBack(){
        SceneManager.LoadScene("Menu");
    }
}
