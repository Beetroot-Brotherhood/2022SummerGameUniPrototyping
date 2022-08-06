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

    public GameObject pressEUiText;

    // Start is called before the first frame update
    void Start()
    {
        DollHouseGeneral.UpdateUIBodyPartImage();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width/2, Screen.height/2));

        if (Physics.Raycast(ray, out RaycastHit hit, raycastRange, layerMask, QueryTriggerInteraction.UseGlobal)) {
            
            PartInfo partInfo = hit.collider.gameObject.GetComponent<PartInfo>();
            pressEUiText.SetActive(true);
            //Functionality for possible input "Press E" or "Press Q" or Both
            if (OnPlayerInput.instance.onInteract) {
                DollHouseGeneral.SwitchBodyPart(hit.collider.gameObject, partInfo.partInfoSO.bodyPartPos, dropPosition);
                DollHouseGeneral.UpdateUIBodyPartImage();
                OnPlayerInput.instance.onInteract = false;
            }           
        }
        else {
            pressEUiText.SetActive(false);
        }
        OnPlayerInput.instance.onInteract = false;
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
