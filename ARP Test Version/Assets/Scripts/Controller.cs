using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Controller : MonoBehaviour
{
    private bool onGoingGame = true;
    public bool startRigth;
    public bool startLeft;
    private int peopleCounterLeft;
    private int peopleCounterRigth;
    int finalResult;
    int[,] operations = { { 2, 4 }, { 3, 4 } };
    System.Random rnd;
    int op;
    public GameObject house;
    public static Controller controlCharacter;



    private void Awake()
    {
        controlCharacter = this;
        startRigth = true;
        startLeft = false;
        rnd = new System.Random();
        op = rnd.Next(operations.Length - 1);
        peopleCounterLeft = operations[op,0];
        peopleCounterRigth = operations[op,1];
        finalResult = peopleCounterRigth - peopleCounterLeft;
        house.GetComponent<Animator>().speed = 0;
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
        house.GetComponent<Animator>().speed = 1;
        house.GetComponent<Animator>().Play("Base Layer.MoveHouseUp", -1,0);
        //house.SetActive(false);
        //int p = rnd.Next(10);
        Debug.Log("Valor  array multidimensional:" + operations[0,1]+" Numero random"+ rnd.Next(10));
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
