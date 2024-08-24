using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Script : MonoBehaviour
{
	public Transform[] all_waypoints;// ������ �����
	public Transform vision;// ����� ������ ��������� ��� ��� �������� �����������
	public LayerMask sees_ray_layer_mask;// ���� ������ � �����,���� �� ���� ��������� ������������ ����
	public Transform player_last_point_trig;// ��������� ����� ��� ������ ������
	public GameObject player;// ������ �� ������
	[HideInInspector] public bool player_on_trig_vision;// ���� ��� ����� � ���� ������
	[HideInInspector] public int sees_player_ID;//0 - �� �����, 1 - ��������� ����� ������, 2 - ��� � ������
	[HideInInspector] public Transform target;// ������� ���� �����
	bool look_on_player;
	Transform player_camera;
	Transform my_tranform;
	NavMeshAgent agent;
	Animator enemy_animator;
	[HideInInspector] public Waypoint_trig current_Waypoint_trig;//��� ��������� ��������

	//����� �����//UPD2
	public AudioClip[] mas;//UPD2
	[SerializeField] private AudioSource source_step;//UPD2
	private AudioClip clip;//UPD2
	public bool localStop;//UPD2

	//UPD3
	public AudioSource chasingSource;
	private bool chasing_Audio = false;
	public AudioClip[] chases;
	


	//UPD1 - ���������� ����������� ���� � �����
	//UPD2 - ���������� ������ ������ ����

	// Start is called before the first frame update
	void Start()
	{
		my_tranform = transform;
		player = GameObject.FindWithTag("Player");
		player_camera = GameObject.FindWithTag("MainCamera").transform;
		enemy_animator = GetComponent<Animator>();
		agent = GetComponent<NavMeshAgent>();
		//target = all_waypoints[0];//����� ���������� ��������� ���� �����
		Go_to_next_wp();
		StartCoroutine("Set_destination_COR");
		
	}

	IEnumerator Set_destination_COR()
	{
		while (true)
		{
			yield return new WaitForSeconds(0.1f);
			agent.SetDestination(target.position);
		}
	}

	IEnumerator Ray_on_player_COR()//����� ��������� � ������������� ��� ����� � ������ �� ������� ������
	{
		while (true)
		{
			yield return new WaitForSeconds(0.01f);
			Debug.DrawLine(vision.position, player_camera.position, Color.blue);
			if (player_on_trig_vision)
			{
				if (!Physics.Linecast(vision.position, player_camera.position, out RaycastHit hitinfo, sees_ray_layer_mask))
				{
					//Debug.Log(hitinfo);
					Debug.DrawLine(vision.position, player_camera.position, Color.red);
					if (sees_player_ID < 2) Sees_player();
				}
				else if (sees_player_ID == 2) Go_to_player_last_point();
			}
		}
	}


	void Update()// ��������� �� ���� ����� ��������������
	{
		if(agent.isStopped == false)//UPD2
		{
			SoundStep();
		}
		ChasingAudio();
		if (look_on_player)
		{
			my_tranform.LookAt(player_camera.transform);
			my_tranform.eulerAngles = new Vector3(0, my_tranform.eulerAngles.y, 0);
		}
	}


	public void Sees_player()
	{
		look_on_player = true;// ��� Update
		if (current_Waypoint_trig) current_Waypoint_trig.StopCoroutine("Go_to_next_WP_COR");
		target = player.transform;
		sees_player_ID = 2;
		agent.speed = 3;//UPD1

		//Move();
		Chasing();//UPD1
		Debug.Log("����� �������");

	}


	public void Go_to_player_last_point()
	{
		player_last_point_trig.position = player.transform.position;
		sees_player_ID = 2;
		target = player_last_point_trig;
		agent.speed = 3;//UPD1
		look_on_player = false;
						//Move();
		Chasing();//UPD1
		//Debug.Log("��� � ��������� ����� ������");
	}

	public void Go_to_next_wp()
	{
		List<Transform> available_wps_LIST = new List<Transform>();
		foreach (Transform wp in all_waypoints)
		{
			if (wp.GetComponent<Waypoint_trig>() != current_Waypoint_trig) available_wps_LIST.Add(wp.transform);
		}
		target = available_wps_LIST[Random.Range(0, available_wps_LIST.Count)];
		sees_player_ID = 0;
		agent.speed = 2;
		Move();
		//Debug.Log("��������� ��������:" + target);
		//Debug.Log("��������� ��������:" + target.position);

	}

	public void Stop(string anim_name)
	{
		sees_player_ID = 0;
		agent.isStopped = true;
		enemy_animator.SetTrigger(anim_name);
	}

	void Move()
	{
		if (!enemy_animator.GetCurrentAnimatorStateInfo(0).IsName("Walk")) enemy_animator.SetTrigger("Walk");
		agent.isStopped = false;
		//ChasingAudio();
	}

	void Chasing()//UPD1
	{
		if (!enemy_animator.GetCurrentAnimatorStateInfo(0).IsName("Chasing")) enemy_animator.SetTrigger("Chasing");
		agent.isStopped = false;
		//ChasingAudio();
	}
	void SoundStep()//UPD2
	{
		if (true)
		{
			clip = mas[Random.Range(0, mas.Length)];
		}
		if (!localStop)
		{
			StartCoroutine(playSound());
		}
	}
	IEnumerator playSound()//UPD2
	{
		if (enemy_animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
		{
			localStop = true;
			source_step.PlayOneShot(clip);
			//Debug.Log("XXXXXXXXX");
			yield return new WaitForSeconds(clip.length);
			localStop = false;
		}
		if (enemy_animator.GetCurrentAnimatorStateInfo(0).IsName("Chasing"))
		{
			localStop = true;
			source_step.PlayOneShot(clip);
			//Debug.Log("XXXXXXXXX");
			yield return new WaitForSeconds(clip.length / 2);
			localStop = false;
		}
		
	}
	void ChasingAudio()
	{
		if(chasing_Audio == false && sees_player_ID > 0)
		{
			chasing_Audio = true;
			chasingSource.Stop();
			chasingSource.clip = chases[1];
			chasingSource.Play();
		}
		if(chasing_Audio == true && sees_player_ID < 1) 
		{
			chasing_Audio = false;
			chasingSource.Stop();
			chasingSource.clip = chases[0];
			chasingSource.Play();
		}
	}

}
