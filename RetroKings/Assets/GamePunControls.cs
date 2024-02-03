using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GamePunControls : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject WaitingForOthersPanel;
    [SerializeField] private GameControlsScript GameControls;
    [SerializeField] private GamePUN GameP;
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
    public void SetColor(int white)
    {
        Debug.LogError((white == 0 ? "I got white" : "I got black"));
        GameP.SetMyTurn(white);
        GameControls.SetDisplay(white == 0);
    }

    private void AssignColors()
    {
        //int x = Random.Range(0, 2);
        int x = 0;

        if (x == 0)
        {
            // i get white, the opponent gets black
            pv.RPC("SetColor", RpcTarget.MasterClient, 0);
            pv.RPC("SetColor", RpcTarget.Others, 1);
        }
        else
        {
            // i get black, the opponent gets white
            pv.RPC("SetColor", RpcTarget.MasterClient, 1);
            pv.RPC("SetColor", RpcTarget.Others, 0);
        }
    }
}
