using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DivisionTextScript : MonoBehaviour
{
    Text txtField;
    public static DivisionTextScript current;

    private void Awake()
    {
        // current = this;
        // txtField = DivisionController.current.GetTextField();
    }

    // Start is called before the first frame update
    void Start()
    {
        current = this;
        txtField = DivisionController.current.GetTextField();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActivatedTextCounter()
    {
        ChangeTextColor("green");
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


    public void ChangeTextColor(string op)
    {
        switch (op)
        {
            case "green":
                txtField.color = Color.green;
                break;
            case "red":
                txtField.color = Color.red;
                break;
        }
    }

    // Assigns a specific text to the TextField when the game ends
    public void FinishText(string responseCorrect, string responseUser)
    {
        txtField.gameObject.SetActive(true);
        Debug.Log("RESULTADO: responseCorrect:" + responseCorrect + " | " + " responseUser:" + responseUser);
        if (responseCorrect == responseUser)
        {
            txtField.text = "Respuesta correcta!";
            ChangeTextColor("green");

        }
        else
        {
            txtField.text = "Respuesta erronea";
            ChangeTextColor("red");

        }

    }
}
