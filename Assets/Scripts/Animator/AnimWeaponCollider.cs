using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimWeaponCollider : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponentInParent<Weapon>().ActivateCollider();
    }
}
