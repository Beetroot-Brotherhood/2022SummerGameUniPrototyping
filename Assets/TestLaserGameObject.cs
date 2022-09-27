using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLaserGameObject : MonoBehaviour
{
    public GameObject laserGameObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        laserGameObject.SetActive(StarterAssets.StarterAssetsInputs.instance.onFire1);
    }
}
