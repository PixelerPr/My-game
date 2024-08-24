using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stul_Lock : MonoBehaviour
{
    public GameObject stul;
    Animator animator;
    AudioSource audio;
    public GameObject door;
    Door doorscr;
    public string anim;
    public AudioClip clip;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        doorscr = door.GetComponent<Door>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Stul_Open()
    {
        animator.Play(anim);
        doorscr.locked = false;
        audio.PlayOneShot(clip);
        Destroy(stul.GetComponent<Stul_Lock>());
    }
}
