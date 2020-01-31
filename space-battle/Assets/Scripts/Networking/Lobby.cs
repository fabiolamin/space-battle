using UnityEngine;
using Photon.Pun;

public class Lobby : MonoBehaviourPunCallbacks
{
    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinRandomRoom();
    }
}
