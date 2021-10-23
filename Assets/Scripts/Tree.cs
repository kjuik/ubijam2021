using UnityEngine;

public class Tree : MonoBehaviour
{
    public Vector3 speed;

    bool isChopped;
    float fallDownDuration;

    public void Update()
    {
        //TODO should I move if chopped?
        transform.position += speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Chop Trigger" && !isChopped)
        {
            ChopDown();
        }
    }

    private void ChopDown()
    {
        isChopped = true;
        //TODO animate

        GetComponentInChildren<Collider>().enabled = false;
        Destroy(gameObject, fallDownDuration);
    }
}
