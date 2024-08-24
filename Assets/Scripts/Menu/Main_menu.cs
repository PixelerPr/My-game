using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main_menu : MonoBehaviour
{

    private AudioSource audio_button;

    //public GameObject Multiplayer_menu;

    // Start is called before the first frame update
    void Start()
    {
        audio_button= GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void BUTTON_New_Game()
    {
        audio_button.Play();
        SceneManager.LoadScene(1);
    }
    public void BUTTON_Continue_Game()
    {
        audio_button.Play();
    }
    public void BUTTON_Options_Game()
    {
		audio_button.Play();
	}
    public void BUTTON_Multiplayer_Game()
    {
		audio_button.Play();
        SceneManager.LoadScene(2);

	}
    public void BUTTON_Quit_Game() 
    {
		audio_button.Play();
        Application.Quit();
	}
}
