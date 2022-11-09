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
    Dictionary<string, string> answer = new Dictionary<string, string>(){
        {"top","" },
        {"middle","" },
        {"bottom","" }
    };
    public string responseUser { get; set; }
    int question { get; set; }
    string responseCorrect { get; set; }
    public int repeats { get; set; }
    System.Random rnd = new System.Random();
    int seed;
    int factor1;
    int factor2;
    int counterToStart;
    public Vector3 initialPos { get; set; }
    public bool onRoad { get; set; }
    public Text btnTextSound;
    public Text textField;
    public GameObject boat;
    public GameObject effectsToWinner;
    public AudioClip soundWinner;
    public AudioClip soundLoser;
    public AudioClip soundMain;
    public TextMesh txtop;
    public TextMesh txtmiddle;
    public TextMesh txtbottom;
    public TextMesh txtOperation;
    public Button btnRestart;
    public UnityEvent OnClick = new UnityEvent();


    private void Awake()
    {
        current = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        counterToStart = 3;
        repeats = 1;
        initialPos = boat.transform.localPosition;
        responseCorrect = "";
        responseUser = "";
        onGoingGame = false;
        CalculateAnswer();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // This function is assigns the values to the island, one correct and two incorrects answers.
    void AssignValuesToIsland()
    {
        string option = islands[rnd.Next(2)];
        switch (option)
        {
            case "TopIsland":
                responseCorrect = option;
                answer["top"] = question.ToString();
                //txtop.text = question.ToString();
                answer["middle"] = (Random.Range(seed,10) * factor1).ToString();
                //txtmiddle.text = rnd.Next(10 - factor1).ToString();
                answer["bottom"] = (Random.Range(1,seed) * factor2).ToString();
                //txtbottom.text = rnd.Next(10 - factor2).ToString();
                break;
            case "MiddleIsland":
                responseCorrect = option;
                answer["top"] = (Random.Range(seed, 10) * factor1).ToString();
                //txtop.text = rnd.Next(10 - factor2).ToString();
                answer["middle"] = question.ToString();
                //txtmiddle.text = question.ToString();
                answer["bottom"] = (Random.Range(1, seed) * factor2).ToString();
                //txtbottom.text = rnd.Next(10 - factor1).ToString();
                break;
            case "BottomIsland":
                responseCorrect = option;
                answer["top"] = (Random.Range(seed, 10) * factor1).ToString();
                //txtop.text = (rnd.Next(System.Math.Abs(factor1 - 1)) * factor2).ToString();
                answer["middle"] = (Random.Range(1, seed) * factor2).ToString();
                //txtmiddle.text = (rnd.Next(factor2 + 1) * factor1).ToString();
                answer["bottom"] = question.ToString();
                //txtbottom.text = question.ToString();
                break;
        }
    }

    // Calcular las opciones para retar al usuario.
    void CalculateAnswer()
    {
        seed =  Random.Range(0, 10);
        factor1 = Random.Range(seed, 10);
        factor2 =  Random.Range(factor1,10);
        question = factor1 * factor2;
        answer["operation"] = factor1.ToString()+"x"+factor2.ToString();
        AssignValuesToIsland();
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

    void UpdateStatusText(bool flag)
    {
        txtOperation.gameObject.SetActive(flag);
        txtop.gameObject.SetActive(flag);
        txtmiddle.gameObject.SetActive(flag);
        txtbottom.gameObject.SetActive(flag);
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
            txtop.text = answer["top"];
            txtmiddle.text = answer["middle"];
            txtbottom.text = answer["bottom"];
            txtOperation.text = answer["operation"];
            soundGame = GetComponent<AudioSource>();
            UpdateStatusText(true);
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

    // Get the  sound's button text.
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

    // Take control' again at the end of the animation when restarting the game..
    public void TakeControl(string animationClip)
    {
        CallFinishText();
        if (ValidateAttempts())
        {
            CalculateAnswer();
            
        }
            
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
        if (ValidateAttempts())
            btnRestart.gameObject.SetActive(true);
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
    public string ObtainResult()
    {
        return responseCorrect;
    }

    // Restart the game (3 times)
    public void RestartGame()
    {
        if(ValidateAttempts())
        {
            counterToStart = 3;
            //MultiplicationTextScript.current.DeactivateText();
            MultiplicationTextScript.current.ActivatedTextCounter();
            ShowCounterToStartGame();
            UpdateStatusText(false);
            effectsToWinner.SetActive(false);
            UpdateSound(soundMain);
            repeats = repeats + 1;
            btnRestart.gameObject.SetActive(false);
        }

        
    }

    // Return if gets the limit of attempts.
    public bool ValidateAttempts()
    {
        if (repeats < 3)
            return true;
        return false;
    }
}
