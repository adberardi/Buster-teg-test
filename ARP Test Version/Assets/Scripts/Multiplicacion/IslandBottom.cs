﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IslandBottom : MonoBehaviour
{
    public static IslandBottom current;
    public GameObject boat;
    public UnityEvent OnClick = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit Hit;
        if (MultiplicationController.current.onGoingGame)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(ray, out Hit) && Hit.collider.gameObject == gameObject)
                {
                    Debug.Log("Presionando sobre isla");
                    boat.GetComponent<Animator>().SetBool("IdleToBottom", true);
                    boat.GetComponent<Animator>().speed = 1;
                    //boat.GetComponent<Animator>().Play("Base Layer.MoveBoatTop");
                }

            }
        }
    }
}
