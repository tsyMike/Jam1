using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class StopNavmeshOnAnimation : StateMachineBehaviour
{   

    //Stops Navmesh Agent Movement while the animation is Playing!
    private Vector3 lastAgentVelocity;
    private NavMeshPath lastAgentPath;
    private NavMeshAgent agent;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent=animator.GetComponent<NavMeshAgent>();
        lastAgentVelocity = agent.velocity;
        lastAgentPath = agent.path;
        agent.velocity = Vector3.zero;
        agent.ResetPath();
    }

    

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.velocity = lastAgentVelocity;
        agent.SetPath(lastAgentPath);
    }


}
