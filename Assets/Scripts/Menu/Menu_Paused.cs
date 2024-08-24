using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Paused : MonoBehaviour
{
    public GameObject Menu;
    public GameObject crosshair;
    [SerializeField] KeyCode keyMenu;
    bool isMenuPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        Menu.SetActive(false);
    }
    void Update()
    {
        ActiveMenu();
        
    }

    void ActiveMenu()
    {
        if (Input.GetKeyDown(keyMenu))
        {
            isMenuPaused = !isMenuPaused;
        }
        if (isMenuPaused)
        {
            Menu.SetActive(true);
            crosshair.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
        }
        else
        {
            Menu.SetActive(false);
            crosshair.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
        }
    }

    public void Menu_Resume()
    {
        isMenuPaused = false;
        
    }
    public void Menu_Quit()
    {
		Time.timeScale = 1f;
        SceneManager.LoadScene(0);
	}

}
