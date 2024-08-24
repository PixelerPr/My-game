using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class New_Interact_Mult : MonoBehaviourPunCallbacks
{
	[SerializeField] private float _maxDistanceRay;
	[SerializeField] private Camera cam; //������ �� ������� ����� ���� ���
	[SerializeField] private GameObject notice;
	private Ray _ray;//��� ���
	private RaycastHit _hit;//������������ ����
	[SerializeField] private LayerMask mask;
	public bool greenKey = false;
	private PhotonView _photonView;

	private void Start()
	{
		_photonView = GetComponent<PhotonView>(); // Получаем PhotonView
		if (_photonView.IsMine)
		{
			// Ищем камеру и notice только для локального игрока

			notice = GameObject.Find("Подсказка"); // Найдем объект notice по имени
			if (notice != null)
			{
				Debug.Log("Notice found successfully");
			}
			else
			{
				Debug.LogError("Notice not found");
			}
		}
	}

	private void Update()
	{
		if (!_photonView.IsMine)
			return;
		Ray();
		DrawRay();
		InteractShkaf();
		InteractDoor();
		//InteractKey();
		Flash_Pickup();
		Note_interact();
		Interact_Take();
		InteractTumbochka();
		Interact_Items();
		Interact_Stul();
		Interact_Secret_Shkaf();
		Interact_Doski_Escape();
		Interact_Locker1();
		Insert_ruchka_Escape();
		Interact_Locker2();
		//Debug.Log("1:" +_hit.GetType() + "2:" + _hit.distance + "3:" + _hit.collider);
	}

	private void Ray()
	{
		//���� �������� �� �������� ��� �� ������ ������ ���� ������ � ������ �� ��� ��� ����������� ����
		_ray = cam.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
	}
	private void DrawRay()// ��������� ���� ��� ������ � ��������
	{
		if (Physics.Raycast(_ray, out _hit, _maxDistanceRay, mask))//�������� ������������ �� ��� � ��� ��
		{
			Debug.DrawRay(_ray.origin, _ray.direction * _maxDistanceRay, Color.blue);
			notice.SetActive(false);
		}
		if (_hit.transform == null)
		{
			Debug.DrawRay(_ray.origin, _ray.direction * _maxDistanceRay, Color.red);
			notice.SetActive(false);
		}
	}

	private void InteractShkaf()
	{
		if (_hit.transform != null && _hit.transform.GetComponent<Shkaf>())
		{
			Debug.DrawRay(_ray.origin, _ray.direction * _maxDistanceRay, Color.green);
			notice.SetActive(true);
			if (Input.GetKeyDown(KeyCode.E))
			{
				_hit.transform.GetComponent<Shkaf>().Open_Close();
			}
		}

	}

	private void InteractTumbochka()
	{
		if (_hit.transform != null && _hit.transform.GetComponent<Tumbochka>())
		{
			Debug.DrawRay(_ray.origin, _ray.direction * _maxDistanceRay, Color.green);
			notice.SetActive(true);
			if (Input.GetKeyDown(KeyCode.E))
			{
				_hit.transform.GetComponent<Tumbochka>().Open_Close();
			}
		}

	}


	private void InteractDoor()
	{
		if (_hit.transform != null && _hit.transform.GetComponent<Doors_Mult>())
		{
			Debug.DrawRay(_ray.origin, _ray.direction * _maxDistanceRay, Color.green);
			notice.SetActive(true);
			if (Input.GetKeyDown(KeyCode.E))
			{
				_hit.transform.GetComponent<Doors_Mult>().Open_Close();
			}
		}

	}
	//private void InteractKey()
	//{
	//	if (_hit.transform != null && _hit.transform.GetComponent<Key_pickUp_Mult>())
	//	{
	//		Debug.DrawRay(_ray.origin, _ray.direction * _maxDistanceRay, Color.yellow);
	//		notice.SetActive(true);
	//		if (Input.GetKeyDown(KeyCode.E))
	//		{
	//			_hit.transform.GetComponent<Key_pickUp_Mult>().Key_Pickup_mult();
	//		}
	//	}

	//}
	private void Flash_Pickup()
	{
		if (_hit.transform != null && _hit.transform.GetComponent<Flashlight_Pick>())
		{
			Debug.DrawRay(_ray.origin, _ray.direction * _maxDistanceRay, Color.yellow);
			notice.SetActive(true);
			if (Input.GetKeyDown(KeyCode.E))
			{
				_hit.transform.GetComponent<Flashlight_Pick>().Flash_Pick();
			}
		}

	}
	private void Note_interact()
	{
		if (_hit.transform != null && _hit.transform.GetComponent<Note>())
		{
			Debug.DrawRay(_ray.origin, _ray.direction * _maxDistanceRay, Color.green);
			notice.SetActive(true);
			if (Input.GetKeyDown(KeyCode.E))
			{
				_hit.transform.GetComponent<Note>().Nnote();
			}
		}


	}
	private void Interact_Take()
	{
		if (_hit.transform != null && _hit.transform.GetComponent<Take_Mult>())
		{
			Debug.DrawRay(_ray.origin, _ray.direction * _maxDistanceRay, Color.yellow);
			notice.SetActive(true);
			if (Input.GetKeyDown(KeyCode.E))
			{
				Debug.Log("NAZHAL E");
				_hit.transform.GetComponent<Take_Mult>().Pick_up();
			}
		}
	}

	private void Interact_Items()
	{
		if (_hit.transform != null && _hit.transform.GetComponent<Bridge_Mult>())
		{
			Debug.DrawRay(_ray.origin, _ray.direction * _maxDistanceRay, Color.yellow);
			notice.SetActive(true);
			if (Input.GetKeyDown(KeyCode.E))
			{
				_hit.transform.GetComponent<Bridge_Mult>().Use_Bridge();
			}
		}
	}

	private void Interact_Stul()
	{
		if (_hit.transform != null && _hit.transform.GetComponent<Stul_Lock>())
		{
			Debug.DrawRay(_ray.origin, _ray.direction * _maxDistanceRay, Color.yellow);
			notice.SetActive(true);
			if (Input.GetKeyDown(KeyCode.E))
			{
				_hit.transform.GetComponent<Stul_Lock>().Stul_Open();
			}
		}
	}

	private void Interact_Secret_Shkaf()
	{
		if (_hit.transform != null && _hit.transform.GetComponent<Secret_Shkaf_Mult>())
		{
			Debug.DrawRay(_ray.origin, _ray.direction * _maxDistanceRay, Color.yellow);
			notice.SetActive(true);
			if (Input.GetKeyDown(KeyCode.E))
			{
				_hit.transform.GetComponent<Secret_Shkaf_Mult>().Insert_Book();
			}
		}
	}

	private void Interact_Doski_Escape()
	{
		if (_hit.transform != null && _hit.transform.GetComponent<Doski_Escape_Mult>())
		{
			Debug.DrawRay(_ray.origin, _ray.direction * _maxDistanceRay, Color.yellow);
			notice.SetActive(true);
			if (Input.GetKeyDown(KeyCode.E))
			{
				_hit.transform.GetComponent<Doski_Escape_Mult>().Use_Lom();
			}
		}
	}

	private void Interact_Locker1()
	{
		if (_hit.transform != null && _hit.transform.GetComponent<Locker1_Escape_Mult>())
		{
			Debug.DrawRay(_ray.origin, _ray.direction * _maxDistanceRay, Color.yellow);
			notice.SetActive(true);
			if (Input.GetKeyDown(KeyCode.E))
			{
				_hit.transform.GetComponent<Locker1_Escape_Mult>().Open_Locker_Escape();
			}
		}
	}

	private void Insert_ruchka_Escape()
	{
		if (_hit.transform != null && _hit.transform.GetComponent<Ruchka_Mult>())
		{
			Debug.DrawRay(_ray.origin, _ray.direction * _maxDistanceRay, Color.yellow);
			notice.SetActive(true);
			if (Input.GetKeyDown(KeyCode.E))
			{
				_hit.transform.GetComponent<Ruchka_Mult>().Insert_ruchka();
			}
		}
	}
	private void Interact_Locker2()
	{
		if (_hit.transform != null && _hit.transform.GetComponent<Locker2_Mult>())
		{
			Debug.DrawRay(_ray.origin, _ray.direction * _maxDistanceRay, Color.yellow);
			notice.SetActive(true);
			if (Input.GetKeyDown(KeyCode.E))
			{
				_hit.transform.GetComponent<Locker2_Mult>().Locker2();
			}
		}
	}

	private class Key_pickUp_Mult
	{
	}
}
