using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using ARProject.GamesPlayed;
using MongoDB.Bson;

public class SumaController : MonoBehaviour
{
    private readonly int ScoreGame = 50;
    public bool onGoingGame { get; set; }
    public bool endRoute { get; set; }
    public bool startRigth;
    public int peopleCounterLeft { get; set; }
    public int peopleCounterRigth { get; set; }
    int finalResult;
    // {Numero limite generador personajes , Factor Velocidad Personaje}
    int[,] difficulty = { { 1, 5 }, { 2, 8 }, { 3, 10 } };
    System.Random rnd;
    int op;
    public float Speed { get; set; }
    int counterToStart;
    int responseUser;
    AudioSource soundGame { get; set; }
    public GameObject house;
    public GameObject textInput;
    public Transform prefab;
    public Transform newParent;
    public Transform limitInsideHouse;
    public Text textField;
    public GameObject effectsToWinner;
    public AudioClip soundWinner;
    public AudioClip soundLoser;
    public Text btnTextSound;
    public static SumaController controlCharacter;
    private GamesPlayed Gp;
    public GameObject PanelIntro;



    private void Awake()
    {
        endRoute = false;
        onGoingGame = false;
        controlCharacter = this;
        startRigth = true;
        responseUser = 0;
        rnd = new System.Random();
        op = 0;
        Speed = difficulty[op, 1] * 0.1f;
        peopleCounterRigth = rnd.Next(10);
        finalResult = peopleCounterRigth;
        house.GetComponent<Animator>().speed = 0;
        Gp = new GamesPlayed();
        //soundGame = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //soundGame = GetComponent<AudioSource>();
        //SumaTextScript.current.SetText("Juego en progreso");
        counterToStart = 3;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Returns the prefab passed as parameter from Unity
    public Transform GetPrefab()
    {
        return prefab;
    }

    public GameObject GetHouse()
    {
        return house;
    }

    // Returns the parent of the prefab
    public Transform GetNewParent()
    {
        return newParent;
    }

    // Returns the boundary located inside the house and thus deletes the character that entered.
    public Transform GetLimitInsideHouse()
    {
        return limitInsideHouse;
    }

    // Returns the TextField from the Game's UI
    public Text GetTextField()
    {
        return textField;
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
            PanelIntro.SetActive(false);
            onGoingGame = true;
            soundGame = GetComponent<AudioSource>();
            soundGame.Play();
            SumaSpawnerStart.current.CreateObjectStart();
        }
    }


    public Text GetSoundButton()
    {
        return btnTextSound;
    }

    // Return the audio clip it will be played
    public AudioSource GetAudioClip()
    {
        return soundGame;
    }

    // Updates the TextField that show the countdown to start the game (void ShowCounterToStartGame)
    private void UpdateCounterToStart()
    {
        Debug.Log("SumaController - UpdateCounterToStart: Valor de counterToStart: " + counterToStart.ToString());
        SumaTextScript.current.SetText(counterToStart);
        counterToStart--;
        Invoke("ShowCounterToStartGame", 1.0f);
    }

    // show the final result
    public void ShowResult(float axisX, float axisZ)
    {
        if (onGoingGame == false)
        {
            int valuef = ObtainResult();
            int count = 0;
            axisX = (axisX + 0.0200f) / 2;
            axisZ = (axisZ + 0.0200f) / 2;
            for (float x = -axisX; x < axisX; x = x + 0.0100f)
            {
                for (float z = axisZ; z > -axisZ; z = z - 0.0100f)
                {
                    if (count < valuef)
                    {
                        SumaSpawnerResult.current.CreateObjectResult(x * 10, z * 10);
                        count++;
                    }
                }
            }


            ActivateInputResult();
        }
    }


    public void ShowResult()
    {
        if (onGoingGame == false)
        {
            int valuef = ObtainResult();
            string nameActivity = "SpawnerResult_";
            for (int i = 10; i > valuef; i--)
            {
                string characterToEnable = nameActivity + i.ToString();
                GameObject character = GameObject.Find(characterToEnable);
                //GameObject character = Resources.Load
                character.SetActive(false);
            }
            ActivateInputResult();
        }
    }

    // Enables data input to the user to indicate their response.
    private void ActivateInputResult()
    {
        textInput.gameObject.SetActive(true);
        InputField resultInput = textInput.GetComponent<InputField>();
        resultInput.Select();
        resultInput.ActivateInputField();
        resultInput.onEndEdit.AddListener(delegate { DisplayResultInsideHouse(resultInput.text); });
        // TODO: Implementar Borrado de Listeners, revisar documentacion
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
        PanelIntro.SetActive(true);
        SumaTextScript.current.FinishText(ObtainResult(), responseUser);
        if (ObtainResult() == responseUser)
        {
            // User Win
            GamesPlayed newGame = new GamesPlayed();
            newGame.User = ObjectId.Parse(PlayerPrefs.GetString("IDUser"));
            newGame.Group = ObjectId.Parse(PlayerPrefs.GetString("IDGroup"));
            
            newGame.DayPlayed = DateTime.Now.ToShortDateString();
            newGame.FinalTimer = "00:00:00";
            //Game's id in specific.
            newGame.Game = ObjectId.Parse("64f51341cc401f225d28727c");
            effectsToWinner.SetActive(true);
            UpdateSound(soundWinner);
            if(!PlayerPrefs.GetInt("RewardActivity").Equals(""))
            {
                newGame.FinalScore = PlayerPrefs.GetInt("RewardActivity") + ScoreGame;
            }
            else
            {
                newGame.FinalScore = ScoreGame;
            }
            
            Gp.CreateRecord(newGame);
        }
        else
        {
            // User Lose
            UpdateSound(soundLoser);
        }
    }

    // Increase the value of people's counter  from the rigth.
    public void IncreasePeopleCounterRigth()
    {
        peopleCounterRigth++;
    }

    // Decrease the value of people's counter  from the rigth.
    public void DecreasePeopleCounteRigth()
    {
        peopleCounterRigth--;
        Debug.Log("     DecreasePeopleCounteRigth:" + peopleCounterRigth);
    }

    // Return the result of people it will be inside the house.
    public int ObtainResult()
    {
        return finalResult;
    }

    // The house disappears at the end of the game.
    public void DisplayResultInsideHouse(String answer)
    {
        Debug.Log("Valor de answer: " + answer);
        house.GetComponent<Animator>().speed = 1;
        house.GetComponent<Animator>().Play("Base Layer.MoveHouseSuma", 1, 0);
        //house.SetActive(false);
        //int p = rnd.Next(10);
        //SumaTextScript.current.SetText("Juego Finalizado");
        responseUser = Int32.Parse(answer);
        Invoke("CallFinishText", 0.5f);
    }

    // Creates the object according to the passing value.
    public void CreateObject()
    {
        SumaSpawnerStart.current.CreateObjectStart();
    }
}
