using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextScript : MonoBehaviour
{
    public static TextScript txt;
    public GUIText t;

    private void Awake()
    {
        txt = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        t.text = "Contador";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
