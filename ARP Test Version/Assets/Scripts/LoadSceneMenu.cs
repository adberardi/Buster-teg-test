using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneMenu : MonoBehaviour
{
   

    // Start is called before the first frame update
    void Start()
    {
        
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
        if (MultiplicationController.current.repeats <= 3)
        {
            Debug.Log("Reiniciando Juego> " + MultiplicationController.current.repeats.ToString());
            MultiplicationController.current.repeats = MultiplicationController.current.repeats + 1;
            //boat.transform.localPosition = new Vector3(-0.274f, 0.01f, 0f);
            //SceneManager.LoadScene(4);
            //MultiplicationController.current.btnRestart.gameObject.SetActive(false);
            GameObject aux = MultiplicationController.current.GetBoat();
            aux.GetComponent<Animator>().SetBool("TopToIdle", true);
            MultiplicationController.current.RestartGame();
        }

    }

}
