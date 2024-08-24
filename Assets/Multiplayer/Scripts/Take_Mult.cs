using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Take_Mult : MonoBehaviourPunCallbacks
{
	private GameObject RUKA;
	private FPS_Mult nf;
	private GameObject player;

	public string name_item;
	[SerializeField] private LayerMask mask;

	private void Start()
	{
		if (GameManager_Mult.Instance.player != null)
		{
			AssignReferences();
		}
		else
		{
			StartCoroutine(WaitForPlayer());
		}
	}

	private IEnumerator WaitForPlayer()
	{
		while (GameManager_Mult.Instance.player == null)
		{
			yield return null;
		}
		AssignReferences();
	}

	private void AssignReferences()
	{
		player = GameManager_Mult.Instance.player;
		RUKA = GameManager_Mult.Instance.hand;
		nf = player.GetComponent<FPS_Mult>();


		// Проверим назначены ли ссылки
		if (player == null)
			Debug.LogError("Player is not assigned in GameManager!");
		if (RUKA == null)
			Debug.LogError("Hand is not assigned in GameManager!");
		if (nf == null)
			Debug.LogError("FPS component is not found on the player!");

	}

	public void Pick_up()
	{
		if (nf.handEmpty)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, mask))
			{
				Debug.DrawRay(ray.origin, ray.direction * 2f, Color.yellow);
				Debug.Log("1:" + hit.GetType() + "2:" + hit.distance + "3:" + hit.collider);
				Debug.Log("MASSSSSK");

				photonView.RPC("HandlePickUp", RpcTarget.All, hit.collider.gameObject.GetComponent<PhotonView>().ViewID);
			}
		}
	}

	[PunRPC]
	private void HandlePickUp(int viewID)
	{
		PhotonView itemPhotonView = PhotonView.Find(viewID);
		if (itemPhotonView != null)
		{
			GameObject item = itemPhotonView.gameObject;
			item.transform.parent = RUKA.transform;
			Rigidbody rb = item.GetComponent<Rigidbody>();
			BoxCollider collider = item.GetComponent<BoxCollider>();

			rb.isKinematic = true;
			rb.useGravity = false;
			collider.enabled = false;
			item.transform.position = RUKA.GetComponent<Transform>().position;
			item.transform.rotation = RUKA.GetComponent<Transform>().rotation;

			if (item == gameObject)
			{
				nf.handEmpty = false;
				nf.name_item_inHands = name_item;
			}
		}
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.G))
		{
			if (RUKA.transform.childCount > 0)
			{
				Rigidbody itemRigidbody = RUKA.transform.GetChild(0).GetComponent<Rigidbody>();
				BoxCollider itemCollider = RUKA.transform.GetChild(0).GetComponent<BoxCollider>();
				itemRigidbody.isKinematic = false;
				itemRigidbody.useGravity = true;
				itemCollider.enabled = true;
				nf.handEmpty = true;
				nf.name_item_inHands = null;
				RUKA.transform.GetChild(0).parent = null;
			}
		}
	}
}
