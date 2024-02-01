using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GamePunControls : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject WaitingForOthersPanel;
    private PhotonView pv;

    private void Start()
    {
        pv = GetComponent<PhotonView>();
        Setup();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Setup();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        PhotonNetwork.Disconnect();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        PhotonNetwork.LoadLevel(0);
    }

    private void Setup()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            WaitingForOthersPanel.SetActive(false);
            // master player will set up the assignment of colors

            if (PhotonNetwork.IsMasterClient)
            {
                AssignColors();
            }
        }
    }

    [PunRPC]
    public void SetColor(bool white)
    {
        Debug.LogError((white ? "I got white" : "I got black"));
    }

    private void AssignColors()
    {
        int x = Random.Range(0, 2);

        if (x == 0)
        {
            // i get white, the opponent gets black
            pv.RPC("SetColor", RpcTarget.MasterClient, true);
            pv.RPC("SetColor", RpcTarget.Others, false);
        }
        else
        {
            // i get black, the opponent gets white
            pv.RPC("SetColor", RpcTarget.MasterClient, false);
            pv.RPC("SetColor", RpcTarget.Others, true);
        }
    }
}
