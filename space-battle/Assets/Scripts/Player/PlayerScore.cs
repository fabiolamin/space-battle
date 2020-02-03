using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerScore : MonoBehaviour
{
    private int value = 0;
    private PlayerHUD playerHUD;
    private PhotonView photonView;
    private ExitGames.Client.Photon.Hashtable customProperties;
    [SerializeField] private int pointsToIncrease = 10;

    private void Awake()
    {
        playerHUD = GetComponent<PlayerHUD>();
        photonView = GetComponent<PhotonView>();
        value = 0;
        SetScore();
    }

    private void SetScore()
    {
        customProperties = new ExitGames.Client.Photon.Hashtable();
        customProperties.Add("Score", value);
        photonView.Owner.SetCustomProperties(customProperties);
    }

    public void AddPoints()
    {
        value += pointsToIncrease;
        photonView.Owner.CustomProperties.Remove("Score");
        photonView.Owner.CustomProperties.Add("Score", value);
        playerHUD.ShowScore(value);
    }
}
