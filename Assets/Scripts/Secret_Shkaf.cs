using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Secret_Shkaf : MonoBehaviour
{
    public GameObject[] knigi;
    public GameObject shkaf;
    public GameObject player;
    public GameObject needBook1;
    public GameObject needBook2;
    public GameObject needBook3;
    int cntKnig = 0;
    Take b1scr;
    Take b2scr;
    Take b3scr;
    FPS plscr;
    AudioSource audio;
    Animator animator;
    public string anim;
    public AudioClip clip;



    // Start is called before the first frame update
    void Start()
    {
        plscr = player.GetComponent<FPS>();
        b1scr = needBook1.GetComponent<Take>();
        b2scr= needBook2.GetComponent<Take>();
        b3scr= needBook3.GetComponent<Take>();
        animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
		if (cntKnig == 3)
		{
			animator.Play(anim);
            audio.PlayOneShot(clip);
			Destroy(shkaf.GetComponent<Secret_Shkaf>());
            cntKnig++;
		}
	}

    public void Insert_Book()
    {
        if(b1scr.name_item == plscr.name_item_inHands)
        {
            Debug.Log("Пытаемся вставить книгу");
            Destroy(needBook1);
			plscr.name_item_inHands = null;
			plscr.handEmpty = true;
			knigi[0].SetActive(true);
            cntKnig++;
        }
		if (b2scr.name_item == plscr.name_item_inHands)
		{
			Destroy(needBook2);
			plscr.name_item_inHands = null;
			plscr.handEmpty = true;
			knigi[1].SetActive(true);
            cntKnig++;
		}
		if (b3scr.name_item == plscr.name_item_inHands)
		{
			Destroy(needBook3);
			plscr.name_item_inHands = null;
			plscr.handEmpty = true;
			knigi[2].SetActive(true);
            cntKnig++;
		}

	}
}
