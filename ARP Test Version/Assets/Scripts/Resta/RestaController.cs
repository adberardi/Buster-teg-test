using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using MongoDB.Bson;
using ARProject.GamesPlayed;

public class RestaController : MonoBehaviour
{
    private readonly int ScoreGame = 50;
    public bool onGoingGame { get; set; }
    public bool endRoute { get; set; }
    public bool startLeft;
    public int peopleCounterRigth { get; set; }
    public int peopleCounterLeaving { get; set; }
    int finalResult;
    // {Numero limite generador personajes , Factor Velocidad Personaje}
    int[,] difficulty = { { 1, 5 }, { 2, 8 }, { 3, 10 } };
    System.Random rnd;
    int op;
    public float Speed { get; set; }
    int counterToStart;
    int responseUser;
    bool showPeople { get; set; }
    int counterDownHouse { get; set; }
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
    public static RestaController controlCharacter;
    private GamesPlayed Gp;
    public GameObject PanelIntro;

    private void Awake()
    {
        endRoute = false;
        onGoingGame = false;

        controlCharacter = this;
        startLeft = true;
        responseUser = 0;
        rnd = new System.Random();
        op = 0;
        Speed = difficulty[op, 1] * 0.1f;
        peopleCounterRigth = rnd.Next(10);
        peopleCounterLeaving = rnd.Next(1, peopleCounterRigth);
        finalResult = peopleCounterRigth - peopleCounterLeaving;
        house.GetComponent<Animator>().speed = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        //soundGame = GetComponent<AudioSource>();
        counterToStart = 3;
        showPeople = true;
        string op = PlayerPrefs.GetString("LevelSchool");
        switch (op)
        {
            case "1erGrado":
                counterDownHouse = 5;
                break;
            case "2doGrado":
                counterDownHouse = 4;
                break;
            case "3erGrado":
                counterDownHouse = 3;
                break;
            default:
                counterDownHouse = 0;
                break;
        }
        
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
            PanelIntro.SetActive(true);
            MoveHouseToUp();
            ShowPeopleResult();
            controlCharacter.UpdateCounterToDownHouse();
            onGoingGame = true;
            soundGame = GetComponent<AudioSource>();
            soundGame.Play();
            showPeople = false;
            //RestaSpawnerStart.current.CreateObjectStart();
        }
    }

    // Shows the counter to down the house and start to create the characters.
    public void ShowCounterToDownHouse()
    {

        if (counterDownHouse >= 0)
        {
            
            Invoke("UpdateCounterToDownHouse", 1.0f);
        }
        else
        {
            showPeople = false;
            PanelIntro.SetActive(false);
            controlCharacter.MoveHouseToDown();
            //RestaSpawnerStart.current.CreateObjectStart();
        }
    }

    // Counter to down the house
    public void UpdateCounterToDownHouse()
    {
        RestaTextScript.current.SetTextCounterDownHouse(counterDownHouse);
        counterDownHouse--;
        Invoke("ShowCounterToDownHouse", 1.0f);
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
        
        RestaTextScript.current.SetText(counterToStart);
        counterToStart--;
        Invoke("ShowCounterToStartGame", 1.0f);
    }

    public void ShowPeopleResult()
    {
        Debug.Log(" --------> ShowPeopleResult");
        //float axisX = GetHouse().transform.localPosition.x;
        /* float axisX = GetHouse().transform.localScale.x;
         float axisZ = GetHouse().transform.localScale.z;
             //int valuef = ObtainResult();
             int valuef = peopleCounterRigth;
             int count = 0;
             axisX = (axisX + 0.0200f) / 2;
             axisZ = (axisZ - 0.0200f) / 2;
             for (float x = axisX; x > 0; x = x - 0.0100f)
             {
                 for (float z = axisZ; z > -axisZ; z = z - 0.0100f)
                 {
                     if (count < valuef)
                     {
                         RestaSpawnerResult.current.CreateObjectResult(x * 10, z * 10);
                         count++;
                     }
                 }
             }*/
        int valuef = peopleCounterRigth;
        string nameActivity = "SpawnerResult_";
        for (int i = 10; i > valuef; i--)
        {
            string characterToEnable = nameActivity + i.ToString();
            GameObject character = GameObject.Find(characterToEnable);
            Debug.Log("RestaController - ShowPeopleResult: Voy por aqui");
            //GameObject character = Resources.Load
            character.SetActive(false);
        }
    }

    // show the final result
    public void ShowResult(float axisX, float axisZ)
    {
        if (onGoingGame == false)
        {
            int valuef = ObtainResult();
            int count = 0;
            axisX = (axisX + 0.0200f) / 2;
            axisZ = (axisZ - 0.0200f) / 2;
            for (float x = axisX; x > 0; x = x - 0.0100f)
            {
                for (float z = axisZ; z > -axisZ; z = z - 0.0100f)
                {
                    if (count < valuef)
                    {
                        RestaSpawnerResult.current.CreateObjectResult(x * 10, z * 10);
                        count++;
                    }
                }
            }


            ActivateInputResult();
        }
    }


    public void ShowResult()
    {
        Debug.Log("RestaController - ShowResult: 1");
        if (onGoingGame == false)
        {
            Debug.Log("RestaController - ShowResult: 2");
            int valuef = ObtainResult();
            string nameActivity = "SpawnerResult_";
            Debug.Log("RestaController - ShowResult: 3");
            for (int i = 10; i > valuef; i--)
            {
                Debug.Log("::::ShowResult = Dentro del Loop");
                string characterToEnable = nameActivity + i.ToString();
                Debug.Log("::::ShowResult = " + characterToEnable+" valuef: "+ valuef);
                GameObject character = GameObject.Find("/ARCamera/ImageTarget/SpawnersResults/" + characterToEnable);
                //GameObject character = Resources.Load
                character.SetActive(false);
                Debug.Log("::::ShowResult = character.SetActive(false);");
            }
            Debug.Log("RestaController - ShowResult: 4");
            ActivateInputResult();
            Debug.Log("RestaController - ShowResult: 5");
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
        RestaTextScript.current.FinishText(ObtainResult(), responseUser);
        if (ObtainResult() == responseUser)
        {
            // User Win
            GamesPlayed newGame = new GamesPlayed();
            newGame.User = ObjectId.Parse(PlayerPrefs.GetString("IDUser"));
            newGame.Group = ObjectId.Parse(PlayerPrefs.GetString("IDGroup"));
            newGame.DayPlayed = DateTime.Now.ToShortTimeString();
            newGame.FinalTimer = "00:00:00";
            //Game's id in specific.
            newGame.Game = ObjectId.Parse("6503c38c6bf01ab29fe956ea");
            effectsToWinner.SetActive(true);
            UpdateSound(soundWinner);
            if (!PlayerPrefs.GetInt("RewardActivity").Equals(""))
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
    public void IncreasePeopleCounterLeft()
    {
        peopleCounterRigth++;
    }

    // Decrease the value of people's counter  from the rigth.
    public void DecreasePeopleCounteLeft()
    {
        peopleCounterLeaving--;
        Debug.Log("     DecreasePeopleCounteLeft:" + peopleCounterLeaving);
    }

    // Return the result of people it will be inside the house.
    public int ObtainResult()
    {
        return finalResult;
    }

    // The house disappears at the end of the game.
    public void DisplayResultInsideHouse(String answer)
    {
        Debug.Log("----> DisplayResultInsideHouse <----");
        MoveHouseToUp();
        responseUser = Int32.Parse(answer);
        Invoke("CallFinishText", 0.5f);
    }

    // The house move to the plane of the game.
    public void MoveHouseToDown()
    {
        /*
        house.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animation/RestaMoveDown");
        house.GetComponent<Animator>().speed = 1;
        house.GetComponent<Animator>().Play("Base Layer.MoveHouseDown", -1, 0);
        */
        house.GetComponent<Animator>().SetBool("MoveHouseDown", true);
        house.GetComponent<Animator>().SetBool("MoveHouseUp", false);
        Invoke("CreateObject", 2.0f);
    }

    public void MoveHouseToUp()
    {
        /*
        house.GetComponent<Animator>().speed = 1;
        house.GetComponent<Animator>().Play("Base Layer.MoveHouseUp", -1, 0.0f);
        */
        house.GetComponent<Animator>().SetBool("MoveHouseDown", false);
        house.GetComponent<Animator>().SetBool("MoveHouseUp", true);
        house.GetComponent<Animator>().speed = 1;
        house.GetComponent<Animator>().Play("Base Layer.MoveHouseUp", -1, 0.0f);
    }

    public static void ResetPeopleInsideHouse()
    {
        int totalinside = controlCharacter.newParent.childCount;
        Transform aux;
        for(int i = 0; i < totalinside; i++)
        {
            aux = controlCharacter.newParent.transform.GetChild(i);
            Destroy(aux.gameObject);
        }
    }

    // Creates the object according to the passing value.
    public void CreateObject()
    {
        ResetPeopleInsideHouse();
        RestaSpawnerStart.current.CreateObjectStart();
    }
}
