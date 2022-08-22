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

    public GameObject spine;

    public BodyParts[] bodyParts;

    [ContextMenu("OnValidate")]
    public void OnValidate() {
        DollHouseGeneral.AssignBodyParts(bodyParts, spine);
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