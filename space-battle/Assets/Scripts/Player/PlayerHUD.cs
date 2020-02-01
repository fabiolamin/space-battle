using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    private PhotonView photonView;
    [SerializeField] private Text nickname;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();

        if (photonView.IsMine)
        {
            nickname.color = Color.green;
            photonView.RPC("ShowNickname", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    private void ShowNickname()
    {
        nickname.text = photonView.Owner.NickName;
    }
}
