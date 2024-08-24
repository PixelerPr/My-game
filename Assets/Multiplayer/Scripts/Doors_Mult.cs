using Photon.Pun;
using System.Collections;
using UnityEngine;
public class Doors_Mult : MonoBehaviourPunCallbacks
{
	[SerializeField] private Animator anim;
	[SerializeField] private AudioSource audio;
	[SerializeField] private AudioClip openClip;
	[SerializeField] private AudioClip closeClip;
	[SerializeField] private AudioClip lockClip;
	public string openTrigger;
	public string closeTrigger;
	public bool locked = false;
	public bool firstAnimIncluded = true;
	private PhotonView photonView;

	private void Start()
	{
		photonView = GetComponent<PhotonView>();

		anim = GetComponent<Animator>();
		audio = GetComponent<AudioSource>();

	}

	public void Open_Close()
	{
		if (!locked)
		{
			if (firstAnimIncluded)
			{
				photonView.RPC("PlayOpenAnimation", RpcTarget.All);
				firstAnimIncluded = false;
			}
			else
			{
				photonView.RPC("PlayCloseAnimation", RpcTarget.All);
				firstAnimIncluded = true;
			}
		}
		else
		{
			audio.PlayOneShot(lockClip);
		}
	}

	[PunRPC]
	private void PlayOpenAnimation()
	{
		anim.Play(openTrigger);
		audio.PlayOneShot(openClip);
	}

	[PunRPC]
	private void PlayCloseAnimation()
	{
		anim.Play(closeTrigger);
		audio.PlayOneShot(closeClip);
	}
}
