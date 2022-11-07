using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FinishAnimation()
    {
        string routeSelected = "";
        Debug.Log("->Termino animacion!!!!");
        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("MoveBoatTop"))
            routeSelected = "MoveBoatTop";
        else if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("MoveBoatMiddle"))
            routeSelected = "MoveBoatMiddle";
        else
            routeSelected = "MoveBoatBottom";
        MultiplicationController.current.TakeControl(routeSelected);
    }
}
