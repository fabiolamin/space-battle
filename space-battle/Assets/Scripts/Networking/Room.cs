using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class Room : MonoBehaviourPunCallbacks, IOnEventCallback
{
    private Timer timer;
    private RaiseEventManager raiseEventManager;
    private PlayerNetworkingSettings playerNetworkingSettings;
    [SerializeField] private UIPanelManager uiPanelManager;
    [SerializeField] private byte numberOfPlayers = 2;
    [SerializeField] private AsteroidSpawn asteroidSpawn;

    private void Awake()
    {
        timer = GetComponent<Timer>();
        raiseEventManager = GetComponent<RaiseEventManager>();
        playerNetworkingSettings = GetComponent<PlayerNetworkingSettings>();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        CreateRoom();
    }

    private void CreateRoom()
    {
        string name = "Room " + Random.Range(100, 999);
        RoomOptions options = new RoomOptions()
        {
            MaxPlayers = numberOfPlayers
        };

        PhotonNetwork.CreateRoom(name, options);
    }

    public override void OnJoinedRoom()
    {
        playerNetworkingSettings.InstantiatePlayer();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == numberOfPlayers)
        {
            raiseEventManager.Create(0, "Room Panel");
            timer.SetTimer();
            asteroidSpawn.InstantiateAsteroids();
        }
    }

    public void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code == 0 && photonEvent.CustomData.ToString() == "Room Panel")
        {
            uiPanelManager.EnableRoomPanel();
        }
    }
}
