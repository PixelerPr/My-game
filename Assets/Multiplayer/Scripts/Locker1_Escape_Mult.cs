using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locker1_Escape_Mult : MonoBehaviourPunCallbacks
{
	private GameObject player;
	private FPS plscr;
	public GameObject locker;
	public GameObject balka;
	public GameObject finish;
	private Finish1 fscr;
	public int need_key;
	private Animator animator;
	private AudioSource audio;
	public string anim;
	private PhotonView photonView;

	private void AssignReferences()
	{
		player = GameManager_Mult.Instance.player;
		plscr = player.GetComponent<FPS>();
		animator = balka.GetComponent<Animator>();
		fscr = finish.GetComponent<Finish1>();
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

	public void Open_Locker_Escape()
	{
		if (plscr.keys[need_key] == true)
		{
			if (PhotonNetwork.IsConnected)
			{
				photonView.RPC("OpenLockerEscapeRPC", RpcTarget.All);
			}
			else
			{
				OpenLockerEscapeRPC();
			}
		}
	}

	[PunRPC]
	private void OpenLockerEscapeRPC()
	{
		StartCoroutine(OpenLockerEscapeCoroutine());
	}

	private IEnumerator OpenLockerEscapeCoroutine()
	{
		fscr.step_for_finish += 1;
		animator.Play(anim);
		yield return new WaitForSeconds(2);
		Destroy(balka.gameObject);
		Destroy(locker);
	}
}
