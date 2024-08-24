using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doski_Escape_Mult : MonoBehaviourPunCallbacks
{
	private GameObject player;
	private FPS_Mult pscr;
	public GameObject doska;
	public GameObject lom;
	private Take_Mult tscript;
	public GameObject finish;
	private Finish1 fscr;
	private AudioSource audio;
	private Animator animator;
	public string anim;
	private PhotonView photonView;

	private void AssignReferences()
	{
		player = GameManager_Mult.Instance.player;
		tscript = lom.GetComponent<Take_Mult>();
		pscr = player.GetComponent<FPS_Mult>();
		fscr = finish.GetComponent<Finish1>();
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
	}

	public void Use_Lom()
	{
		if (tscript.name_item == pscr.name_item_inHands)
		{
			if (PhotonNetwork.IsConnected)
			{
				photonView.RPC("UseLomRPC", RpcTarget.All);
			}
			else
			{
				UseLomRPC();
			}
		}
	}

	[PunRPC]
	private void UseLomRPC()
	{
		StartCoroutine(UseLomCoroutine());
	}

	private IEnumerator UseLomCoroutine()
	{
		fscr.step_for_finish += 1;
		animator.Play(anim);
		yield return new WaitForSeconds(4);
		Destroy(doska);
	}
}
