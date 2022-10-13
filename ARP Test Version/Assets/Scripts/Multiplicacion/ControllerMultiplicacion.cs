using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ControllerMultiplicacion : MonoBehaviour
{
    public static ControllerMultiplicacion current;
    AudioSource soundGame;
    public bool onGoingGame { get; set; }
    int responseUser;
    int finalResult;
    int counterToStart;
    public GameObject textInput;
    public GameObject boat;
    public GameObject effectsToWinner;
    public AudioClip soundWinner;
    public AudioClip soundLoser;
    public UnityEvent OnClick = new UnityEvent();


    private void Awake()
    {
        current = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Shows the countdown to start the game
    public void ShowCounterToStartGame()
    {
        if (counterToStart >= 0)
        {
            Invoke("UpdateCounterToStart", 1.0f);
        }
        else
        {
            onGoingGame = true;
            soundGame.Play();
            SpawnerStart.current.CreateObjectStart();
        }
    }

    // Updates the TextField that show the countdown to start the game (void ShowCounterToStartGame)
    private void UpdateCounterToStart()
    {
        TextScript.current.SetText(counterToStart);
        counterToStart--;
        Invoke("ShowCounterToStartGame", 1.0f);
    }

    // Change the sound in the game.
    public void UpdateSound(AudioClip newSound)
    {
        soundGame.Stop();
        soundGame.clip = newSound;
        soundGame.Play();
        soundGame.loop = true;
    }

    // Enable text indicating the correct answer
    public void CallFinishText()
    {
        textInput.gameObject.SetActive(false);
        TextScript.current.FinishText(ObtainResult(), responseUser);
        if (ObtainResult() == responseUser)
        {
            // User Win
            effectsToWinner.SetActive(true);
            UpdateSound(soundWinner);
        }
        else
        {
            // User Lose
            UpdateSound(soundLoser);
        }
    }

    // Return the result of people it will be inside the house.
    public int ObtainResult()
    {
        return finalResult;
    }
}
