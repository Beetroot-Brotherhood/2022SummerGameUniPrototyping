using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class SceneData : MonoBehaviour
{
    public static SceneData instance;

    void Awake () {
        if (instance != null) {
            Debug.LogError("There are two SceneData instances in the scene!");
        }
        instance = this;
    }

    public ThirdPersonController player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
