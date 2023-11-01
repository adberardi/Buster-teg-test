using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SumaTextScript : MonoBehaviour
{
    Text txtField;
    public static SumaTextScript current;

    private void Awake()
    {
        current = this;
        txtField = SumaController.controlCharacter.GetTextField();
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

    // Assigns a specific text to the TextField of the set.
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
            txtField.text = "El juego empieza en:\n" + newNum.ToString();
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
