using UnityEngine;
using Photon.Pun;

public class AsteroidSpawn : MonoBehaviour
{
    private int numberOfDisabledAsteroids = 0;
    [Header("Asteroid")]
    [SerializeField] private Asteroid asteroid;
    [SerializeField] private int numberOfAsteroids = 10;
    private Asteroid[] asteroids;

    [Header("Positions X and Y")]
    [SerializeField] private int maximumXPosition = 1000;
    [SerializeField] private int minimumXPosition = 220;
    [SerializeField] private int maximumYPosition = 500;
    [SerializeField] private int minimumYPosition = 90;

    private void Awake()
    {
        asteroids = new Asteroid[numberOfAsteroids];
    }

    public void InstantiateAsteroids()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            for (int position = 0; position < numberOfAsteroids; position++)
            {
                asteroids[position] = PhotonNetwork.Instantiate(asteroid.name, GetRandomPosition(), Quaternion.identity).GetComponent<Asteroid>();
            }
        }
    }

    private Vector3 GetRandomPosition()
    {
        int x = Random.Range(minimumXPosition, maximumXPosition);
        int y = Random.Range(minimumYPosition, maximumYPosition);

        return new Vector3(x, y, -1);
    }

    public void VerifyIfAllAsteroidsAreDisabled()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (AreAllAsteroidsDisabled())
            {
                SetAllAsteroids();
                numberOfDisabledAsteroids = 0;
            }
        }
    }

    private bool AreAllAsteroidsDisabled()
    {
        return numberOfDisabledAsteroids == numberOfAsteroids;
    }

    private void SetAllAsteroids()
    {
        foreach (Asteroid asteroid in asteroids)
        {
            asteroid.transform.position = GetRandomPosition();
            asteroid.SetAsEnabled();
        }
    }

    public void AddDisabledAsteroid()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            numberOfDisabledAsteroids++;
        }
    }
}
