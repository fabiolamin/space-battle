using Photon.Pun;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PhotonView photonView;
    private Rigidbody2D playerRigidbody;
    private Vector2 screenBoundaries;
    private float spriteWidth;
    private float spriteHeight;
    [SerializeField] float speed = 200f;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        screenBoundaries = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x / 2;
        spriteHeight = GetComponent<SpriteRenderer>().bounds.size.y / 2;
    }

    private void FixedUpdate()
    {
        if(photonView.IsMine)
        {
            Move();
        }
    }

    private void LateUpdate()
    {
        if(photonView.IsMine)
        {
            SetBoundaries();
        }
    }

    private void Move()
    {
        Vector2 newPosition = playerRigidbody.position;
        float angle = playerRigidbody.rotation;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            newPosition.y += speed * Time.fixedDeltaTime;
            angle = 0;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            newPosition.y -= speed * Time.fixedDeltaTime;
            angle = -180;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            newPosition.x += speed * Time.fixedDeltaTime;
            angle = -90;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            newPosition.x -= speed * Time.fixedDeltaTime;
            angle = 90;
        }

        playerRigidbody.MovePosition(newPosition);
        playerRigidbody.MoveRotation(angle);
    }

    private void SetBoundaries()
    {
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, 140f + spriteWidth, screenBoundaries.x - spriteWidth);
        position.y = Mathf.Clamp(position.y, 0f + spriteHeight, screenBoundaries.y - spriteHeight);
        transform.position = position;
    }
}
