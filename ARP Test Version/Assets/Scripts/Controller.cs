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
    public static Controller controlCharacter;



    private void Awake()
    {
        controlCharacter = this;
        startRigth = true;
        startLeft = false;
        peopleCounterLeft = 2;
        peopleCounterRigth = 2;
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
    }

    // Return actual value of status game
    public bool GetOnGoingGame()
    {
        return onGoingGame;
    }

    public int IncreasePeopleCounterLeft()
    {
        peopleCounterLeft++;
        return peopleCounterLeft;
    }

    public int DecreasePeopleCounterLeft()
    {
        peopleCounterLeft--;
        return peopleCounterLeft;
    }

    public int IncreasePeopleCounterRigth()
    {
        peopleCounterRigth++;
        return peopleCounterRigth;
    }

    public int DecreasePeopleCounteRigth()
    {
        peopleCounterRigth--;
        return peopleCounterRigth;
    }

    public int GetPeoplecounterLeft()
    {
        return peopleCounterLeft;
    }

    public int GetPeoplecounterRigth()
    {
        return peopleCounterRigth;
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
