﻿using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class Asteroid : MonoBehaviour
{
    private PhotonView photonView;
    private AsteroidSpawn asteroidSpawn;
    [SerializeField] private float speedRotation = 40f;
    public int Index { get; set; }

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        asteroidSpawn = GameObject.FindGameObjectWithTag("AsteroidSpawn").GetComponent<AsteroidSpawn>();
    }

    private void Update()
    {
        Rotate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Missile"))
        {
            Missile missile = collision.gameObject.GetComponent<Missile>();
            missile.playerWhoShot.AddScore();
            missile.Disable();
            SetAsDisabled();
            asteroidSpawn.AddDisabledAsteroid();
            asteroidSpawn.VerifyIfAllAsteroidsAreDisabled();
            asteroidSpawn.InstatiatedParticles[Index].Play();
        }
    }

    private void Rotate()
    {
        transform.Rotate(0, 0, speedRotation * Time.deltaTime);
    }

    public void SetAsDisabled()
    {
        if (photonView.IsMine)
        {
            photonView.RPC("Disable", RpcTarget.All);
        }
    }

    [PunRPC]
    private void Disable()
    {
        gameObject.SetActive(false);
    }

    public void SetAsEnabled()
    {
        if (photonView.IsMine)
        {
            photonView.RPC("Enable", RpcTarget.All);
        }
    }

    [PunRPC]
    private void Enable()
    {
        gameObject.SetActive(true);
    }
}
