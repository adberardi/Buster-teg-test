using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneMenu : MonoBehaviour
{
    public int repeats { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        repeats = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void restartGame()
    {
        if (repeats < 3)
        {
            Debug.Log("Reiniciando Juego> " + repeats.ToString());
            repeats = repeats + 1;
            //boat.transform.localPosition = new Vector3(-0.274f, 0.01f, 0f);
            //SceneManager.LoadScene(4);
            MultiplicationController.current.btnRestart.gameObject.SetActive(false);
        }

    }

}
