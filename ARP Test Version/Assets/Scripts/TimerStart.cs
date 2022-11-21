using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class TimerStart : MonoBehaviour
{
    public static TimerStart current;
    public bool runningTimer;
    float timeElapsed;
    TimeSpan timeChrono;
    public Text txtInfoPenalty;
    public Text previousChrono;
    public Text totalChrono;


    private void Awake()
    {
        current = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        previousChrono.text = "00:00:00";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTimer()
    {
        runningTimer = true;
        timeElapsed = 0F;
        StartCoroutine(TimerUpdate());
    }

    public void StopTimer()
    {
        runningTimer = false;
    }

    public void RestartTimer()
    {
        txtInfoPenalty.gameObject.SetActive(false);
        previousChrono.text = "00:00:00";
        totalChrono.text = "00:00:00";
    }

    public void DisplayTimerResult()
    {
        totalChrono.text = timeChrono.ToString("mm':'ss':'ff");
    }

    public void AddPenalty()
    {
        TimeSpan penaltyAdd = new TimeSpan(0,0,5);
        txtInfoPenalty.gameObject.SetActive(true);
        previousChrono.text = timeChrono.ToString("mm':'ss':'ff");
        totalChrono.text = (timeChrono + penaltyAdd).ToString("mm':'ss':'ff");
        //DisplayTimerResult();
    }

    private IEnumerator TimerUpdate()
    {
        while(runningTimer)
        {
            timeElapsed += Time.deltaTime;
            timeChrono = TimeSpan.FromSeconds(timeElapsed);
            previousChrono.text = timeChrono.ToString("mm':'ss':'ff");
            //DisplayTimerResult();
            yield return null;

        }
    }


}
