using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Secret_Shkaf_Mult : MonoBehaviourPunCallbacks
{
	public GameObject[] knigi;
	public GameObject shkaf;
	private GameObject player;
	public GameObject needBook1;
	public GameObject needBook2;
	public GameObject needBook3;
	int cntKnig = 0;
	Take_Mult b1scr;
	Take_Mult b2scr;
	Take_Mult b3scr;
	FPS_Mult plscr;
	AudioSource audio;
	Animator animator;

	public string anim;
	public AudioClip clip;
	private PhotonView photonView;

	private void AssignReferences()
	{
		player = GameManager_Mult.Instance.player;
		plscr = player.GetComponent<FPS_Mult>();
		b1scr = needBook1.GetComponent<Take_Mult>();
		b2scr = needBook2.GetComponent<Take_Mult>();
		b3scr = needBook3.GetComponent<Take_Mult>();
		animator = GetComponent<Animator>();
		audio = GetComponent<AudioSource>();
	}
	private IEnumerator WaitForPlayer()
	{
		while (GameManager_Mult.Instance.player == null)
		{
			yield return null;
		}
		AssignReferences();
	}

	// Start is called before the first frame update
	void Start()
	{
		photonView = GetComponent<PhotonView>();
		if (GameManager_Mult.Instance.player != null)
		{
			AssignReferences();
		}
		else
		{
			StartCoroutine(WaitForPlayer());
		}
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
		if (b1scr.name_item == plscr.name_item_inHands)
		{
			photonView.RPC("RPC_InsertBook", RpcTarget.All, b1scr.name_item);
		}
		else if (b2scr.name_item == plscr.name_item_inHands)
		{
			photonView.RPC("RPC_InsertBook", RpcTarget.All, b2scr.name_item);
		}
		else if (b3scr.name_item == plscr.name_item_inHands)
		{
			photonView.RPC("RPC_InsertBook", RpcTarget.All, b3scr.name_item);
		}
	}


	[PunRPC]
	private void RPC_InsertBook(string itemName)
	{
		if (b1scr.name_item == itemName)
		{
			Destroy(needBook1);
			plscr.name_item_inHands = null;
			plscr.handEmpty = true;
			knigi[0].SetActive(true);
			cntKnig++;
		}
		else if (b2scr.name_item == itemName)
		{
			Destroy(needBook2);
			plscr.name_item_inHands = null;
			plscr.handEmpty = true;
			knigi[1].SetActive(true);
			cntKnig++;
		}
		else if (b3scr.name_item == itemName)
		{
			Destroy(needBook3);
			plscr.name_item_inHands = null;
			plscr.handEmpty = true;
			knigi[2].SetActive(true);
			cntKnig++;
		}
	}
}
