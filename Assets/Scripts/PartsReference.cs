using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BodyParts {
    public string tag;
    public GameObject gameObject;
}

public class PartsReference : MonoBehaviour
{
    public GameObject spine;

    public BodyParts[] bodyParts;

    public void AssignBodyParts() {
        for (int i = 0; i < bodyParts.Length; i++) {
            bodyParts[i].gameObject = Krezme.QualityOfLife.FindGameObjectInChildrenWithTag(spine.transform, bodyParts[i].tag);
        }
    }

    [ContextMenu("OnValidate")]
    public void OnValidate() {
        AssignBodyParts();
    }
    
}

namespace Krezme {
    public static class QualityOfLife {
        public static GameObject FindGameObjectInChildrenWithTag(Transform parent, string tag) {
            if (parent.childCount > 0) {
                for (int i = parent.childCount-1; i >= 0; i--) {
                    GameObject gO = FindGameObjectInChildrenWithTag(parent.GetChild(i), tag);
                    if (gO != null) {
                        return gO;
                    }
                }
                if (CompareTags(parent.tag, tag)) {
                    return parent.gameObject;
                }
            }
            else {
                if (CompareTags(parent.tag, tag)) {
                    return parent.gameObject;
                }
            }
            return null;
        }

        public static bool CompareTags(string firstTag, string secondTag) {
            return firstTag == secondTag ? true : false;
        }
    }
}