using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ControllerMultiplicacion : MonoBehaviour
{
    public static ControllerMultiplicacion current;
    public GameObject boat;
    public UnityEvent OnClick = new UnityEvent();


    private void Awake()
    {
        current = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
