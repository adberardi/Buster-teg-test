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
    public TextMesh txtChrono;
    List<string> resultsTimer = new List<string>();
    public Text finalTextOne;
    public Text finalTextTwo;
    public Text finalTextThree;
    public Text finalText;
    TimeSpan TimeFinal { get; set; }



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

    public string GetFinalTextOne()
    {
        return finalTextOne.text;
    }

    public string GetFinalTextTwo()
    {
        return finalTextTwo.text;
    }

    public string GetFinalTextThree()
    {
        return finalTextThree.text;
    }

    public string GetFinalText()
    {
        return finalText.text;
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
        txtChrono.text = "00:00:00";
    }

    public List<string> DisplayFinalTimers()
    {
        finalTextOne.text = resultsTimer[0];
        finalTextTwo.text = resultsTimer[1];
        finalTextThree.text = resultsTimer[2];
        finalText.text = TimeFinal.ToString("mm':'ss':'ff");
        return resultsTimer;
    }

    public void DisplayTimerResult()
    {
        totalChrono.text = timeChrono.ToString("mm':'ss':'ff");
        TimeFinal += timeChrono;
        resultsTimer.Add(totalChrono.text);
    }

    //Gets the Final Timer
    public TimeSpan GetTimerResult()
    {
        totalChrono.text = timeChrono.ToString("mm':'ss':'ff");
        return TimeFinal += timeChrono;
    }

    public void AddPenalty()
    {
        TimeSpan penaltyAdd = new TimeSpan(0,0,5);
        txtInfoPenalty.gameObject.SetActive(true);
        previousChrono.text = timeChrono.ToString("mm':'ss':'ff");
        totalChrono.text = (timeChrono + penaltyAdd).ToString("mm':'ss':'ff");
        TimeFinal += (timeChrono + penaltyAdd);
        resultsTimer.Add(totalChrono.text);
        
        //DisplayTimerResult();
    }

    private IEnumerator TimerUpdate()
    {
        while(runningTimer)
        {
            timeElapsed += Time.deltaTime;
            timeChrono = TimeSpan.FromSeconds(timeElapsed);
            txtChrono.text = timeChrono.ToString("mm':'ss':'ff");
            //DisplayTimerResult();
            yield return null;

        }
    }


}
