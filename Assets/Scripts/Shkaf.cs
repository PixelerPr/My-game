using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shkaf : MonoBehaviour
{
	//public GameObject info;
	//private New_Interact inter;
	[SerializeField] private Animator anim;
	[SerializeField] private AudioSource audio;
	[SerializeField] private AudioClip open_clip;
	[SerializeField] private AudioClip close_clip;
	public string Open_Left;
	public string Close_Left;
	public bool firstAnimIncluded = true;

	// Start is called before the first frame update
	void Start()
	{
		anim = GetComponent<Animator>();
		audio = GetComponent<AudioSource>();
		//inter = info.GetComponent<New_Interact>();


	}

	public void Open_Close()
	{
		
		
			if (firstAnimIncluded == true)
			{
				anim.Play(Open_Left);
				audio.PlayOneShot(open_clip);
				firstAnimIncluded = false;

			}
			else
			{
				anim.Play(Close_Left);
				audio.PlayOneShot(close_clip);
				firstAnimIncluded = true;
			}
		


	}
}
