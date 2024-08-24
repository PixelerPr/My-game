using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ruchka_Mult : MonoBehaviourPunCallbacks
{
	private GameObject player;
	private FPS_Mult plscr;
	public GameObject ruchka_item;
	private Take_Mult tscript;
	public GameObject finish;
	Finish1 fscr;
	public GameObject ruchka_escape;
	public GameObject next_step;
	AudioSource audio;
	private PhotonView photonView;


	private void AssignReferences()
	{
		tscript = ruchka_item.GetComponent<Take_Mult>();
		plscr = player.GetComponent<FPS_Mult>();
		audio = GetComponent<AudioSource>();
		fscr = finish.GetComponent<Finish1>();
	}
	private IEnumerator WaitForPlayer()
	{
		while(GameManager_Mult.Instance.player == null)
		{
			yield return null;
		}
		AssignReferences();
	}
	// Start is called before the first frame update
	void Start()
	{
		photonView= GetComponent<PhotonView>();
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

	}

	public void Insert_ruchka()
	{
		if(player != null && tscript != null && plscr != null)
		{
			if(tscript.name_item == plscr.name_item_inHands)
			{
				photonView.RPC("Insert_ruchka_Active", RpcTarget.All, tscript.name_item);
			}
		}
	}

	[PunRPC]
	public void Insert_ruchka_Active()
	{
		if (tscript.name_item == plscr.name_item_inHands)
		{
			ruchka_escape.SetActive(true);
			Destroy(ruchka_item);
			next_step.SetActive(true);
			fscr.step_for_finish += 1;
			Destroy(gameObject);
		}
	}
}
