using UnityEngine;

public class Tree : MonoBehaviour
{
    public Vector3 speed;
    public float fallDuration;
    public GameObject particles;

    bool isFalling;

    public void Update()
    {
        switch (GameManager.Instance.CurrentState)
        {
            case GameManager.State.Started:
                break;
            case GameManager.State.Playing:
                transform.position += speed * Time.deltaTime;
                break;
            case GameManager.State.Lost:
                break;
            case GameManager.State.Won:
            case GameManager.State.EndScreen:
                if (!isFalling)
                {
                    Fall();
                }
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Chop Trigger" && !isFalling)
        {
            Fall();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Chop Trigger" && !isFalling)
        {
            Fall();
        }
    }

    private void Fall()
    {
        isFalling = true;

        transform.Find("Visuals").gameObject.SetActive(false);
        particles.SetActive(true);

        GetComponentInChildren<Collider>().enabled = false;
        Destroy(gameObject, fallDuration);
    }
}
