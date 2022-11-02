using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MultiplicationController : MonoBehaviour
{
    public static MultiplicationController current;
    AudioSource soundGame { get; set; }
    public bool onGoingGame { get; set; }
    List<string> islands = new List<string> { "TopIsland", "MiddleIsland", "BottomIsland" };
    int responseUser;
    string responseCorrect;
    System.Random rnd = new System.Random();
    int finalResult;
    int factor1;
    int factor2;
    int counterToStart;
    public bool onRoad { get; set; }
    public Text btnTextSound;
    public Text textField;
    public GameObject boat;
    public GameObject effectsToWinner;
    public AudioClip soundWinner;
    public AudioClip soundLoser;
    public TextMesh txtop;
    public TextMesh txtmiddle;
    public TextMesh txtbottom;
    public TextMesh txtOperation;
    public UnityEvent OnClick = new UnityEvent();


    private void Awake()
    {
        current = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        counterToStart = 3;
        onGoingGame = false;
        factor1 = rnd.Next(10);
        factor2 = rnd.Next(factor1);

        txtOperation.text = factor1.ToString() + "x" + factor2.ToString();
        finalResult = factor1*factor2;
        responseUser = 8;
        string option = islands[rnd.Next(2)];
        switch (option) {
            case "TopIsland": responseCorrect = option;
                txtop.text = finalResult.ToString();
                txtmiddle.text = rnd.Next(10 - factor1).ToString();
                txtbottom.text = rnd.Next(10 - factor2).ToString();
                break;
            case "MiddleIsland": responseCorrect = option;
                txtmiddle.text = finalResult.ToString();
                txtop.text = rnd.Next(10 - factor2).ToString();
                txtbottom.text = rnd.Next(10 - factor1).ToString();
                break;
            case "BottomIsland": responseCorrect = option;
                txtbottom.text = finalResult.ToString();
                txtop.text = rnd.Next(10 - factor1).ToString();
                txtmiddle.text = rnd.Next(10 - factor2).ToString();
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void restartGame()
    {
        SceneManager.LoadScene(4);
    }

    // Returns the prefab passed as parameter from Unity
    public GameObject GetBoat()
    {
        return boat;
    }

    // Returns the TextField from the Game's UI
    public Text GetTextField()
    {
        return textField;
    }

    // Shows the countdown to start the game
    public void ShowCounterToStartGame()
    {
        Debug.Log("Entrando en ShowCounterToStartGame");
        if (counterToStart >= 0)
        {
            Invoke("UpdateCounterToStart", 1.0f);
        }
        else
        {
            onGoingGame = true;
            soundGame = GetComponent<AudioSource>();
            soundGame.Play();
        }
    }

    // Updates the TextField that show the countdown to start the game (void ShowCounterToStartGame)
    private void UpdateCounterToStart()
    {
        MultiplicationTextScript.current.SetText(counterToStart);
        counterToStart--;
        Invoke("ShowCounterToStartGame", 1.0f);
    }


    public Text GetSoundButton()
    {
        return btnTextSound;
    }

    // Change the sound in the game.
    public void UpdateSound(AudioClip newSound)
    {
        soundGame.Stop();
        soundGame.clip = newSound;
        soundGame.Play();
        soundGame.loop = true;
    }

    // Return the audio clip it will be played
    public AudioSource GetAudioClip()
    {
        return soundGame;
    }

    // Enable text indicating the correct answer
    public void CallFinishText()
    {
        MultiplicationTextScript.current.FinishText(ObtainResult(), responseUser);
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

    // Return the current position from the Boat
    public Vector3 GetBoatPosition()
    {
        boat.gameObject.GetComponent<Animator>().speed = 0;
        return boat.transform.localPosition;
    }

    // Sets the current position to Boat
    public void SetBoatPosition(Vector3 currentPosition)
    {
        boat.gameObject.GetComponent<Animator>().speed = 1;
        boat.transform.localPosition = currentPosition;
    }

    // Return the result of people it will be inside the house.
    public int ObtainResult()
    {
        return finalResult;
    }
}
