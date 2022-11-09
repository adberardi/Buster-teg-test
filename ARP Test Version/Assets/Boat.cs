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

    // Trigger when end the animation.
    public void FinishAnimation()
    {
        string routeSelected = "";
        Debug.Log("->Termino animacion!!!!");
        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("MoveBoatTop"))
        {
            routeSelected = "MoveBoatTop";
           /* if (MultiplicationController.current.ValidateAttempts())
            {
                GetComponent<Animator>().SetBool("TopToIdle", true);
                GetComponent<Animator>().SetBool("IdleToTop", false);
            } */
                
        }
            
        else if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("MoveBoatMiddle"))
            routeSelected = "MoveBoatMiddle";
        else
            routeSelected = "MoveBoatBottom";
        MultiplicationController.current.TakeControl(routeSelected);
    }
}
