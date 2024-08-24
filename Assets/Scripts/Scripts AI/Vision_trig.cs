using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision_ : MonoBehaviour
{

	Transform target;
	Enemy_Script enemy;

	// Start is called before the first frame update
	void Start()
	{
		target = GameObject.Find("PlayerCamera").transform;
		enemy = transform.root.gameObject.GetComponent<Enemy_Script>();
	}

	void OnTriggerEnter(Collider other)
	{
		enemy.StartCoroutine("Ray_on_player_COR");
		enemy.player_on_trig_vision = true;
	}

	void OnTriggerExit(Collider other)
	{
		Debug.Log("ВЫШЕЛ ИЗ АТАКИ!");
		enemy.StopCoroutine("Ray_on_player_COR");
		enemy.player_on_trig_vision = false;
		if (enemy.sees_player_ID == 2) enemy.Go_to_player_last_point();
	}


}