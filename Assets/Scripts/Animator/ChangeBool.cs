using UnityEngine;
using System.Collections;

public class ChangeBool : StateMachineBehaviour {

    public string boolName;
    public bool status;
    public bool reverseOnExit;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        animator.SetBool(boolName, status);

	}
	
	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	
        if(reverseOnExit)
        {
            animator.SetBool(boolName, !status);
        }
	}

}
