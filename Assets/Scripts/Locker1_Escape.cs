using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locker1_Escape : MonoBehaviour
{
    public GameObject player;
    FPS plscr;
    public GameObject locker;
    public GameObject balka;
    public GameObject finish;
    Finish1 fscr;
    public int need_key;
    Animator animator;
    AudioSource audio;
    public string anim;



    // Start is called before the first frame update
    void Start()
    {
        plscr = player.GetComponent<FPS>();
        animator = balka.GetComponent<Animator>();
        fscr = finish.GetComponent<Finish1>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open_Locker_Escape()
    {
        if (plscr.keys[need_key] == true)
        {
            fscr.step_for_finish += 1;
            StartCoroutine("Open_Locker_Escape_COR");
        }
    }
    IEnumerator Open_Locker_Escape_COR()
    {
		
		animator.Play(anim);
        yield return new WaitForSeconds(2);
        Destroy(balka.gameObject);
		Destroy(locker);
	}
}
