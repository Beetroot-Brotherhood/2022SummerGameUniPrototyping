using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PartsUIReference : MonoBehaviour
{

    public GameObject bodyPartsUIParent;

    public BodyParts[] bodyPartsUI;

    [ContextMenu("OnValidate")]
    void OnValidate() {
        DollHouseGeneral.AssignBodyParts(bodyPartsUI, bodyPartsUIParent);
    }
}
