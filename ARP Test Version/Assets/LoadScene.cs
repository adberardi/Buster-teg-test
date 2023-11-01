﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.Text;

public class LoadScene : MonoBehaviour
{
    public int IdNextScene { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Valida si la palabra contiene acentos, en caso de que una letra lo tenga la remueve y la sustituye por la letra sin acento.
    public string RemoveAccents(string textToPrepare)
    {
        string wordsWithAccents = "áéíóúñ";
        string textWithoutAccent = Regex.Replace(wordsWithAccents.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9 ]+", "");
        return textWithoutAccent;
    }

    public void ChangeScene(int index)
    {
        PlayerPrefs.SetInt("IdPreviousScene", SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(index);
        PlayerPrefs.SetInt("IdNextScene", IdNextScene);

        //Invocar la funcion RemoveAccents, y se le pasa como parametro el texto a validar.
        string clickedButtonNameGame = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text;
        PlayerPrefs.SetString("NameGame", clickedButtonNameGame);
    }

    public void BtnDivision(int index)
    {
        if (ValidateLevelSchoolWithGame() == 3)
        {
            PlayerPrefs.SetInt("IdNextScene", IdNextScene);
            ChangeScene(index);
        }
        else
        {
            Debug.Log("Este juego solo esta disponible a partir de 3er Grado");
        }

    }

    public void BtnMultiplication(int index)
    {
        if(ValidateLevelSchoolWithGame() >= 2)
        {
            PlayerPrefs.SetInt("IdNextScene", IdNextScene);
            ChangeScene(index);
        }
        else
        {
            Debug.Log("Este juego solo esta disponible a partir de 2do Grado");
        }

    }


    //Validate if the user can play the game.
    public int ValidateLevelSchoolWithGame()
    {
        string op = PlayerPrefs.GetString("LevelSchool");
        switch (op)
        {
            case "1erGrado":
                return 1;
            case "2doGrado":
                return 2;
            case "3erGrado":
                return 3;
            default:
                return 0;
        }
    }


}
