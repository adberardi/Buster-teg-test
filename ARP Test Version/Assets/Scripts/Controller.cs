using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public bool startRigth = false;
    public bool startLeft = false;
    public static Controller controlCharacter;


    private void Awake()
    {
        controlCharacter = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateObject(string index)
    {
        switch (index)
        {
            case "Left":
                SpawnerEnd.current.CreateObjectEnd();
                controlCharacter.startLeft = false;
                controlCharacter.startRigth = true;
                break;
            case "Rigth":
                SpawnerStart.current.CreateObjectStart();
                controlCharacter.startLeft = true;
                controlCharacter.startRigth = false;
                break;
            default:
                break;
        }
        
    }
}
