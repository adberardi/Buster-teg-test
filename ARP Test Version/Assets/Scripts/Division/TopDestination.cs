using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TopDestination : MonoBehaviour
{
    public static TopDestination current;
    public GameObject boat;
    public UnityEvent OnClick = new UnityEvent();

    private void Awake()
    {
        current = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        boat.GetComponent<Animator>().speed = 0;
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
                    DivisionController.current.responseUser = "TopIsland";
                    DivisionController.current.FlagIdTransition = "IdleToTop";
                    boat.GetComponent<Animator>().SetBool("IdleToTop", true);
                    boat.GetComponent<Animator>().Play("MoveBoatTop", -1, 0f);
                    boat.GetComponent<Animator>().speed = 1;
                    TimerStart.current.StopTimer();
                }

            }
        }

    }
}
