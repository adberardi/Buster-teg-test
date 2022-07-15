using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour
{
    public Text txt;
    public static TextScript current;

    private void Awake()
    {
        current = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //txt.text = "Probando";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText(string newWord)
    {
        txt.text = newWord;
    }
}
