using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void ChangeScene(int index)
    {
        PlayerPrefs.SetInt("IdPreviousScene", SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(index);
        PlayerPrefs.SetInt("IdNextScene", IdNextScene);
    }
}
