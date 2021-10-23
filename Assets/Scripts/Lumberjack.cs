using System.Collections;
using UnityEngine;

public class Lumberjack : MonoBehaviour
{
    public float chopDuration;
    public float chopTriggerDelay;

    public Collider hitbox;
    public Collider chopTrigger;

    bool isRunning;
    bool isChopping;

    Animator animator;

    private void Start()
    {
        chopTrigger.gameObject.SetActive(false);
        animator = GetComponentInChildren<Animator>();
    }

    internal void StartRunning()
    {
        isRunning = true;
        animator.SetTrigger("start");
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
        animator.SetTrigger("chop");

        yield return new WaitForSeconds(chopTriggerDelay);
        chopTrigger.gameObject.SetActive(true);


        yield return new WaitForSeconds(chopDuration - chopTriggerDelay);
        chopTrigger.gameObject.SetActive(false);
        isChopping = false;
    }


    internal void Die()
    {
        StopAllCoroutines();
        isChopping = false;
        isRunning = false;
        animator.SetTrigger("ko");
    }

    internal void Win()
    {
        StopAllCoroutines();
        isChopping = false;
        isRunning = false;
        //TODO animate
    }
}
