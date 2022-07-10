using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private bool onGoingGame = true;
    public bool startRigth;
    public bool startLeft;
    private int peopleCounterLeft;
    private int peopleCounterRigth;
    int finalResult;
    public GameObject house;
    public static Controller controlCharacter;



    private void Awake()
    {
        controlCharacter = this;
        startRigth = true;
        startLeft = false;
        peopleCounterLeft = 1;
        peopleCounterRigth = 3;
        finalResult = peopleCounterRigth - peopleCounterLeft;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Update value
    public void SetOnGoingGame(bool newValue)
    {
        onGoingGame = newValue;
        if (onGoingGame == false)
        {
            int valuef = ObtainResult();
            Debug.Log("Resultado final: " + valuef);
            int count = 0;
            while(count < valuef)
            {
                SpawnerResult.current.CreateObjectResult(0.1f * count);
                count++;
            }
            DisplayResultInsideHouse();
        }
    }

    // Return actual value of status game
    public bool GetOnGoingGame()
    {
        return onGoingGame;
    }

    // Increase the value of people's counter  from the rigth.
    public int IncreasePeopleCounterLeft()
    {
        peopleCounterLeft++;
        return peopleCounterLeft;
    }

    // Decrease the value of people's counter  from the left.
    public int DecreasePeopleCounterLeft()
    {
        peopleCounterLeft--;
        return peopleCounterLeft;
    }

    // Increase the value of people's counter  from the rigth.
    public int IncreasePeopleCounterRigth()
    {
        peopleCounterRigth++;
        return peopleCounterRigth;
    }

    // Decrease the value of people's counter  from the rigth.
    public int DecreasePeopleCounteRigth()
    {
        peopleCounterRigth--;
        return peopleCounterRigth;
    }

    // Return the value of people's counter from the left
    public int GetPeoplecounterLeft()
    {
        return peopleCounterLeft;
    }

    // Return the value of people's counter from the rigth
    public int GetPeoplecounterRigth()
    {
        return peopleCounterRigth;
    }

    // Return the result of people it will be inside the house.
    public int ObtainResult()
    {
        return finalResult;
    }

    // The house disappears at the end of the game.
    public void DisplayResultInsideHouse()
    {
        house.SetActive(false);
    }

    // Creates the object according to the passing value.
    public void CreateObject(string index)
    {
        switch (index)
        {
            case "Left":
                SpawnerEnd.current.CreateObjectEnd();
                controlCharacter.startLeft = false;
                controlCharacter.startRigth = true;
                break;
            case "Rigth":
                SpawnerStart.current.CreateObjectStart();
                controlCharacter.startLeft = true;
                controlCharacter.startRigth = false;
                break;
            default:
                break;
        }
        
    }
}
