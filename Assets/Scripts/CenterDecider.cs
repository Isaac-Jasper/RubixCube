using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterDecider : MonoBehaviour
{
    public GameObject[] centerPos;
    public int cubePosCount = 0;

    public GameObject[] centerDetecters;
    public int detectersCount = 0;

    void Start() {
        centerPos = new GameObject[3];
        centerDetecters = new GameObject[3];
    }

    void OnTriggerEnter(Collider col) {
        if (col.CompareTag("cubePos")) {
            centerPos[cubePosCount] = col.gameObject;
            cubePosCount++;
        }
        if (col.CompareTag("Detecter")) {
            centerDetecters[detectersCount] = col.gameObject;
            detectersCount++;
        }
    }

    public void Distribute() {
        for (int i = 0; i < 3; i++) {
            centerDetecters[i].GetComponent<cubes>().targetPos = centerPos[i];
            centerDetecters[i].GetComponent<cubes>().canGetPos = false;
        }
        GameObject.FindGameObjectWithTag("CubeCreator").GetComponent<createCube>().confirm = true;
    }
}
