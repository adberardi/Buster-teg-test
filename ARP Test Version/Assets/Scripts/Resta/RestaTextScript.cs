using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestaTextScript : MonoBehaviour
{
    Text txtField;
    public static RestaTextScript current;

    private void Awake()
    {
        current = this;
        //txtField = RestaController.controlCharacter.GetTextField();
    }

    // Start is called before the first frame update
    void Start()
    {
        txtField = RestaController.controlCharacter.GetTextField();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Assigns a specific text to the TextField of the set.
    public void SetText(int newNum)
    {
        Debug.Log("Contador para iniciar juego:" + newNum);
        if (newNum == 0)
        {
            txtField.text = "EMPIEZA!!";
            txtField.color = Color.yellow;
        }
        else
        {
            //txtField.text = string.Format("{0}", newNum);
            txtField.text = newNum.ToString();
        }

    }

    // Assigns a specific text to the TextField of the set, when the counter to down the house is active
    public void SetTextCounterDownHouse(int newNum)
    {
        
        if (newNum == 0)
        {
            txtField.text = "Bajando Casa";
            Invoke("DeactivateText", 1f);
        }
        else
        {
            //txtField.text = string.Format("{0}", newNum);
            txtField.text = "Bajar casa en: "+newNum.ToString();
        }

    }

    // Disable the game TextField
    private void DeactivateText()
    {
        txtField.gameObject.SetActive(false);
    }

    // Assigns a specific text to the TextField when the game ends
    public void FinishText(int peopleInHouse, int responseUser)
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
