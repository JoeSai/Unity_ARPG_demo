using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyButton
{
    public bool IsPressing = false;
    public bool OnPressed = false;
    public bool OnReleased = false;
    public bool IsExtending = false;
    public bool IsDelaying = false;

    public bool DoubleClick = false;

    [Header("===== Settings =====")]
    public float extendingDuration = 0.15f;
    public float delayingDuration = 1.0f;
    public float longPressDuration = 1.0f;

    public bool curState = false;
    public bool lastState = false;
    public float lastPressTime;


    private MyTimer extTimer = new MyTimer();
    private MyTimer delayTimer = new MyTimer();

    public void Tick(bool input)
    {
        extTimer.Tick();
        delayTimer.Tick();

        curState = input;
        IsPressing = curState;

        if (OnPressed && IsExtending)
        {
            DoubleClick = lastPressTime < longPressDuration;
            lastPressTime = 0;
        }
        else
        {
            DoubleClick = false;
        }

        OnPressed = false;
        OnReleased = false;
        IsExtending = false;
        IsDelaying = false;


        if (curState != lastState)
        {
            if (curState == true)
            {
                OnPressed = true;
                StartTimer(delayTimer, delayingDuration);
            }
            else
            {
                OnReleased = true;
                StartTimer(extTimer, extendingDuration);
            }
        }


        if (IsPressing)
        {
            lastPressTime += Time.deltaTime;
        }

        lastState = curState;
        if (extTimer.state == MyTimer.STATE.RUN)
        {
            IsExtending = true;
        }

        if(delayTimer.state == MyTimer.STATE.RUN)
        {
            IsDelaying = true;
        }

    }

    private void StartTimer(MyTimer timer, float duration)
    {
        timer.duration = duration;
        timer.Go();
    }

}
