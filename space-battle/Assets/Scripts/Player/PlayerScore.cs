using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class PlayerScore : MonoBehaviourPunCallbacks
{
    private int value = 0;
    private PlayerHUD playerHUD;
    private PhotonView photonView;
    private ExitGames.Client.Photon.Hashtable customProperties;
    [SerializeField] private int scoreToIncrease = 10;

    private void Awake()
    {
        playerHUD = GetComponent<PlayerHUD>();
        photonView = GetComponent<PhotonView>();
        value = 0;
        SetScore();
    }

    public void SetScore()
    {
        customProperties = new ExitGames.Client.Photon.Hashtable();
        customProperties.Add("Score", value);
        photonView.Owner.SetCustomProperties(customProperties);
    }

    public void AddScore()
    {
        value += scoreToIncrease;
        photonView.Owner.CustomProperties.Remove("Score");
        photonView.Owner.CustomProperties.Add("Score", value);
        playerHUD.ShowScore(value);
    }
}
