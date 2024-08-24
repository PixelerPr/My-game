using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
	public GameObject player;
	FPS pscr;
	public GameObject bridge;
	public GameObject item;
	Take tscript;


	// Start is called before the first frame update
	void Start()
	{
		tscript = item.GetComponent<Take>();
		pscr = player.GetComponent<FPS>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void Use_Bridge()
	{

		if (tscript.name_item == pscr.name_item_inHands)
		{
			Destroy(item);
			pscr.name_item_inHands = null;
			pscr.handEmpty = true;
			bridge.gameObject.SetActive(true);
			Debug.Log("Получилось");
		}
		else
		{
			Debug.Log("Без предмета");
		}
	}
}
