using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimSinos;

public class Mooving : StateMachineBehaviour
{
    private Character char_controller;
    public string name_arrived; 

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        char_controller = animator.GetComponent<Character>();

        animator.SetBool("Arrived Hungry", false);
        animator.SetBool("Arrived Sleepy", false);
        animator.SetBool("Arrived Sad", false);
        animator.SetBool("Arrived Idle", false);

        if (animator.GetBool("Hungry") || animator.GetBool("Starving"))
        {
            char_controller.chooseTarget(char_controller.HungryTarget);
            name_arrived = "Arrived Hungry";
        }
        else if (animator.GetBool("Sleepy"))
        {
            char_controller.chooseTarget(char_controller.SleepyTarget);
            name_arrived = "Arrived Sleepy";
        }
        else if (animator.GetBool("Sad"))
        {
            char_controller.chooseTarget(char_controller.SadTarget);
            name_arrived = "Arrived Sad";
        }
        else if (!animator.GetBool("Needy"))
        {
            char_controller.chooseTarget(char_controller.IdleTarget);
            name_arrived = "Arrived Idle";
        }
        else
        {
            char_controller.chooseTarget(null);
            name_arrived = "";
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (char_controller.hasArrived())
        {
            animator.SetBool(name_arrived, true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
