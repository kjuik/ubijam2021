using UnityEngine;

public class Tree : MonoBehaviour
{
    public Vector3 speed;
    public float fallDuration;

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

        //TODO animate
        transform.Find("Visuals").gameObject.SetActive(false);

        GetComponentInChildren<Collider>().enabled = false;
        Destroy(gameObject, fallDuration);
    }
}
