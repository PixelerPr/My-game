using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tumbochka : MonoBehaviour
{

	[SerializeField] private Animator anim;
	[SerializeField] private AudioSource audio;
	[SerializeField] private AudioClip open_clip;
	[SerializeField] private AudioClip close_clip;
	public string Open;
	public string Close;
	public bool firstAnimIncluded = true;

	// Start is called before the first frame update
	void Start()
    {
		anim = GetComponent<Animator>();
		audio = GetComponent<AudioSource>();
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	public void Open_Close()
	{


		if (firstAnimIncluded == true)
		{
			anim.Play(Open);
			audio.PlayOneShot(open_clip);
			firstAnimIncluded = false;

		}
		else
		{
			anim.Play(Close);
			audio.PlayOneShot(close_clip);
			firstAnimIncluded = true;
		}



	}
}
