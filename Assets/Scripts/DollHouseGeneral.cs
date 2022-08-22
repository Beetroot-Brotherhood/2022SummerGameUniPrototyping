using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DollHouseGeneral
{
    public static void AssignBodyParts(BodyParts[] bodyParts, GameObject parent) {
        if (parent != null) {
            for (int i = 0; i < bodyParts.Length; i++) {
                bodyParts[i].gameObject = Krezme.QualityOfLife.FindGameObjectInChildrenWithTag(parent.transform, bodyParts[i].tag.ToString());
            }
        }
    }

    public static void SwitchBodyPart(GameObject bodyPartToSwitch, BodyPartNames replacementPartType, GameObject dropPosition) {
        for (int i = 0; i < BodyPartsManager.instance.partsReference.bodyParts.Length; i++){
            if (BodyPartsManager.instance.partsReference.bodyParts[i].tag == replacementPartType) {
                GameObject oldPart = BodyPartsManager.instance.partsReference.bodyParts[i].gameObject.transform.GetChild(0).gameObject;
                Collider[] colliders = bodyPartToSwitch.GetComponents<Collider>();
                foreach (Collider collider in colliders) {
                    collider.enabled = false;
                }
                bodyPartToSwitch.GetComponent<Rigidbody>().useGravity = false;
                bodyPartToSwitch.GetComponent<Rigidbody>().isKinematic = true;
                bodyPartToSwitch.transform.parent = oldPart.transform.parent;
                bodyPartToSwitch.transform.localPosition = Vector3.zero;
                bodyPartToSwitch.transform.localRotation = Quaternion.Euler(0,0,0);
                
                oldPart.transform.parent = null;
                oldPart.transform.position = dropPosition.transform.position;
                Collider[] oldColliders = oldPart.GetComponents<Collider>();
                foreach (Collider collider in oldColliders) {
                    collider.enabled = true;
                }
                oldPart.GetComponent<Rigidbody>().useGravity = true;
                oldPart.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }

    public static void UpdateUIBodyPartImage() {
        foreach (BodyParts bodyPartsUI in BodyPartsManager.instance.partsUIReference.bodyPartsUI) {
            foreach (BodyParts bodyParts in BodyPartsManager.instance.partsReference.bodyParts) {
                if (bodyPartsUI.tag == bodyParts.tag) {
                    bodyPartsUI.gameObject.GetComponent<UnityEngine.UI.Image>().color = bodyParts.gameObject.transform.GetChild(0).GetComponent<PartInfo>().meshRenderer.materials[0].color;
                }
            }
        }
    }
}
