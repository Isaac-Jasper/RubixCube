using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTestSmooth : MonoBehaviour
{
    public GameObject target;
    void Update()
    {
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, target.transform.localRotation, 0.02f);
    }
}
