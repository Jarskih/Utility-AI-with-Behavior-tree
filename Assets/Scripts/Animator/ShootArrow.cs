using System.Collections;
using System.Collections.Generic;
using AI;
using UnityEngine;

public class ShootArrow : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponentInParent<Weapon>().RangedAttack();
    }
}
