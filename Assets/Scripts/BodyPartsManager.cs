using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PartsReference), typeof(PartsUIReference))]
public class BodyPartsManager : MonoBehaviour
{
    public static BodyPartsManager instance;

    void Awake() {
        if (instance != null) {
            Debug.LogError("");
        }
        else {
            instance = this;
        }
    }

    public PartsReference partsReference;
    public PartsUIReference partsUIReference;

    public float raycastRange;
    public LayerMask layerMask;

    public GameObject dropPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width/2, Screen.height/2));

        if (Physics.Raycast(ray, out RaycastHit hit, raycastRange, layerMask, QueryTriggerInteraction.UseGlobal)) {
            
            PartInfo partInfo = hit.collider.gameObject.GetComponent<PartInfo>();
            //Functionality for possible input "Press E" or "Press Q" or Both

            if (partInfo.partInfoSO.bodyPartPos == BodyPartNames.HeadPartPos || partInfo.partInfoSO.bodyPartPos == BodyPartNames.TorsoPartPos) {
                if (OnPlayerInput.instance.onEquipRight) {
                    DollHouseGeneral.SwitchBodyPart(hit.collider.gameObject, partInfo.partInfoSO.bodyPartPos, dropPosition);
                    OnPlayerInput.instance.onEquipRight = false;
                }
            }else {
                if (OnPlayerInput.instance.onEquipRight) {
                    DollHouseGeneral.SwitchBodyPart(hit.collider.gameObject, partInfo.partInfoSO.bodyPartPos, dropPosition);
                    OnPlayerInput.instance.onEquipRight = false;
                }
                if (OnPlayerInput.instance.onEquipLeft) {
                    DollHouseGeneral.SwitchBodyPart(hit.collider.gameObject, partInfo.partInfoSO.bodyPartPos, dropPosition);
                    OnPlayerInput.instance.onEquipLeft = false;
                }
            }
        }
    }

    #if UNITY_EDITOR
    void Reset() {
        partsReference = GetComponent<PartsReference>();
        partsUIReference = GetComponent<PartsUIReference>();
    }
    void OnValidate () {
        partsReference = GetComponent<PartsReference>();
        partsUIReference = GetComponent<PartsUIReference>();
    }
    #endif
}
