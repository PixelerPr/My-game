using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key_pickUp : MonoBehaviour
{
    public GameObject player;
    FPS plscr;
    public int number_key;

    // Start is called before the first frame update
    void Start()
    {
        plscr = player.GetComponent<FPS>();

    }


    public void Key_Pickup()
    {
        plscr.keys[number_key] = true;
        Destroy(gameObject);
    }

}
