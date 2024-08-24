using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Mult : MonoBehaviourPunCallbacks
{
	public static GameManager_Mult Instance;

	public GameObject player;
	public GameObject hand;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public void SetPlayer(GameObject newPlayer)
	{
		player = newPlayer;
		// Предполагаем, что камера имеет тег "MainCamera" и рука - дочерний объект камеры с именем "Hand"
		hand = newPlayer.transform.Find("Camera/RUKA").gameObject;
	}
}
