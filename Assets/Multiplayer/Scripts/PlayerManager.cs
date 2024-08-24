using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;


public class PlayerManager : MonoBehaviour
{
    private PhotonView _photonView;
    // Start is called before the first frame update
    void Start()
    {
        _photonView = GetComponent<PhotonView>();
        if (_photonView.IsMine)
        {
            CreateController();
        }
    }

    
    private void CreateController()
    {
		int playerIndex = PhotonNetwork.LocalPlayer.ActorNumber;
		Vector3 spawnPosition = new Vector3(36, 2, -29); // ��������� �������� ������� ��������

		// ���������� ������� �������� ��� ������� ������ � ����������� �� ��� �������
		switch (playerIndex)
		{
			case 2:
				spawnPosition = new Vector3(39, 2, -26);
				break;
			case 3:
				spawnPosition = new Vector3(42, 2, -23);
				break;
			// �������� �������������� ������ ��� �������� ���������� �������, ���� ����������
			default:
				 // �� ���������, ����������� ��������� �������
				break;
		}

		Quaternion spawnRotation = Quaternion.identity; // ���������� ������ ��� �������

		GameObject player = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player_MP"), spawnPosition, spawnRotation);
		GameManager_Mult.Instance.SetPlayer(player); // Назначаем созданного игрока в GameManager
	}
}
