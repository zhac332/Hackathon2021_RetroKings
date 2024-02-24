using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Threading;
using UnityEngine;

public class PUNConnectionScript : MonoBehaviourPunCallbacks
{
    [SerializeField] private DisplayMessageScript MessagePanel; 
    [SerializeField] private GameObject LoadingPanel;
    [SerializeField] private GameObject HostPanel;
    [SerializeField] private GameObject PlayPanel;
    [Header("Maximum time to wait before timeout strike")]
    [SerializeField] private int Join_Timeout = 10;
    private bool timeout = false;

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
        if (PhotonNetwork.CountOfRooms == 0)
        {
            OnJoinRoomFailed(-1, "No rooms available.");
            return;
        }

        timeout = false;
        StartCoroutine(Timeout());
        PhotonNetwork.JoinRandomRoom();
    }

    private IEnumerator Timeout()
    {
        yield return new WaitForSeconds(Join_Timeout);
        timeout = true;
        MessagePanel.ShowMessage("Couldn't find any public games.");
        LoadingPanel.SetActive(false);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("joined room");
        PhotonNetwork.LoadLevel(2);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        LoadingPanel.SetActive(false);
        MessagePanel.ShowMessage("Cannot join game.\nReturn code: " + returnCode + ".\n" + message);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to server");
        MessagePanel.ShowMessage("Connected to server");
        HostPanel.SetActive(true);
        PlayPanel.SetActive(false);
        LoadingPanel.SetActive(false);
    }

    public void Disconnect()
    {
        PhotonNetwork.Disconnect();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        MessagePanel.ShowMessage("Disconnected from server. \n" + cause);
        HostPanel.SetActive(false);
        PlayPanel.SetActive(true);
    }
}
