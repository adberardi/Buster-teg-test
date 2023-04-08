﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatDivision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Trigger when end the animation.
    public void FinishAnimation()
    {
        string road = "";
        TimerStart.current.StopTimer();
        switch (DivisionController.current.responseUser)
        {
            case "TopIsland":
                road = "MoveBoatTop";
                break;
            case "MiddleIsland":
                road = "MoveBoatMiddle";
                break;
            case "BottomIsland":
                road = "MoveBoatBottom";
                break;
        }


        DivisionController.current.routeSelected = road;
        Debug.Log("->Termino animacion!!!! Isla seleccionada: " + road);
        DivisionController.current.CallFinishText();
    }

}