using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight_Pick : MonoBehaviour
{
    [SerializeField] private GameObject flash;
	//[SerializeField] private AudioSource audio;
	// Start is called before the first frame update
	void Start()
    {
        //audio = GetComponent<AudioSource>();
    }

    public void Flash_Pick()
    {
		flash.SetActive(true);
		//audio.Play();
		Destroy(gameObject);
	}
}
