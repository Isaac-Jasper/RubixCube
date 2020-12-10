using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTest : MonoBehaviour
{
    public float x = 0;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            x += 0.1f;
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }
}
