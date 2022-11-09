﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IslandTop : MonoBehaviour
{
    public static IslandTop current;
    public GameObject boat;
    public UnityEvent OnClick = new UnityEvent();

    private void Awake()
    {
        current = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        boat.GetComponent<Animator>().speed = 0;
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
                    //boat.GetComponent<Animator>().SetBool("TopToIdle", false);
                    boat.GetComponent<Animator>().SetBool("IdleToTop", true);
                    boat.GetComponent<Animator>().speed = 1;
                }

            }
        }

    }
}
