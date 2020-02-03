using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerShooting : MonoBehaviour
{
    private PhotonView photonView;
    private Missile[] missiles;
    private int position = 0;
    [SerializeField] private Missile missile;
    [SerializeField] private int numberOfMissiles = 5;
    [SerializeField] private Transform missileSpawn;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        missiles = new Missile[numberOfMissiles];
        if (photonView.IsMine)
        {
            photonView.RPC("InstantiateMissiles", RpcTarget.AllBuffered);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && photonView.IsMine)
        {
            photonView.RPC("SetShooting", RpcTarget.All);
        }
    }

    [PunRPC]
    private void InstantiateMissiles()
    {
        for (int position = 0; position < numberOfMissiles; position++)
        {
            missiles[position] = Instantiate(missile, missileSpawn.position, Quaternion.identity).GetComponent<Missile>();
            missiles[position].Disable();
            missiles[position].playerWhoShot = GetComponent<PlayerScore>();
        }
    }

    [PunRPC]
    private void SetShooting()
    {
        if (IsAnAvailablePosition())
        {
            SetMissile();
            Shoot();
            position++;
        }
        else
        {
            position = 0;
        }
    }

    private bool IsAnAvailablePosition()
    {
        return position < missiles.Length;
    }

    private void SetMissile()
    {
        missiles[position].SetPosition(missileSpawn.position);
        missiles[position].SetRotation(transform.rotation);
        missiles[position].Enable();
    }

    private void Shoot()
    {
        missiles[position].Move(transform.up);
    }
}
