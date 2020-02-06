using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class PlayerScore : MonoBehaviourPunCallbacks
{
    private PlayerHUD playerHUD;
    private PhotonView photonView;
    [SerializeField] private int scoreToIncrease = 1;
    public int Score { get; private set; }

    private void Awake()
    {
        playerHUD = GetComponent<PlayerHUD>();
        photonView = GetComponent<PhotonView>();
        Score = 0;
    }

    public void AddScore()
    {
        if(photonView.IsMine)
        {
            photonView.RPC("Add", RpcTarget.AllBuffered);
            playerHUD.ShowScore(Score);
        }
    }

    [PunRPC]
    private void Add()
    {
        Score += scoreToIncrease;
    }
}
