using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge_Mult : MonoBehaviourPunCallbacks
{
	private GameObject player;
	public GameObject bridge;
	public GameObject item;
	private Take_Mult tScript;
	private FPS_Mult pScript;
	private PhotonView photonView;

	private void AssignReferences()
	{
		player = GameManager_Mult.Instance.player;
		tScript = item.GetComponent<Take_Mult>();
		pScript = player.GetComponent<FPS_Mult>();
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


	public void Use_Bridge()
	{
		if (player != null && tScript != null && pScript != null)
		{
			if (tScript.name_item == pScript.name_item_inHands)
			{
				photonView.RPC("BridgeActivation", RpcTarget.All, tScript.name_item);
			}
		}
		else
		{
			Debug.LogError("One of the references (player, tScript, or pScript) is null.");
		}
	}


	[PunRPC]
	private void BridgeActivation(string itemName)
	{
		if (tScript.name_item == itemName)
		{
			Destroy(item);
			pScript.name_item_inHands = null;
			pScript.handEmpty = true;
			bridge.gameObject.SetActive(true);

		}

	}
}
