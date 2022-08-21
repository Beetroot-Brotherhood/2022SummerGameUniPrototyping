using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchGameObjectOnStart : MonoBehaviour
{

    private Rigidbody rb;

    public float speed;

    private float currentLivePassed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.up * speed;
    }

    // Update is called once per frame
    void Update()
    {
        currentLivePassed += Time.deltaTime;

        if (currentLivePassed >= 2) {
            Destroy(this.gameObject);
        }
    }
}
