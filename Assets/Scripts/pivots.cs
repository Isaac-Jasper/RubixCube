using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pivots : MonoBehaviour
{
    public Vector3 angles;

    public float size;
    private float odd;
    public int rotType;
    public Vector3 currentAxis; 
    int speed = 500;
    private Vector3 pivotPoint;
    private Vector3 vecRot;
    private Quaternion targetQ;
    public List<Transform> cubePos; //why does this have to be a list? i have no clue but it fixes the bug
    private Transform T;
    void Start()
    {
        
        T = this.transform;
    }
    public void Rotate()
    {
        size = GameObject.FindGameObjectWithTag("CubeCreator").GetComponent<createCube>().size;
        if (size % 2 == 0)
        {
            odd = 0.5f;
        }
        size = (size / 2) - odd;

            switch (rotType)
            {
                case 0:
                    StartCoroutine(WhileRotate(true, Vector3.forward, 2));// 2 = z axis
                    break;
                case 1:
                    StartCoroutine(WhileRotate(false, Vector3.forward, 2)); //false = negative rotation
                    break;
                case 2:
                    StartCoroutine(WhileRotate(true, Vector3.right, 0));// 0 = x axis
                    break;
                case 3:
                    StartCoroutine(WhileRotate(false, Vector3.right, 0));
                    break;
                case 4:
                    StartCoroutine(WhileRotate(true, Vector3.up, 1));// 1 = y axis
                    break;
                case 5:
                    StartCoroutine(WhileRotate(false, Vector3.up, 1)); //!!!!!!!!!!!!!!!!!!!!!!!doesnt woooork
                    break;
            }
    }   
    public void rotationDirection(Vector3 RotationDirection, int rotationType)
    {
        pivotPoint = RotationDirection;
        rotType = rotationType;
    }
    public void OnTriggerEnter(Collider col) {
        if (col.CompareTag("cubePos")) {
            cubePos.Add(col.gameObject.transform);
        }
    }
    public void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("cubePos"))
        {
            cubePos.Remove(col.gameObject.transform);
        }
    }
    public IEnumerator WhileRotate(bool forwards, Vector3 Direction, int Axis)
    {
        currentAxis = transform.localEulerAngles;
        if (forwards)
        {
            if (currentAxis.x == 360)
            {
                currentAxis.x = 0;
            }
            if (currentAxis.y == 360)
            {
                currentAxis.y = 0;
            }
            if (currentAxis.z == 360)
            {
                currentAxis.z = 0;
            }
            switch (Axis)
            {
                case 0: 
                    while (T.localEulerAngles.x < currentAxis.x + 90) //currently rotate infinitly dont know why, trying to get it to actually rotate 90 degrees and stay
                    {
                        T.RotateAround(pivotPoint, Direction, speed * Time.deltaTime);
                        yield return null;
                    }
                    break;
                case 1: 
                    while (T.localEulerAngles.y < currentAxis.y + 90)
                    {
                        T.RotateAround(pivotPoint, Direction, speed * Time.deltaTime);
                        yield return null;
                    }
                    break;
                case 2: 
                    while (T.localEulerAngles.z < currentAxis.z + 90)
                    {
                        T.RotateAround(pivotPoint, Direction, speed * Time.deltaTime);
                        yield return null;
                    }
                    break;
            }
        } else
        {
            if (currentAxis.x == 0)
            {
                currentAxis.x = 360;
            }
            if (currentAxis.y == 0)
            {
                currentAxis.x = 360;
            }
            if (currentAxis.z == 0)
            {
                currentAxis.x = 360;
            }

            switch (Axis)
            {
                case 0:
                    while (T.localEulerAngles.x > currentAxis.x - 90 || T.localEulerAngles.x == currentAxis.x)
                    {
                        T.RotateAround(pivotPoint, Direction, speed * Time.deltaTime);
                        yield return null;
                    }
                    break;
                case 1:
                    while (T.localEulerAngles.y > currentAxis.y - 90 || T.localEulerAngles.y == currentAxis.y)
                    {
                        T.RotateAround(pivotPoint, Direction, speed * Time.deltaTime);
                        yield return null;
                    }
                    break;
                case 2:
                    while (T.localEulerAngles.z > currentAxis.z - 90 || T.localEulerAngles.z == currentAxis.z)
                    {
                        T.RotateAround(pivotPoint, Direction, speed * Time.deltaTime * -1);
                        yield return null;
                    }
                    break;
            }
        }
        vecRot = transform.localEulerAngles;
        T.position = cubePos[0].position;
        vecRot.x = Mathf.Round(vecRot.x / 90) * 90;
        vecRot.y = Mathf.Round(vecRot.y / 90) * 90;
        vecRot.z = Mathf.Round(vecRot.z / 90) * 90;
        targetQ.eulerAngles = vecRot;
        transform.rotation = targetQ;
    }
    void Update()
    {
        angles = T.localEulerAngles;
    }
}

