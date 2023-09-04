using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ARProject.Tutorial;

public class TutorialController : MonoBehaviour
{
    Tutorial demo = new Tutorial();
    public Text TextArea;

    // Start is called before the first frame update
    void Start()
    {
        TextArea.text = demo.Description;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Carga el juego seleccionado por el usuario.
    public void BtnContinue()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("IdNextScene"));
    }

    //Carga la escena anterior (Pueden ser escenas como; Menu de juego de Suma, Menu de Resta o Home)
    public void BtnBack()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("IdPrevious"));
    }
}
