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

    private createCube cubeCreator;

    void Start() {
        cubeCreator = GameObject.FindGameObjectWithTag("CubeCreator").GetComponent<createCube>();
        centerPos = new GameObject[3];
        centerDetecters = new GameObject[3];
        cubeCreator.centerDeciderCreated = true;
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
        if (cubePosCount == 3 && detectersCount == 3)
        {
            cubeCreator.hasDetected = true;
        }
    } //if cubePos enters its added to an array, and if Detecter enters its added to an array. both arrays should have 3 items in them if this object is needed 
    public void Distribute() {
        for (int i = 0; i < 3; i++) {
            centerDetecters[i].GetComponent<cubes>().targetPos = centerPos[i];
            centerDetecters[i].GetComponent<cubes>().canGetPos = false;
        }
        cubeCreator.confirm = true;
    } //distributes the TargetPos Gameobjects to the Detecers so the three detectors that intersect at the middle all get different targetPos attached to them
}
