using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BodyParts {
    public BodyPartNames tag;
    public GameObject gameObject;
}

public class PartsReference : MonoBehaviour
{
    public static PartsReference instance;

    void Awake () {
        if (instance != null) {
            Debug.LogError("PartsReference has two instances!");
        }
        else {
            instance = this;
        }
    }

    public GameObject spine;

    public BodyParts[] bodyParts;

    public void AssignBodyParts() {
        for (int i = 0; i < bodyParts.Length; i++) {
            bodyParts[i].gameObject = Krezme.QualityOfLife.FindGameObjectInChildrenWithTag(spine.transform, bodyParts[i].tag.ToString());
        }
    }

    [ContextMenu("OnValidate")]
    public void OnValidate() {
        AssignBodyParts();
    }
    
}

public enum BodyPartNames {
    None,
    HeadPartPos,
    TorsoPartPos,
    LeftArmPartPos,
    RightArmPartPos,
    LeftForearmPartPos,
    RightForearmPartPos,
    LeftHandPartPos,
    RightHandPartPos,
    LeftThighPartPos,
    RightThighPartPos,
    LeftLegPartPos,
    RightLegPartPos
}