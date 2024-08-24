using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : MonoBehaviour
{
    public GameObject deadobj;
    public AudioSource audio;
    TriggerScript dead;

    // Start is called before the first frame update
    void Start()
    {
        dead= deadobj.GetComponent<TriggerScript>();
        audio = GetComponent<AudioSource>();
        Debug.Log("ТИП ПЕРЕМЕННОЙ:" + dead.GetType());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
        {
            audio.Play();
            dead.StartCoroutine("Death_Falling_COR");
        }
	}
}
