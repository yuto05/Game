using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPose : StateMachineBehaviour
{
    int hashRandom = Animator.StringToHash("Random");

    // OnStateMachineEnter is called when entering a state machine via its Entry Node
    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        //0～2の値をランダムで出す
        animator.SetInteger(hashRandom, Random.Range(0, 3));
    }
}
