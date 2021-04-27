using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockChoiceCRo : MonoBehaviour
{
    Quaternion defQ;

    private void Awake()
    {
        defQ = transform.rotation;
    }
    void Update()
    {
        transform.rotation = defQ;
    }
}
