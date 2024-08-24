using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locker2_Mult : MonoBehaviourPunCallbacks
{
	public GameObject player;
	private FPS_Mult plscr;
	public GameObject finish;
	Finish1 fscr;
	public int need_key;
	AudioSource audio;

	private void AssignReferences()
	{
		player = GameManager_Mult.Instance.player;
		plscr = player.GetComponent<FPS_Mult>();
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
		plscr = player.GetComponent<FPS_Mult>();
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
			if (PhotonNetwork.IsConnected)
			{
				photonView.RPC("Locker2_Active", RpcTarget.All);
			}
			else
			{
				Locker2_Active();
			}
		}
	}

	[PunRPC]
	public void Locker2_Active()
	{
		if (plscr.keys[need_key] == true)
		{
			fscr.step_for_finish += 1;
			Destroy(gameObject);

		}
	}
}
