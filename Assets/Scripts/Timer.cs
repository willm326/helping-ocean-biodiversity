using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField]
    float secondsRemaining;
    Action callOnComplete; //Function to call when the timer reaches 0

    [SerializeField]
    Text clock;

    public void SetTimer(float startingSeconds, Action completionFunction)
    {
        secondsRemaining = startingSeconds;
        callOnComplete = completionFunction;
        clock.enabled = true;
        StartCoroutine(Countdown());
    }

    public void Stop()
    {
        StopAllCoroutines();
        clock.enabled = false;
    }

    IEnumerator Countdown()
    {
        while (secondsRemaining > 0)
        {
            updateClock();
            secondsRemaining -= 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
        updateClock();
        callOnComplete.Invoke();
        clock.enabled = false;
    }

    void updateClock()
    {
        if (secondsRemaining > 0)
        {
            clock.text = secondsRemaining.ToString();
        }
        else
        {
            clock.text = "0.00";
        }
    }
}


