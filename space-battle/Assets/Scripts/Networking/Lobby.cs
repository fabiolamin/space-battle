using UnityEngine;
using Photon.Pun;

public class Lobby : MonoBehaviourPunCallbacks
{
    [SerializeField] private UIPanelManager uiPanelManager;

    public void ConnectToLobby()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        uiPanelManager.EnableLobbyPanel();
        PhotonNetwork.JoinRandomRoom();
    }
}
