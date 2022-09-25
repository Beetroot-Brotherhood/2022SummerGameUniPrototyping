using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLockOn : MonoBehaviour
{
    public float viewRadius;

    [Range(0, 360)]
    public float viewAngle;

    public LayerMask targetMask;

    public LayerMask obstacleMask;

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();

    public bool targetVisible = false;

    void Start()
    {
        StartCoroutine("FindTargetsWithDelay", 0.2f);
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets()
    {
        for (int i = 0; i < visibleTargets.Count; i++)
        {
            if (visibleTargets[i].TryGetComponent<YbotTestController2>(out YbotTestController2 ybotTestController2))
            {
                ybotTestController2.targetedUI.SetActive(false);
            }
        }


        visibleTargets.Clear(); // clears list so there are no duplicate items stored in it
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform; // gets the transform for any target the enemy is looking at
            Vector3 dirToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position); // gets the distance between the target and enemy

                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask)) // if the raycast doesn't hit an obstacle blocking the target
                {
                    if (target.TryGetComponent<YbotTestController2>(out YbotTestController2 ybotTestController2)) {
                        ybotTestController2.targetedUI.SetActive(true);
                        visibleTargets.Add(target); // adds the target to the visible targets list
                        targetVisible = true;
                    }
                }
                else
                {
                    targetVisible = false;
                }
            }
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
