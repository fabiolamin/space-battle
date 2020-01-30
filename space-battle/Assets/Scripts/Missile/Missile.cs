using UnityEngine;

public class Missile : MonoBehaviour
{
    private float timeAuxiliary;
    private Rigidbody2D missileRigidbody;
    [SerializeField] float shootingForce = 1000f;
    [SerializeField] float timeToDisable = 2f;

    private void Awake()
    {
        timeAuxiliary = timeToDisable;
        missileRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(IsEnabled())
        {
            if(IsCountdownDone())
            {
                Disable();
                timeToDisable = timeAuxiliary;
            }
        }
    }

    private bool IsEnabled()
    {
        return gameObject.activeSelf;
    }

    private bool IsCountdownDone()
    {
        timeToDisable -= Time.deltaTime;
        if(timeToDisable <= 0)
        {
            return true;
        }

        return false;
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void Move(Vector2 direction)
    {
        missileRigidbody.AddForce(direction * shootingForce);
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void SetPosition(Vector3 newPosition)
    {
        transform.position = newPosition;
    }

    public void SetRotation(Quaternion newRotation)
    {
        transform.rotation = newRotation;
    }
}
