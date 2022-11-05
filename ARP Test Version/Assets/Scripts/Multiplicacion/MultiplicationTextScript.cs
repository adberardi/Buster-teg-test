using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiplicationTextScript : MonoBehaviour
{
    Text txtField;
    public static MultiplicationTextScript current;

    private void Awake()
    {
       // current = this;
       // txtField = MultiplicationController.current.GetTextField();
    }

    // Start is called before the first frame update
    void Start()
    {
        current = this;
        txtField = MultiplicationController.current.GetTextField();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActivatedTextCounter()
    {
        txtField.gameObject.SetActive(true);
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
            txtField.text = newNum.ToString();
        }

    }

    // Disable the game TextField
    public void DeactivateText()
    {
        txtField.gameObject.SetActive(false);
    }

    // Assigns a specific text to the TextField when the game ends
    public void FinishText(string responseCorrect, string responseUser)
    {
        txtField.gameObject.SetActive(true);
        if (responseCorrect == responseUser)
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
