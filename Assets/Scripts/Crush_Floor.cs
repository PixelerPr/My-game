using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crush_Floor : MonoBehaviour
{
    public GameObject[] floor;
    AudioSource audio;


    // Start is called before the first frame update
    void Start()
    {
        audio= GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter(Collider other)
	{

        Debug.Log("FLAG");
	    if (other.tag == "Player")
		{
			Debug.Log("FLAG2");
			StartCoroutine("Crash_Floor_COR");
		}

		
	}

    IEnumerator Crash_Floor_COR()
    {
		foreach (var item in floor)
		{
			item.gameObject.GetComponent<Rigidbody>().isKinematic = false;
		}
        audio.Play();
        yield return new WaitForSeconds(2);
        foreach(var item in floor)
        {
            Destroy(item.gameObject);
			Destroy(gameObject.GetComponent<Crush_Floor>());
		}
	}
}
