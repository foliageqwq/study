using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBoolBehavior : StateMachineBehaviour
{
    public string boolName;
    public bool updataOnState;
    public bool updataOnStateMachine;
    public bool valueOnEnter, valueOnExit;

    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    // 进入状态机中的任意状态时调用  
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updataOnState) 
        {
            animator.SetBool(boolName, valueOnEnter);
        }
    }

    // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    // 离开状态机中的任意状态时调用
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updataOnState) 
        {
            animator.SetBool(boolName, valueOnEnter);
        }
    }

    // OnStateMove is called before OnStateMove is called on any state inside this state machine
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateIK is called before OnStateIK is called on any state inside this state machine
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMachineEnter is called when entering a state machine via its Entry Node
    // 进入状态机时调用（通过入口节点）
    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        if(updataOnStateMachine)
        animator.SetBool(boolName, valueOnEnter);
    }

    // OnStateMachineExit is called when exiting a state machine via its Exit Node
    // 离开状态机时调用（通过出口节点）
    override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        if(updataOnStateMachine)
        animator.SetBool(boolName, valueOnExit);
    }
}
