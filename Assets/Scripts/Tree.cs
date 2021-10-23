using UnityEngine;

public class Tree : MonoBehaviour
{
    public Vector3 speed;
    public float fallDuration;
    public GameObject particles;

    bool isFalling;

    public void Update()
    {
        //TODO should I move if chopped?
        transform.position += speed * Time.deltaTime;
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

        SoundEffects.Instance.PlayChop();

        transform.Find("Visuals").gameObject.SetActive(false);
        particles.SetActive(true);

        GetComponentInChildren<Collider>().enabled = false;
        Destroy(gameObject, fallDuration);
    }
}
