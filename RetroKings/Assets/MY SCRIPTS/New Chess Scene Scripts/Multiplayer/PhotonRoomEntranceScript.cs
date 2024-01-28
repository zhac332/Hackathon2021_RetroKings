using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonRoomEntranceScript : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject WaitingForOthersPanel;

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            WaitingForOthersPanel.SetActive(false);
            // both players need to be assigned colors to decide who starts

            if (PhotonNetwork.IsMasterClient)
            {
                DecideColors();
            }
        }
    }

    private void DecideColors()
    {
        // will send a PunRPC call to the GamePunControlsScript to assign the colors
    }

    public void ExitButton_OnClick()
    {
        PhotonNetwork.Disconnect();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        PhotonNetwork.Disconnect();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        PhotonNetwork.LoadLevel(0); // back to main menu
    }
}
