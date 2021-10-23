using System.Collections;
using UnityEngine;

public class Lumberjack : MonoBehaviour
{
    public float chopDuration;

    public Collider hitbox;
    public Collider chopTrigger;

    bool isRunning;
    bool isChopping;

    internal void StartRunning()
    {
        isRunning = true;
        //TODO animate
    }

    internal void TryChop()
    {
        if (isRunning && !isChopping)
        {
            StartCoroutine(ChopCoroutine());
        }
    }

    private IEnumerator ChopCoroutine()
    {
        isChopping = true;
        chopTrigger.gameObject.SetActive(true);

        //TODO animate
        yield return new WaitForSeconds(chopDuration);

        chopTrigger.gameObject.SetActive(false);
        isChopping = false;
    }


    internal void Die()
    {
        StopAllCoroutines();
        isChopping = false;
        isRunning = false;
        //TODO animate
    }

    internal void Win()
    {
        StopAllCoroutines();
        isChopping = false;
        isRunning = false;
        //TODO animate
    }
}
