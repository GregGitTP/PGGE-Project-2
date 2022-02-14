using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PGGE.Patterns;

public class GameManager : Singleton<GameManager>
{
    public List<string> Rooms = new List<string>();

    [SerializeField]
    AudioClip buttonClick;

    AudioSource audioSource;

    private void Start(){
        audioSource = GetComponent<AudioSource>();

        SceneManager.LoadScene("Menu");
    }

    public void ButtonSoundEffect(){
        audioSource.PlayOneShot(buttonClick);
    }
}
