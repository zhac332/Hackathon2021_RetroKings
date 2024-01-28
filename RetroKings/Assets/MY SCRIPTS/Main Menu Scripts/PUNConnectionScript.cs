using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine;

public class PUNConnectionScript : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject LoadingPanel;
    [SerializeField] private GameObject HostPanel;
    [SerializeField] private GameObject PlayPanel;

    public void GlobalButton_OnClick()
    {
        PhotonNetwork.ConnectUsingSettings();
        LoadingPanel.SetActive(true);
    }

    private string GenerateUniqueSuffix()
    {
        // You can use a timestamp or a random number as a suffix
        // In this example, I'm using a combination of timestamp and random number
        string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
        string randomSuffix = UnityEngine.Random.Range(1000, 9999).ToString();

        return timestamp + "_" + randomSuffix;
    }

    public void HostButton_OnClick()
    {
        string baseRoomName = "ChessRoom"; // You can change this to your desired base name
        string uniqueRoomName = baseRoomName + "_" + GenerateUniqueSuffix();

        LoadingPanel.SetActive(true);
        PhotonNetwork.CreateRoom(uniqueRoomName, new RoomOptions()
        {
            MaxPlayers = 2,
            CleanupCacheOnLeave = true
        });
    }

    public override void OnCreatedRoom()
    {
        PhotonNetwork.LoadLevel(2);
    }

    public void JoinButton_OnClick()
    {
        LoadingPanel.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("joined room");
        PhotonNetwork.LoadLevel(2);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to server");
        HostPanel.SetActive(true);
        PlayPanel.SetActive(false);
        LoadingPanel.SetActive(false);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        HostPanel.SetActive(false);
        PlayPanel.SetActive(true);
    }
}
