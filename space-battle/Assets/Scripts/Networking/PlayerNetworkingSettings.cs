using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;

public class PlayerNetworkingSettings : MonoBehaviour
{
    private int index;
    [SerializeField] private InputField playerName;
    [SerializeField] private GameObject[] spaceships;
    [SerializeField] private Transform spawn;

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
        return playerName.text == "";
    }

    public void ChooseSpaceShip(int value)
    {
        index = value;
    }

    public void InstantiatePlayer()
    {
        PhotonNetwork.Instantiate(spaceships[index].name, spawn.position, Quaternion.identity);
    }
}
