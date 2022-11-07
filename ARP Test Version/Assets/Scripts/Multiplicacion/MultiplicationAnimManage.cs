using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplicationAnimManage : StateMachineBehaviour
{
    public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        Debug.Log(" -> Saliendo");
    }


    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        Debug.Log("OnStateExit");
        animator.SetBool("IdleToTop", false);
        if (stateInfo.IsName("MoveBoatMiddle"))
            Debug.Log("|||||||||||||| Voy a la isla del medio");
        //if (MultiplicationController.current.repeats == 3)
            //animator.SetBool("TopToExit", true);
    }
}
