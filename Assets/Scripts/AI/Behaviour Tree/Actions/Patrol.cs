using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class Patrol : ActionNode
{
    public Transform[] points;
    [HideInInspector]
    public int destPoint = 0;

    private int damping = 2;

    protected override void OnStart() {

        points = context.pathFollow.points;

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        context.agent.autoBraking = false;

        GotoNextPoint();
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {

        // Choose the next destination point when the agent gets
        // close to the current one.
        if (!context.agent.pathPending && context.agent.remainingDistance < 0.5f)
            GotoNextPoint();
        return State.Running;

    }

    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to look at the destination it is moving towards
        var lookPos = points[destPoint].position - context.agent.transform.position;
        lookPos.y = 0;

        var rotation = Quaternion.LookRotation(lookPos);
        context.agent.transform.rotation = Quaternion.Slerp(context.agent.transform.rotation, rotation, Time.deltaTime * damping);

        // Set the agent to go to the currently selected destination.
        context.agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }
}
