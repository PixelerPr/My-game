using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ruchka_Escape : MonoBehaviour
{
    public GameObject player;
    FPS plscr;
    public GameObject ruchka_item;
    Take tscript;
    public GameObject finish;
    Finish1 fscr;
    public GameObject ruchka_escape;
    public GameObject next_step;
    AudioSource audio;



    // Start is called before the first frame update
    void Start()
    {
		tscript = ruchka_item.GetComponent<Take>();
		plscr = player.GetComponent<FPS>();
		audio = GetComponent<AudioSource>();
        fscr = finish.GetComponent<Finish1>();
	}

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Insert_ruchka()
    {
        if(tscript.name_item == plscr.name_item_inHands)
        {
            ruchka_escape.SetActive(true);
            Destroy(ruchka_item);
            next_step.SetActive(true);
            fscr.step_for_finish += 1;
			Destroy(gameObject);
		}
    }
}
