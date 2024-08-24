using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Waypoint_trig : MonoBehaviour
{
	public float move_delay_MIN = 3f;
	public float move_delay_MAX = 5f;
	public Enemy_Script _enemy;
	public string idle_anim_name = "Idle";

	public void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Enemy")
		{
			Debug.Log("���� ����� � ������� ���������");
			if (transform == _enemy.target)
			{
				Debug.Log("��������� ���� ��������� � ����������� ���������");
				_enemy = other.gameObject.GetComponent<Enemy_Script>();
				_enemy.Stop(idle_anim_name);
				_enemy.current_Waypoint_trig = this;
				StartCoroutine("Go_to_next_wp_COR");

			}
		}
	}

	IEnumerator Go_to_next_wp_COR()
	{
		yield return new WaitForSeconds(Random.Range(move_delay_MIN, move_delay_MAX));
		_enemy.Go_to_next_wp();
	}
}

