using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    private PhotonView photonView;
    [SerializeField] private Text nickname;
    [SerializeField] private Text score;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();

        if (photonView.IsMine)
        {
            nickname.color = Color.green;
            score.color = Color.green;
            photonView.RPC("ShowNicknameRPC", RpcTarget.AllBuffered);
        }
    }

    private void Update()
    {
        LockTextRotation();
    }

    [PunRPC]
    private void ShowNicknameRPC()
    {
        nickname.text = photonView.Owner.NickName;
    }

    private void LockTextRotation()
    {
        nickname.transform.rotation = Quaternion.Euler(0, 0, 0);
        score.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void ShowScore(int value)
    {
        if (photonView.IsMine)
        {
            photonView.RPC("ShowScoreRPC", RpcTarget.AllBuffered, value);
        }
    }

    [PunRPC]
    private void ShowScoreRPC(int value)
    {
        score.text = value.ToString();
    }
}
