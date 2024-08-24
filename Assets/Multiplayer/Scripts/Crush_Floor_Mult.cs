using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crush_Floor_Mult : MonoBehaviourPunCallbacks
{
	public GameObject[] floor;
	private AudioSource audio;

	private void Start()
	{
		audio = GetComponent<AudioSource>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!photonView.IsMine) return; // ���� �� ��������� �����, ������������

		if (other.CompareTag("Player"))
		{
			photonView.RPC("CrashFloor", RpcTarget.All);
		}
	}

	[PunRPC]
	private void CrashFloor()
	{
		StartCoroutine(CrashFloorCoroutine());
	}

	private IEnumerator CrashFloorCoroutine()
	{
		foreach (var item in floor)
		{
			item.GetComponent<Rigidbody>().isKinematic = false;
		}

		audio.Play();
		yield return new WaitForSeconds(2f);

		foreach (var item in floor)
		{
			PhotonNetwork.Destroy(item);
		}

		if (photonView.IsMine)
		{
			PhotonNetwork.Destroy(gameObject);
		}
	}
}
