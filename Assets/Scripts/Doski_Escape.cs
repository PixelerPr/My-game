using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doski_Escape : MonoBehaviour
{
	public GameObject player;
	FPS pscr;
	public GameObject doska;
	public GameObject lom;
	Take tscript;
	public GameObject finish;
	Finish1 fscr;
    AudioSource audio;
    Animator animator;
	public string anim;


	// Start is called before the first frame update
	void Start()
    {
		tscript = lom.GetComponent<Take>();
		pscr = player.GetComponent<FPS>();
		fscr = finish.GetComponent<Finish1>();
		animator = GetComponent<Animator>();
		audio= GetComponent<AudioSource>();
	}

    // Update is called once per frame
    void Update()
    {

	}

	public void Use_Lom()
	{
		if (tscript.name_item == pscr.name_item_inHands)
		{
			fscr.step_for_finish += 1;
			StartCoroutine("Use_Lom_COR");
		}
	}
	IEnumerator Use_Lom_COR()
	{
		animator.Play(anim);
		yield return new WaitForSeconds(4);
		Destroy(doska);
		
	}
}
