﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour
{
    public Text txtField;
    public static TextScript current;

    private void Awake()
    {
        current = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //txtField.text = "Probando";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText(int newNum)
    {
        Debug.Log("Contador para iniciar juego:" + newNum);
        if (newNum == 0)
        {
            txtField.text = "EMPIEZA!!";
            Invoke("DeactivateText", 1f);
        }
        else
        {
            //txtField.text = string.Format("{0}", newNum);
            txtField.text = newNum.ToString();
        }

    }

    private void DeactivateText()
    {
        txtField.gameObject.SetActive(false);
    }

    public void FinishText(int peopleInHouse,int responseUser)
    {
        txtField.gameObject.SetActive(true);
        if (peopleInHouse == responseUser)
        {
            txtField.text = "Respuesta correcta!";
            txtField.color = Color.green;
        }
        else
        {
            txtField.text = "Respuesta erronea";
            txtField.color = Color.red;
        }

    }
}
