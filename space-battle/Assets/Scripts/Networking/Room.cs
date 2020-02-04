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

    public int NumberOfPlayers
    {
        get { return numberOfPlayers; }
        private set { numberOfPlayers = (byte)value; }
    }

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
            SetRoom();
        }
    }

    private void SetRoom()
    {
        timer.SetTimer();
        asteroidSpawn.InstantiateAsteroids();
        if (PhotonNetwork.IsMasterClient)
        {
            raiseEventManager.Create(0, "Room Panel");
        }
    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        if (propertiesThatChanged.ContainsKey("Timer"))
        {
            timer.CurrentTime = (float)propertiesThatChanged["Timer"];
            timer.SetText();

            if (timer.CurrentTime <= 0 && PhotonNetwork.IsMasterClient)
            {
                raiseEventManager.Create(1, "Scoreboard Panel");
            }
        }
    }

    public void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code == 0)
        {
            uiPanelManager.EnableRoomPanel();
        }

        else if (photonEvent.Code == 1)
        {
            uiPanelManager.EnableScoreboardPanel();
        }
    }

    public void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }
}
