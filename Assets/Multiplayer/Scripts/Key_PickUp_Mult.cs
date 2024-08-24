using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key_PickUp_Mult : MonoBehaviourPunCallbacks
{
	private GameObject player;
	private FPS_Mult plscr;
	public int number_key;
	private PhotonView photonView;

	private void AssignReferences()
	{
		player = GameManager_Mult.Instance.player;
		plscr = player.GetComponent<FPS_Mult>();
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

	public void Key_Pickup_mult()
	{
		plscr.keys[number_key] = true;

		if (PhotonNetwork.IsConnected)
		{
			photonView.RPC("KeyPickupRPC", RpcTarget.All);
		}
		else
		{
			KeyPickupRPC();
		}
	}

	[PunRPC]
	private void KeyPickupRPC()
	{
		Destroy(gameObject);
	}
}
