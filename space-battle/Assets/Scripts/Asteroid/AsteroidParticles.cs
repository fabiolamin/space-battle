using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class AsteroidParticles : MonoBehaviour
{
    private PhotonView photonView;
    private ParticleSystem particleSystem;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        particleSystem = GetComponent<ParticleSystem>();
    }

    public void Play()
    {
        if(photonView.IsMine)
        {
            photonView.RPC("PlayRPC", RpcTarget.All);
        }
    }

    [PunRPC]
    private void PlayRPC()
    {
        particleSystem.Play();
    }
}
