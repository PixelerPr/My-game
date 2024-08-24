using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Windows.WebCam;

public class FPS_Mult : MonoBehaviourPunCallbacks
{
	private GameObject DBCAM;
	[SerializeField] private float xRot; //������� �� X
	[SerializeField] private float yRot; //������� �� Y
	[SerializeField] public float speed_move; //��������
	[SerializeField] private float x_Move; //�������� �� X (����� ������)
	[SerializeField] private float z_Move; //�������� �� Z (����� �����)
	CharacterController player; // ������ ���������
	public Camera player_cam;
	[SerializeField] GameObject playerGameObject;
	Vector3 move_Direction; // ������ ��������
	[SerializeField] public float sensivity;
	[SerializeField] private bool canUseHeadBob = true;
	public bool handEmpty = true;
	public string name_item_inHands = null;
	public bool[] keys;
	private PhotonView _photonView;

	//��������� �����
	float xRotCurrent;
	float yRotCurrent;
	[SerializeField] private float smoothTime = 0.1f;
	float currentVelocityX;
	float currentVelocityY;

	//����� �����
	public AudioClip[] mas;
	[SerializeField] private AudioSource source_step;
	private AudioClip clip;
	[SerializeField] private float timer = 0.5f;
	[SerializeField] private bool stop;

	//HeadBob
	[Header("HeadBob")]
	[SerializeField] private float waklBobSpeed = 14f;
	[SerializeField] private float walkBobAmount = 0.05f;
	private float defalutYPos = 0;
	private float timerBob;

	public GameObject body;
	Animator body_animator;

	private void Awake()
	{
		defalutYPos = player_cam.transform.localPosition.y;
	}
	private void Start()
	{
		player = GetComponent<CharacterController>();
		_photonView = GetComponent<PhotonView>();
		source_step = GetComponent<AudioSource>();
		Cursor.lockState = CursorLockMode.Locked;
		handEmpty = true;
		if (!_photonView.IsMine)
		{
			DBCAM = (GetComponentInChildren<Camera>().gameObject);
			DBCAM.SetActive(false);
		}
		body_animator = body.GetComponent<Animator>();

	}
	// Update is called once per frame
	void Update()
	{
		if (!_photonView.IsMine)
			return;
		if (_photonView.IsMine)
		{
			Move();
			Mouse();
			if (x_Move != 0 || z_Move != 0)
			{
				SoundStep();
			}
			if (canUseHeadBob)
			{
				HandleHeadBob();
			}

		}
	}

	void Move()
	{
		x_Move = Input.GetAxis("Horizontal");
		z_Move = Input.GetAxis("Vertical");
		if (player.isGrounded)
		{
			move_Direction = new Vector3(x_Move, 0f, z_Move); //�������� �������
			move_Direction = transform.TransformDirection(move_Direction); //�������� ������� � ������� (��������� ��������)
			if (x_Move != 0 || z_Move != 0)
			{
				body_animator.SetTrigger("Walk");
			}
			if (move_Direction.x == 0f && move_Direction.y == 0f && move_Direction.z == 0f)
			{
				body_animator.SetTrigger("Idle");
			}
		}
		move_Direction.y -= 1;
		player.Move(move_Direction * Time.deltaTime * speed_move); // ���� ��������

	}
	void Mouse()
	{
		xRot += Input.GetAxis("Mouse X") * sensivity;
		yRot += Input.GetAxis("Mouse Y") * sensivity;
		yRot = Mathf.Clamp(yRot, -90, 90);

		xRotCurrent = Mathf.SmoothDamp(xRotCurrent, xRot, ref currentVelocityX, smoothTime);
		yRotCurrent = Mathf.SmoothDamp(yRotCurrent, yRot, ref currentVelocityY, smoothTime);

		player_cam.transform.rotation = Quaternion.Euler(-yRotCurrent, xRotCurrent, 0f);
		playerGameObject.transform.rotation = Quaternion.Euler(0f, xRotCurrent, 0f);
	}


	void SoundStep()
	{
		if (player.isGrounded)
		{
			clip = mas[Random.Range(0, mas.Length)];
		}
		if (!stop)
		{
			StartCoroutine(playSound());
		}
	}

	IEnumerator playSound()
	{
		stop = true;
		source_step.PlayOneShot(clip);
		yield return new WaitForSeconds(clip.length);
		stop = false;
	}

	void HandleHeadBob()
	{
		if (!player.isGrounded) return;
		if (Mathf.Abs(move_Direction.x) > 0.1f || Mathf.Abs(move_Direction.z) > 0.1f)
		{
			timerBob += Time.deltaTime * (waklBobSpeed);
			player_cam.transform.localPosition = new Vector3(player_cam.transform.localPosition.x, defalutYPos + Mathf.Sin(timerBob) * walkBobAmount,
				player_cam.transform.localPosition.z);

		}
	}
}
