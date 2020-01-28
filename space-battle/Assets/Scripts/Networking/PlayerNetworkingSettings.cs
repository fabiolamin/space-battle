using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;

public class PlayerNetworkingSettings : MonoBehaviour
{
    private int index;
    [SerializeField] private InputField playerName;

    public void ChooseSpaceShip(int value)
    {
        index = value;
    }

    public void SetNickName()
    {
        if (IsInputFieldEmpty())
        {
            PhotonNetwork.NickName = "Player " + Random.Range(100, 999);
        }

        else
        {
            PhotonNetwork.NickName = playerName.text;
        }
    }

    private bool IsInputFieldEmpty()
    {
        return playerName == null;
    }
}
