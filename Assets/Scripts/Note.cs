using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    public string text;
    [SerializeField] private GameObject noteUI;
    [SerializeField] private GameObject plr;
    //[SerializeField] private GameObject menu;
    public TMP_Text text_comp;
    public bool isOpen = false;
    private FPS nfps;
    float t;
    float t2;


	void Start()
    {
        nfps = plr.GetComponent<FPS>();
		
	}

    public void Nnote()
    {
        Debug.Log("�����:" + Time.timeScale);
		if (isOpen == false)
        {
			text_comp.text = text;
            //menu.SetActive(false);
			noteUI.SetActive(true);
            Time.timeScale = 0f;
            isOpen = true;
            t = nfps.sensivity;
            t2 = nfps.speed_move;
            nfps.sensivity = 0;
            nfps.speed_move = 0;
            
		}
        else
        {
            noteUI.SetActive(false);
			//menu.SetActive(true);
			Time.timeScale = 1f;
            isOpen = false;
            nfps.sensivity = t;
            nfps.speed_move = t2;
        }
    }
}
