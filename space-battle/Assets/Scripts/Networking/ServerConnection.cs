using UnityEngine;
using Photon.Pun;

public class ServerConnection : MonoBehaviourPunCallbacks
{
    private PlayerNetworkingSettings playerNetworkingSettings;
    [SerializeField] private UIPanelManager uiPanelManager;

    private void Awake()
    {
        playerNetworkingSettings = GetComponent<PlayerNetworkingSettings>();
    }

    public void ConnectToServer()
    {
        playerNetworkingSettings.SetNickName();
        uiPanelManager.EnableLobbyPanel();
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        uiPanelManager.ConnectionStatus.text = "Finding a match...";
    }
}
