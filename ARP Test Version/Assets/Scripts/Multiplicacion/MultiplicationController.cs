using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using Random = UnityEngine.Random;

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
    public string FlagIdTransition {get; set;}
    public int repeats { get; set; }
    System.Random rnd = new System.Random();
    int seed;
    int factor1;
    int factor2;
    int counterToStart;
    public Vector3 initialPos { get; set; }
    public string routeSelected { get; set; }
    public bool AllowAnswers { get; set; }
    public Text btnTextSound;
    public GameObject BtnBack;
    public Text textField;
    public GameObject boat;
    public GameObject effectsToWinner;
    public AudioClip soundWinner;
    public AudioClip soundLoser;
    public AudioClip soundMain;
    public GameObject BtnSound;
    public TextMesh txtop;
    public TextMesh txtmiddle;
    public TextMesh txtbottom;
    public TextMesh txtOperation;
    public GameObject PanelResultado;
    public GameObject PanelTime;
    public Text TxtTotalCorrectas;
    public int TotalCorrectas { get; set; }
    public Text TxtTotalIncorrectas;
    public int TotalIncorrectas { get; set; }
    public Text TxtIntentosRestantes;
    public int TotalIntentosRestantes { get; set; }
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
        TotalCorrectas = 0;
        TotalIncorrectas = 0;
        TotalIntentosRestantes = 0;
        onGoingGame = false;
        AllowAnswers = false;
        CalculateAnswer();
        PanelTime.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateTotalCorrects()
    {
        TotalCorrectas = TotalCorrectas + 1;
        TxtTotalCorrectas.text = TotalCorrectas.ToString();
    }

    public void UpdateTotalIncorrects()
    {
        TotalIncorrectas = TotalIncorrectas + 1;
        TxtTotalIncorrectas.text = TotalIncorrectas.ToString();
    }

    public void UpdateRemainingAttempts()
    {
        TotalIntentosRestantes = TotalIntentosRestantes + 1;
        TxtIntentosRestantes.text = TotalIntentosRestantes.ToString() + " de 3";
    }

    // This function is assigns the values to the island, one correct and two incorrects answers.
    void AssignValuesToIsland()
    {
        string option = islands[rnd.Next(2)];
        // Apply Unmatch
        //factor1 = factor1 - 1;
        if (factor1 == factor2)
        {
            seed = Random.Range(factor1 + 1, 10);
        }
        else
        {
            seed = Math.Max(factor1, factor2);
        }
        Dictionary<string, int> aux = new Dictionary<string, int>()
            { {"top",0},
            { "mid", 0},
            { "bot",0 }
        };
        switch (option)
        {
            case "TopIsland":
                responseCorrect = option;
                answer["top"] = question.ToString();
                factor1 = factor1 - 1;
                aux["top"] = question;
                aux["mid"] = (Random.Range(seed, 10) * factor1);
                aux["bot"] = (Random.Range(1, seed) * factor2);


                if (aux["top"] == aux["mid"])
                    aux["mid"] = aux["mid"] - factor2;
                if (aux["top"] == aux["bot"])
                    aux["bot"] = aux["bot"] + factor1;

                answer["middle"] = aux["mid"].ToString();
                answer["bottom"] = aux["bot"].ToString();
                break;
            case "MiddleIsland":
                responseCorrect = option;
                answer["middle"] = question.ToString();

                
                aux["top"] = Random.Range(seed, 10) * factor1;
                aux["mid"] = question;
                aux["bot"] = Random.Range(1, seed) * factor2;
                

                if (aux["mid"] == aux["top"])
                    aux["top"] = aux["top"] - factor1;
                if (aux["mid"] == aux["bot"])
                    aux["bot"] = aux["bot"] + factor2;


                answer["top"] = aux["top"].ToString();
                answer["bottom"] = aux["bot"].ToString();
                break;
            case "BottomIsland":
                responseCorrect = option;
                answer["bottom"] = question.ToString();
                aux["top"] = Random.Range(seed, 10) * factor1;
                aux["mid"] = Random.Range(1, seed) * factor2;
                aux["bot"] = question;


                if (aux["bot"] == aux["top"])
                    aux["top"] = aux["top"] - factor2;
                if (aux["bot"] == aux["mid"])
                    aux["mid"] = aux["mid"] + factor1;

                answer["top"] = aux["top"].ToString();
                answer["middle"] = aux["mid"].ToString();

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
            AllowAnswers = true;
            soundGame.Play();
            //if (!TimerStart.current.runningTimer)
                TimerStart.current.StartTimer();
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
    public void TakeControl()
    {
        CallFinishText();
            
    }

    // Enable text indicating the correct answer
    public void CallFinishText()
    {
        MultiplicationTextScript.current.FinishText(ObtainResult(), responseUser);
        if (ObtainResult() == responseUser)
        {
            // User Win
            UpdateTotalCorrects();
            effectsToWinner.SetActive(true);
            TimerStart.current.DisplayTimerResult();
            UpdateSound(soundWinner);
        }
        else
        {
            // User Lose
            TimerStart.current.AddPenalty();
            UpdateTotalIncorrects();
            UpdateSound(soundLoser);
        }
        if (ValidateAttempts())
        {
            btnRestart.gameObject.SetActive(true);
            PanelResultado.SetActive(true);
        }
        else
        {
            //TimerStart.current.DisplayFinalTimers();
            PanelTime.SetActive(true);
            PanelTime.gameObject.GetComponent<Text>().text = "Hola";
            BtnSound.SetActive(false);
            BtnBack.SetActive(false);
            PanelResultado.SetActive(false);
        }

        UpdateRemainingAttempts();
        //PanelResultado.SetActive(true);
        

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

            TimerStart.current.RestartTimer();
            AllowAnswers = false;
            CalculateAnswer();
            counterToStart = 3;
            UpdateStatusText(false);
            // posiciona al barco al inicio.
            boat.GetComponent<Animator>().Play(routeSelected, -1, 0f);
            boat.GetComponent<Animator>().speed = 0;
            boat.GetComponent<Animator>().SetBool(FlagIdTransition, false);
            MultiplicationTextScript.current.DeactivateText();
            MultiplicationTextScript.current.ActivatedTextCounter();
            ShowCounterToStartGame();
            
            effectsToWinner.SetActive(false);
            UpdateSound(soundMain);
            repeats = repeats + 1;
            btnRestart.gameObject.SetActive(false);
            PanelResultado.SetActive(false);
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
