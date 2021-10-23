using System.Collections;
using UnityEngine;

public class Lumberjack : MonoBehaviour
{
    public float chopDuration;
    public float chopTriggerDelay;

    public Collider hitbox;
    public Collider chopTrigger;

    bool isRunning;
    public bool IsChopping { get; private set; }

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
        if (isRunning && !IsChopping)
        {
            StartCoroutine(ChopCoroutine());
        }
    }

    private IEnumerator ChopCoroutine()
    {
        IsChopping = true;
        animator.SetTrigger("chop");

        yield return new WaitForSeconds(chopTriggerDelay);
        chopTrigger.gameObject.SetActive(true);


        yield return new WaitForSeconds(chopDuration - chopTriggerDelay);
        chopTrigger.gameObject.SetActive(false);
        IsChopping = false;
    }


    internal void Die()
    {
        SoundEffects.Instance.PlayDie();

        StopAllCoroutines();
        IsChopping = false;
        isRunning = false;
        animator.SetTrigger("ko");
    }

    internal void Win()
    {
        SoundEffects.Instance.PlayWin();

        StopAllCoroutines();
        IsChopping = false;
        isRunning = false;
        //TODO animate
    }
}
