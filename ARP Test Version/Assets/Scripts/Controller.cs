using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public bool onGoingGame { get; set; }
    public bool endRoute { get; set; }
    public bool startRigth;
    public bool startLeft;
    public int peopleCounterLeft { get; set; }
    public int peopleCounterRigth { get; set; }
    int finalResult;
    // {Numero limite generador personajes , Factor Velocidad Personaje}
    int[,] difficulty = { { 1, 5 }, { 2, 8 }, {3, 10} };
    System.Random rnd;
    int op;
    public float Speed { get; set; }
    int counterToStart;
    int responseUser;
    AudioSource soundGame;
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
    public static Controller controlCharacter;



    private void Awake()
    {
        endRoute = false;
        onGoingGame = false;
        counterToStart = 3;
        controlCharacter = this;
        startRigth = true;
        startLeft = false;
        responseUser = 0;
        rnd = new System.Random();
        //op = rnd.Next(difficulty.Length - 1);
        op = 0;
        //peopleCounterLeft = difficulty[op,0];
        Speed = difficulty[op,1] * 0.1f;
        peopleCounterRigth = rnd.Next(10);
        peopleCounterLeft = rnd.Next(1, peopleCounterRigth);
        finalResult = peopleCounterRigth - peopleCounterLeft;
        house.GetComponent<Animator>().speed = 0;
        soundGame = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //TextScript.current.SetText("Juego en progreso");
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
        if (counterToStart > 0)
        {
            counterToStart--;
            Invoke("UpdateCounterToStart", 1.0f);
        }
        else
        {
            onGoingGame = true;
            soundGame.Play();
            SpawnerStart.current.CreateObjectStart();
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
        TextScript.current.SetText(counterToStart);
        Invoke("ShowCounterToStartGame", 1.0f);
    }

    // show the final result
    public void ShowResult(float axisX, float axisZ)
    {
        if (onGoingGame == false)
        {
            int valuef = ObtainResult();
            Debug.Log("Resultado final: " + valuef);
            int count = 0;
            axisX = axisX / 2;
            axisZ = axisZ / 2;
            for (float x = -0.0200f; x < 0.0200f; x = x + 0.0100f)
            {
                Debug.Log("Primer Loop For");
                for (float z = 0.0200f; z > -0.0200f; z = z - 0.0100f)
                {
                    Debug.Log("Segundo Loop For");
                    if (count < valuef)
                    {
                        Debug.Log("Antes de entrar a CreateObjectResult");
                        SpawnerResult.current.CreateObjectResult(x * 10, z * 10);
                        count++;
                    }
                }
            }

            /*while(count < valuef)
            {
                SpawnerResult.current.CreateObjectResult(0.1f * count, 0.01f * count);
                count++;
            }*/

            ActivateInputResult();

            //DisplayResultInsideHouse();
        }
    }

    // Enables data input to the user to indicate their response.
    private void ActivateInputResult()
    {
        textInput.gameObject.SetActive(true);
        InputField resultInput = textInput.GetComponent<InputField>();
        resultInput.Select();
        resultInput.ActivateInputField();
        resultInput.onEndEdit.AddListener(delegate { DisplayResultInsideHouse(Int32.Parse(resultInput.text)); });
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
        TextScript.current.FinishText(ObtainResult(),responseUser);
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

    // Increase the value of people's counter  from the rigth.
    public void IncreasePeopleCounterLeft()
    {
        peopleCounterLeft++;
    }

    // Decrease the value of people's counter  from the left.
    public void DecreasePeopleCounterLeft()
    {
        peopleCounterLeft--;
        Debug.Log("     DecreasePeopleCounteLeft:" + peopleCounterLeft);
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
        Debug.Log("     DecreasePeopleCounteRigth:"+ peopleCounterRigth);
    }

    // Return the result of people it will be inside the house.
    public int ObtainResult()
    {
        return finalResult;
    }

    // The house disappears at the end of the game.
    public void DisplayResultInsideHouse(int answer)
    {
        house.GetComponent<Animator>().speed = 1;
        house.GetComponent<Animator>().Play("Base Layer.MoveHouseUp", -1,0);
        //house.SetActive(false);
        //int p = rnd.Next(10);
        //TextScript.current.SetText("Juego Finalizado");
        responseUser = answer;
        Invoke("CallFinishText", 0.5f);
    }

    // Creates the object according to the passing value.
    public void CreateObject(string index)
    {
        switch (index)
        {
            case "Left":
                SpawnerEnd.current.CreateObjectEnd();
                break;
            case "Rigth":
                SpawnerStart.current.CreateObjectStart();
                break;
            default:
                break;
        }
        
    }
}
