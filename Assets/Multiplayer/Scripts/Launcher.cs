using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;




public class Launcher : MonoBehaviourPunCallbacks // Наследуемся от MonoBehaviourPunCallbacks
{
    public static Launcher instance;

    [SerializeField] private TMP_InputField _roomInputField;
    [SerializeField] private TMP_Text _errorText;
    [SerializeField] private TMP_Text _roomNameText;
    [SerializeField] private Transform _roomList;
    [SerializeField] private GameObject _roomButtonPrefab;
    [SerializeField] private Transform _playerList;
    [SerializeField] private GameObject _playerNamePrefab;
    [SerializeField] private GameObject _startGameButton;

    private void Start()
    {
        instance = this;
        Debug.Log("Присоединяемся к Мастер серверу");
        Time.timeScale = 1.0f;
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log(Time.timeScale);
        MenuManager.instance.OpenMenu("loading");
    }


    // Метод, вызываемый при успешном подключении к мастер-серверу Photon
    public override void OnConnectedToMaster()
    {
        Debug.Log("Присоединились к Мастер серверу");
        PhotonNetwork.JoinLobby(); // Присоединяемся к лоббиъ
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    // Метод, вызываемый при успешном присоединении к лобби
    public override void OnJoinedLobby()
    {
        Debug.Log("Присоединились к лобби");
        MenuManager.instance.OpenMenu("title");
		int playerIndex = PhotonNetwork.LocalPlayer.ActorNumber; // Получаем индекс игрока
		string randomSuffix = UnityEngine.Random.Range(0, 1000).ToString("0000"); // Генерируем случайное число от 0 до 999
		PhotonNetwork.NickName = "Player" + playerIndex.ToString() + "_" + randomSuffix; // Формируем никнейм игрока
	}

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(3);
    }
    
    public void CreateRoom()
    {
        if(string.IsNullOrEmpty(_roomInputField.text))
        {
            return;
        }

        PhotonNetwork.CreateRoom(_roomInputField.text);
        MenuManager.instance.OpenMenu("loading");
    }
    
    public override void OnJoinedRoom()
    {
        _roomNameText.text = PhotonNetwork.CurrentRoom.Name;
        MenuManager.instance.OpenMenu("room");
        Player[] players = PhotonNetwork.PlayerList;

        for (int i = 0; i < _playerList.childCount; i++)
        {
            Destroy(_playerList.GetChild(i).gameObject);
        }

        for (int i = 0; i < players.Length; i++)
        {
            Instantiate(_playerNamePrefab, _playerList).GetComponent<PlayerListItem>().SetUp(players[i]);
        }
        _startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        
         _startGameButton.SetActive(PhotonNetwork.IsMasterClient);
        
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        _errorText.text = "Error:" + message;
        MenuManager.instance.OpenMenu("error");
    }

    
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        MenuManager.instance.OpenMenu("loading");
    }

    
    public override void OnLeftRoom()
    {
        MenuManager.instance.OpenMenu("title");
    }

    public  void JoinRoom (RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        MenuManager.instance.OpenMenu("loading");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        for (int i = 0; i < _roomList.childCount; i++) 
        {
            Destroy(_roomList.GetChild(i).gameObject);
        }
        for (int i = 0; i < roomList.Count; ++i)
        {
            if (roomList[i].RemovedFromList)
                continue;
            Instantiate(_roomButtonPrefab, _roomList).GetComponent<RoomListItem>().SetUp(roomList[i]);
        }
    }

    public override void OnPlayerEnteredRoom(Player player)
    {
        Instantiate(_playerNamePrefab, _playerList).GetComponent<PlayerListItem>().SetUp(player);
    }
}