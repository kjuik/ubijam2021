using System;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public Vector3 speed;
    public float width;

    public GameObject beginning;
    public GameObject middle1;
    public GameObject middle2;
    public GameObject ending;

    public AnimationCurve startCurve;
    public AnimationCurve endCurve;

    public bool shouldDecideWhenToStop;

    float startTime;

    float currentSpeedPercentage;

    bool isScrolling;
    bool isStarting;
    bool hasWon;

    static bool isSlowingDown;
    static float startSlowingDownTime;

    List<GameObject> currentPanels = new List<GameObject>();

    private void Start()
    {
        if (shouldDecideWhenToStop)
        {
            isSlowingDown = false;
            startSlowingDownTime = 0f;
        }
    }

    internal void Begin()
    {
        isScrolling = true;
        isStarting = true;
        startTime = Time.realtimeSinceStartup;

        currentPanels.Add(beginning);
        currentPanels.Add(middle1);
    }

    internal void Lose()
    {
        isScrolling = false;
    }

    internal void Win()
    {
        hasWon = true;
    }

    private void Update()
    {
        if (isScrolling)
        {
            AdjustSpeed();
            ScrollPanels();
            SwapPanels();
        }
    }


    private void AdjustSpeed()
    {
        if (isStarting && currentSpeedPercentage < 1f)
        {
            currentSpeedPercentage = startCurve.Evaluate(Time.realtimeSinceStartup - startTime);
            if (currentSpeedPercentage >= 1f)
            {
                currentSpeedPercentage = 1f;
                isStarting = false;
            }
        }
        if (hasWon && !isSlowingDown && shouldDecideWhenToStop && ending.transform.localPosition.x < width / 4)
        {
            isSlowingDown = true;
            startSlowingDownTime = Time.realtimeSinceStartup;

            GameManager.Instance.ReachBear();

        }
        if (isSlowingDown && currentSpeedPercentage > 0f) 
        { 
            currentSpeedPercentage = endCurve.Evaluate(Time.realtimeSinceStartup - startSlowingDownTime);
            if (currentSpeedPercentage <= 0f)
            {
                currentSpeedPercentage = 0f;
                isScrolling = false;
            }
        }
    }

    private void ScrollPanels()
    {
        foreach(var panel in currentPanels)
        {
            panel.transform.position += speed * currentSpeedPercentage * Time.deltaTime;
        }
    }

    private void SwapPanels()
    {
        for(var i=0; i<currentPanels.Count;++i)
        {
            var xPosition = currentPanels[i].transform.localPosition.x;

            if (xPosition <= -width)
            {
                if (hasWon && !currentPanels.Contains(ending))
                {
                    currentPanels[i] = ending;
                }
                else if (currentPanels[i] == beginning)
                {
                    currentPanels[i] = middle2;
                }

                currentPanels[i].transform.localPosition = new Vector3(
                    xPosition + 2*width,
                    currentPanels[i].transform.localPosition.y,
                    currentPanels[i].transform.localPosition.z);
            }
        }
    }
}
