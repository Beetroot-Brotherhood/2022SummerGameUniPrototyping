using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class ShootAtPlayer : ActionNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        context.combatManager.primaryAttack.Shoot(SceneData.instance.player.transform.position + new Vector3(0, 1, 0));
        return State.Success;
    }
}
