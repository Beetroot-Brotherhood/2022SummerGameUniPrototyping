using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableSelfAfterTime : MonoBehaviour
{
    void OnEnable()
    {
        Invoke(nameof(DisableSelf), 5f);
    }

    void DisableSelf()
    {
        gameObject.SetActive(false);
    }
}
