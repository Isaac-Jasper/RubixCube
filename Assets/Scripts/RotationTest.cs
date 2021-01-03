using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class RotationTest : MonoBehaviour
{
    public float x = 0;
    public float y = 0;
    public float z = 0;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            transform.Rotate(x, y, z);
        }
    }
}
