using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locker2_Escape : MonoBehaviour
{
	public GameObject player;
	FPS plscr;
	public GameObject finish;
	Finish1 fscr;
	public int need_key;
	AudioSource audio;

	// Start is called before the first frame update
	void Start()
    {
        plscr = player.GetComponent<FPS>();
		fscr = finish.GetComponent<Finish1>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Locker2()
    {
		if (plscr.keys[need_key] == true)
		{
			fscr.step_for_finish += 1;
			Destroy(gameObject);
			
		}
	}
}
