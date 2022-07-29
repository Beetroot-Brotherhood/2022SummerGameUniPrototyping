using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GatherSlicedObjects : MonoBehaviour
{
    public static GatherSlicedObjects instance;

    void Awake () {
        if (instance != null) {
            Debug.LogError("There is more than one GatherSlicedObjects!");
        }
        else {
            instance = this;
        }
    }
    public Text scoreText;
    public int score;
    public GameObject player;
    public int radius;
    public LayerMask layerMask;
    List<SlicedCounter> movingParts = new List<SlicedCounter>();

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + score;
        if (OnSlicerInput.instance.onGatherSlicedParts) {
            Collider[] objectsToGather = Physics.OverlapSphere(player.transform.position , radius, layerMask.value, QueryTriggerInteraction.UseGlobal);
            for (int i = 0; i < objectsToGather.Length; i++) {
                SlicedCounter slicedCounter;
                if (objectsToGather[i].TryGetComponent<SlicedCounter>(out slicedCounter)) {
                    if (!movingParts.Contains(slicedCounter)) {
                        slicedCounter.StartMovingTowardsPlayer(player);
                        movingParts.Add(slicedCounter);
                    }
                }
            }
        }
        else {
            foreach (SlicedCounter movingPart in movingParts) {
                try {
                    movingPart.StopMovingTowardsPlayer();
                }
                catch (System.Exception) {
                }
            }
            movingParts = new List<SlicedCounter>();
        }
    }
}
