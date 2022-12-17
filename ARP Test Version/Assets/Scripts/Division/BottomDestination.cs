using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class BottomDestination : MonoBehaviour
{
    public static BottomDestination current;
    public GameObject boat;
    public UnityEvent OnClick = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit Hit;
        if (DivisionController.current.onGoingGame)
        {
            if (Input.GetMouseButtonDown(0) && DivisionController.current.AllowAnswers)
            {
                if (Physics.Raycast(ray, out Hit) && Hit.collider.gameObject == gameObject)
                {
                    Debug.Log("Presionando sobre isla");
                    DivisionController.current.FlagIdTransition = "IdleToBottom";
                    boat.GetComponent<Animator>().SetBool("IdleToBottom", true);
                    boat.GetComponent<Animator>().Play("MoveBoatBottom", -1, 0f);
                    boat.GetComponent<Animator>().speed = 1;
                    DivisionController.current.responseUser = "BottomIsland";
                    TimerStart.current.StopTimer();
                }

            }
        }
    }
}
