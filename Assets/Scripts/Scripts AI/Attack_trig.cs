using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TriggerScript : MonoBehaviour
{

	public GameObject falling;
    public GameObject en;
	public Transform player;
	public Transform spawnpoint;
	public Image deathscreen;
	public TMP_Text text_death;
	CharacterController controller;
	Enemy_Script enemy;
	public GameObject screamer;
	public AudioClip screamerAudio;
	AudioSource audio;

	NavMeshAgent agent;

	public int life = 1;

	private void Start()
	{
		audio= GetComponent<AudioSource>();
		StartCoroutine("Deathscreen_COR");
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			StartCoroutine("Death_COR");





		}
		if(other.tag == "Door")
		{
			if (other.GetComponent<Door>().firstAnimIncluded == true)
			{
				other.GetComponent<Door>().Open_Close();
			}
			
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Door")
		{
			if (other.GetComponent<Door>().firstAnimIncluded == false)
			{
				other.GetComponent<Door>().Open_Close();
			}

		}
	}

	IEnumerator Death_COR()
	{
		controller = player.GetComponent<CharacterController>();
		agent = en.gameObject.GetComponent<NavMeshAgent>();
		enemy = en.gameObject.GetComponent<Enemy_Script>();
		agent.isStopped = true;
		screamer.SetActive(true);
		audio.PlayOneShot(screamerAudio);
		controller.enabled = false;
		player.transform.position = spawnpoint.transform.position;
		controller.enabled = true;
		enemy.player_on_trig_vision = false;
		yield return new WaitForSeconds(2f);
		StartCoroutine("Deathscreen_COR");
		yield return new WaitForSeconds(1f);
		enemy.Go_to_next_wp();
		enemy.target = enemy.all_waypoints[0];


	}

	IEnumerator Death_Falling_COR()
	{
		controller = player.GetComponent<CharacterController>();
		agent = en.gameObject.GetComponent<NavMeshAgent>();
		enemy = en.gameObject.GetComponent<Enemy_Script>();
		agent.isStopped = true;
		falling.SetActive(true);
		yield return new WaitForSeconds(2f);
		controller.enabled = false;
		player.transform.position = spawnpoint.transform.position;
		controller.enabled = true;
		enemy.player_on_trig_vision = false;
		StartCoroutine("Deathscreen_COR");
		yield return new WaitForSeconds(1f);
		falling.SetActive(false);
		enemy.Go_to_next_wp();
		enemy.target = enemy.all_waypoints[0];
	}

	IEnumerator Deathscreen_COR()
	{
		deathscreen.GetComponent<Image>();
		text_death.GetComponent<TMP_Text>();
		Color color = deathscreen.color;
		Color color_text = text_death.color;
		if (life > 1 && life < 5)
		{

			while (color.a < 1.0f)
			{
				text_death.text = "День" + life + "\n" + "Попытка сбежать не удалась!";
				color.a += 1f * Time.deltaTime;
				color_text.a += 1f * Time.deltaTime;
				deathscreen.color = color;
				text_death.color = color_text;
				yield return null;
			}
			screamer.SetActive(false);
			Debug.Log("Затемнение завершено!");
			yield return new WaitForSeconds(2f);

			while (color.a > 0f)
			{
				color.a -= 1f * Time.deltaTime;
				color_text.a -= 1f * Time.deltaTime;
				deathscreen.color = color;
				text_death.color = color_text;
				yield return null;
			}
			life++;
		}

		if(life == 1)
		{
			Debug.Log("Жизнь 1");
			while (color.a < 1.0f)
			{
				color.a += 1f * Time.deltaTime;
				color_text.a += 1f * Time.deltaTime;
				deathscreen.color = color;
				text_death.color = color_text;
				yield return null;
			}
			screamer.SetActive(false);
			Debug.Log("Затемнение завершено!");
			yield return new WaitForSeconds(2f);

			while (color.a > 0f)
			{
				color.a -= 1f * Time.deltaTime;
				color_text.a -= 1f * Time.deltaTime;
				deathscreen.color = color;
				text_death.color = color_text;
				yield return null;
			}
			life++;
		}

		if (life > 4)
		{

			while (color.a < 1.0f)
			{
				text_death.text = "Игра окончена!" + "\n" + "Вам не удалось сбежать!";
				color.a += 1f * Time.deltaTime;
				color_text.a += 1f * Time.deltaTime;
				deathscreen.color = color;
				text_death.color = color_text;
				yield return null;
			}
			screamer.SetActive(false);
			Debug.Log("Затемнение завершено!");
			yield return new WaitForSeconds(2f);
			SceneManager.LoadScene(0);

		}
	}
}